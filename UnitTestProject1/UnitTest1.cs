using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            RJob.Rjob jobs = new RJob.Rjob(new System.Collections.Generic.List<RJob.RJobOptions>() {
                new RJob.RJobOptions(){
                     canReeentry=false,
                      interval=5,
                      Run=()=>{
                          System.Console.WriteLine("5"+DateTime.Now.ToString());
                          System.Threading.Thread.Sleep(5000);
                      }
                },

                new RJob.RJobOptions(){
                     canReeentry=false,
                      interval=6,
                      Run=()=>{
                          System.Console.WriteLine("6"+DateTime.Now.ToString());
                          System.Threading.Thread.Sleep(5000);
                      }
                },
            }, 
            (e) =>
            {
                System.Console.WriteLine(e.ToString());
            });

            jobs.Start();

            System.Threading.Thread.Sleep(1000*1000);
        }
    }
}
