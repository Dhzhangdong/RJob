# RJob

## 说明

Rjob是.net core 3.1平台下的定时任务管理工具，可以在.net core 控制台运行，也可以在asp.net core上运行.

**在asp.net core上运行的时候如果使用iis承载，可能会因为iis进程回收导致定时任务错误运行时间**。



## 使用方式



```
class Program
    {
        static void Main(string[] args)
        {
            RJob.Rjob jobs = new RJob.Rjob(new System.Collections.Generic.List<RJob.RJobOptions>() {
                new RJob.RJobOptions(){
                      canReeentry=false,//是否可重入，默认不可重入，在run回调没有完成时候不会重新进入
                      Run=()=>{//任务,这里是任务的具体逻辑
                          System.Console.WriteLine("5"+DateTime.Now.ToString());
                          System.Threading.Thread.Sleep(5000);
                      },
                      GetRunKey=()=>{//此回调函数每隔500毫秒调用一次，如果返回的字符串与上次的值不同，表示任务需要运行。
                          return DateTime.Now.ToString("YYYYMMDDHHmmss");//此出表示每秒执行一次
                      },
                },

                new RJob.RJobOptions(){
                     GetRunKey=()=>{ 
                        //每天1点运行
                        
                        if(DateTime.Now.Hour!=1) return null;//返回null不运行
                        return DateTime.Now.ToString("dd");
                     },
                     Run=()=>{ 
                        
                     }
                }
            },
            (e) =>
            {//job在运行中出错时会调用此函数，这里一般将job错误写入到数据
                System.Console.WriteLine(e.ToString());
            });

            jobs.Start();//启动job，

            Console.WriteLine("Hello World!");
        }
    }
```

