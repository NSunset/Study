using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;
using DspatchService.CustomListener;

namespace DspatchService
{
    public class DspatchManage
    {
        public static async Task Init()
        {
            //调度器
            StdSchedulerFactory stdSchedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await stdSchedulerFactory.GetScheduler();

            await scheduler.Start();

            scheduler.ListenerManager.AddJobListener(new CustomJobListener());
            scheduler.ListenerManager.AddTriggerListener(new CustomTriggerListener());


            //具体工作
            IJobDetail jobDetail = JobBuilder.Create<SendMessageJob>()
                                             .WithDescription("发送消息调度")
                                             .WithIdentity("sendMessage", "user")
                                             .UsingJobData("id", 100)
                                             .UsingJobData("name", "张三")
                                             .UsingJobData("endDate", "2019-3-21")
                                             .Build();


            //Simple时间策略
            ITrigger trigger = TriggerBuilder.Create()
                                             .WithDescription("指定每5秒执行一次")
                                             .WithIdentity("5Second", "Second")
                                             .WithSimpleSchedule(builder =>
                                                 builder.WithIntervalInSeconds(3)
                                                        .WithRepeatCount(3)
                                             //.WithMisfireHandlingInstructionFireNow()
                                             //.WithMisfireHandlingInstructionIgnoreMisfires()
                                             //.WithMisfireHandlingInstructionNextWithExistingCount()
                                             //.WithMisfireHandlingInstructionNextWithRemainingCount()
                                             //.WithMisfireHandlingInstructionNowWithExistingCount()
                                             //.WithMisfireHandlingInstructionNowWithRemainingCount()

                                             )
                                             .UsingJobData("name1", "小旋风")
                                             .Build();
            trigger.JobDataMap.Add("endDate", "2021-3-24");



            //Cron时间策略
            //ITrigger trigger = TriggerBuilder.Create()
            //                               .WithDescription("指定具体时间点执行")
            //                               .WithIdentity("Fixed_Time", "Fixed")
            //                               .WithCronSchedule("0 33 11 * * ?"
            //                                                //,builder => builder
            //                                                //.WithMisfireHandlingInstructionDoNothing()
            //                                                //.WithMisfireHandlingInstructionFireAndProceed()
            //                                                //.WithMisfireHandlingInstructionIgnoreMisfires()
            //                               )
            //                               .Build();



            //await scheduler.ScheduleJob(jobDetail, trigger);

            Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> keyValuePairs = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();

            keyValuePairs[jobDetail] = new List<ITrigger>() { trigger };
            await scheduler.ScheduleJobs(keyValuePairs, false);
        }
    }
}
