using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DspatchService.CustomListener
{
    public class CustomJobListener : IJobListener
    {
        public string Name => "CustomJobListener";

        /// <summary>
        /// 即将执行JobDetail时，Trigger中断了。触发
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"时间策略:{context.Trigger.Key.Name}中断,导致{context.JobDetail.Key.Name}没有执行");
            });
        }

        /// <summary>
        /// 即将执行JobDetail时触发。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"即将执行的工作是:{context.JobDetail.Key.Name};具体的时间策略是:{context.Trigger.Key.Name}");
            });
        }

        /// <summary>
        /// JobDetail执行完后触发
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                if (jobException != null)
                {
                    Console.WriteLine($"执行{context.JobDetail.Key.Name}发生错误;具体原因:{jobException.InnerException.InnerException.Message}");
                }
                Console.WriteLine($"执行结束的工作是:{context.JobDetail.Key.Name};具体的时间策略是:{context.Trigger.Key.Name}");
            });
        }
    }
}
