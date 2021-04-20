using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DspatchService.CustomListener
{
    public class CustomTriggerListener : ITriggerListener
    {
        public string Name => "CustomTriggerListener";

        /// <summary>
        /// 时间策略触发完成时调用
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="triggerInstructionCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"时间策略:{trigger.Key.Name}被使用。使用的工作是:{context.JobDetail.Key.Name};时间策略状态是:{triggerInstructionCode}");
            });
        }

        /// <summary>
        /// 在执行工作之前调用这个方法
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"时间策略:{trigger.Key.Name}即将执行具体工作:{context.JobDetail.Key.Name}。");
            });
        }

        /// <summary>
        /// 时间策略失败时调用
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{trigger.Key.Name}触发失败");
            });
        }

        /// <summary>
        /// 在执行工作前，TriggerFired之后调用
        /// 如果返回true，不会执行工作
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("VetoJobExecution执行");
            });
            return false;
        }
    }
}
