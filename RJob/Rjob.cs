﻿using System;
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
        /// <param name="onbegin">在job开始运行时执行的回调</param>
        /// <param name="onend">在job结束运行时执行的回调</param>
        public Rjob(List<RJobOptions> optionsList,Action<Exception> onException, Action<RJobOptions> onbegin=null,Action<RJobOptions> onEnd=null)
        {
            Options = optionsList;
            this.onException = onException;
            this.onBegin = onbegin;
            this.onEnd = onEnd;
        }
        private Action<Exception> onException { get; set; }
        private Action<RJobOptions> onBegin { get; set; }
        private Action<RJobOptions> onEnd { get; set; }
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
                                    if (onBegin != null) onBegin(item);
                                    item.Run2();
                                }
                                catch (Exception e)
                                {
                                    try
                                    {
                                        //在task中运行异常处理程序
                                        Task.Run(() =>
                                        {
                                            this.onException(e);
                                        });
                                    }
                                    catch (Exception e2)
                                    {

                                    }
                                }
                                finally
                                {
                                    try
                                    {
                                        if (onEnd != null) onEnd(item);
                                    }
                                    catch( Exception e)
                                    {
                                        try
                                        {
                                            //在task中运行异常处理程序
                                            Task.Run(() =>
                                            {
                                                this.onException(e);
                                            });
                                        }
                                        catch (Exception e2)
                                        {

                                        }
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
