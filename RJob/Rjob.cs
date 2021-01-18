using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RJob
{
    public class Rjob
    {
        /// <summary>
        /// 初始化job
        /// </summary>
        /// <param name="optionsList">job集合</param>
        /// <param name="onException">在job运行出错时的回调函数</param>
        public Rjob(List<RJobOptions> optionsList,Action<Exception> onException)
        {
            Options = optionsList;
            this.onException = onException;
        }
        private Action<Exception> onException { get; set; }
        public List<RJobOptions> Options { get; set; }
        private Timer timer { get; set; } = null;//计时器
        /// <summary>
        /// 启动job
        /// </summary>
        public void Start()
        {
            timer = new Timer(new TimerCallback((state) =>
            {
                Task.Run(()=> {
                    foreach (var item in Options)
                    {
                        if (item.CanRun())
                        {
                            //在task中运行job
                            Task.Run(() =>
                            {
                                try
                                {
                                    item.Run();
                                }
                                catch (Exception e)
                                {
                                    try
                                    {
                                        //在task中运行异常处理程序
                                        Task.Run(() => {
                                            this.onException(e);
                                        });
                                    }
                                    catch(Exception e2)
                                    {

                                    }
                                }
                            });
                            
                        }
                    }
                });
            }), null, 0, 500);
            return;
        }
        public void Stop()
        {
            var tmp = timer;
            timer = null;
            tmp.Dispose();
        }
    }
}
