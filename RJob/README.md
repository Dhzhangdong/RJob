# RJob

## 说明

Rjob是.net core 3.1平台下的定时任务管理工具，可以在.net core 控制台运行，也可以在asp.net core上运行.

**在asp.net core上运行的时候如果使用iis承载，可能会因为iis进程回收导致定时任务错过运行时间，可以通过启用iis预加载来避免进程回收后进程不启动问题**。



在asp.net core中启动job的代码必须在CreateHostBuilder(args).Build().Run(); 之前。



## 使用方式



```
    class Program
    {
        static void Main(string[] args)
        {
            RJob.Rjob jobs = new RJob.Rjob(new System.Collections.Generic.List<RJob.RJobOptions>() {
                new RJob.RJobOptions(){
                      Run=()=>{
                          //这里是job的具体逻辑
                          System.Console.WriteLine("5"+DateTime.Now.ToString());
                      },
                      GetRunKey=()=>{
                          //此回调函数每隔500毫秒调用一次，如果返回的字符串与上次的值不同，表示任务需要运行。
                          //返回null不回调
                          return DateTime.Now.ToString("ss");
                      },
                      canReeentry=true,//是否可重入，默认不可重入，在run回调没有完成时候不会重新进入
                      Name="我的testJob1"//job名称，建议不要重复
                },

                new RJob.RJobOptions(){
                     GetRunKey=()=>{ 
                        //每天1点运行
                        if(DateTime.Now.Hour!=1) return null;//返回null不运行
                        return DateTime.Now.ToString("dd");
                     },
                     Run=()=>{
                         ; ; ; ;
                     }
                }
            },
            (e) => {//Rjob在执行所以回调函数时产生的错误，包括job运行错误都会调用此回调
                System.Console.WriteLine(e.ToString());
            },
            (opt)=> {//有任务开始时执行此回调,注意：此回调函数如果内部报错会导致所有job运行失败
                System.Console.WriteLine($"{opt.Name} 任务开始运行");
            },
            (opt)=> {// 有任务结束时执行此回调
                System.Console.WriteLine($"{opt.Name} 任务结束运行");
            });

            jobs.Start();

            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
```

