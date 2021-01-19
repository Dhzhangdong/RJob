using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RJob.Rjob jobs = new RJob.Rjob(new System.Collections.Generic.List<RJob.RJobOptions>() {
                new RJob.RJobOptions(){
                      Run=()=>{
                          System.Console.WriteLine("5"+DateTime.Now.ToString());
                      },
                      GetRunKey=()=>{
                          return DateTime.Now.ToString("ss");
                      },
                },
            },
            (e) =>
            {
                System.Console.WriteLine(e.ToString());
            });

            jobs.Start();

            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}
