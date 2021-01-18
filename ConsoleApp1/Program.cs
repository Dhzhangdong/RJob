using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RJob.Rjob jobs = new RJob.Rjob(new System.Collections.Generic.List<RJob.RJobOptions>() {
                new RJob.RJobOptions(){
                      canReeentry=false,//是否可重入
                      Run=()=>{
                          System.Console.WriteLine("5"+DateTime.Now.ToString());
                          System.Threading.Thread.Sleep(5000);
                      },
                      GetRunKey=()=>{
                          return DateTime.Now.ToString("YYYYMMDDHHmmss");
                      },
                },
            },
            (e) =>
            {
                System.Console.WriteLine(e.ToString());
            });

            jobs.Start();

            Console.WriteLine("Hello World!");
        }
    }
}
