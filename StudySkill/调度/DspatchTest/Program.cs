using DspatchService;
using System;

namespace DspatchTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DspatchManage.Init().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            Console.Read();
        }
    }
}
