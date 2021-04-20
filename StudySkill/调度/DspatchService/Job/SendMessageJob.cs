using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DspatchService
{
    [DisallowConcurrentExecution]
    public class SendMessageJob : IJob
    {

        public SendMessageJob()
        {

        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);

                int id = context.JobDetail.JobDataMap.GetInt("id");
                string name = context.JobDetail.JobDataMap.GetString("name");
                DateTime time = context.JobDetail.JobDataMap.GetDateTimeValueFromString("endDate");




                string name1 = context.Trigger.JobDataMap.GetString("name1");
                DateTime time1 = context.Trigger.JobDataMap.GetDateTime("endDate");

                Console.WriteLine($"id:{id};姓名:{name};过期时间:{time}发送信息:{DateTime.Now}");
                Console.WriteLine($"姓名1:{name1};过期时间1:{time1}");
                Console.WriteLine($"***************************");
            });
        }
    }
}
