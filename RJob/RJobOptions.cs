using System;
using System.Collections.Generic;
using System.Text;

namespace RJob
{
    public class RJobOptions
    {
        public Action Run { get; set; }//定时任务
        ///// <summary>
        ///// null 表示未设置
        ///// -1表示每个周期
        ///// </summary>
        //private int? years { get; set; }
        //private int? month { get; set; }
        //private int? week { get; set; }
        //private int? day { get; set; }
        //private int? hours { get; set; }
        //private int? minutes { get; set; }
        //private int? seconds { get; set; }
        /// <summary>
        /// 间隔多少秒
        /// </summary>
        public int? interval { get; set; }//间隔
        private bool runNow { get; set; } = false;//是否立即运行
        public bool canReeentry { get; set; } = false;//是否可重入
        private DateTime? lastRuntime { get; set; }//最后一次运行时间
        private bool runNowOk { get; set; } = false;//立即运行是否执行
        /// <summary>
        /// 是否到了可运行状态，返回true任务将进入执行队列（1秒内执行）
        /// </summary>
        /// <returns></returns>
        public bool CanRun()
        {
            var now = DateTime.Now;
            var canrun = false;//是否可以执行
            
            if (!this.runNow)
            {//立即运行
                this.runNow = true;
                canrun = true;
                this.lastRuntime = now;
                return canrun;
            }

            if(lastRuntime.HasValue 
                && interval.HasValue 
                && (now-lastRuntime.Value).TotalSeconds>= interval.Value)
            {
                canrun = true;
                this.lastRuntime = now;
                return canrun;
            }
            return false;
        }
        /// <summary>
        /// 判断当前时间是否为可执行时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        //private bool TimeisRunTime(DateTime time)
        //{
        //    var now = time;
        //    var toruntime = false;//是否到达执行时间
        //    List<kvMap<int>> runtime = new List<kvMap<int>>()
        //    {
        //        new kvMap<int>(){  k=years,v=now.Year,},
        //        new kvMap<int>(){  k=month,v=now.Month,},
        //        new kvMap<int>(){  k=week,v=-100},
        //        new kvMap<int>(){  k=day,v=now.Day,},
        //        new kvMap<int>(){  k=hours,v=now.Hour,},
        //        new kvMap<int>(){  k=minutes,v=now.Minute,},
        //        new kvMap<int>(){  k=seconds,v=now.Second,}
        //    };
        //    foreach (var item in runtime)
        //    {
        //        if (item.k.HasValue)
        //        {
        //            if (item.k.Value == -1)
        //            {
        //                toruntime = true;
        //            }
        //            else
        //            {
        //                if (item.v != -100)
        //                {
        //                    if (item.v == item.k.Value)
        //                    {
        //                        toruntime = true;
        //                    }
        //                    else
        //                    {
        //                        toruntime = false;//没达到执行时间
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return toruntime;
        //}
        /// <summary>
        /// 每年
        /// </summary>
        /// <returns></returns>
        //public RJobOptions EveryYears()
        //{
        //    this.years = -1;
        //    return this;
        //}
        ///// <summary>
        ///// 每月
        ///// </summary>
        ///// <returns></returns>
        //public RJobOptions EveryMonth()
        //{
        //    this.minutes = -1;
        //    return this;
        //}
        ///// <summary>
        ///// 每周
        ///// </summary>
        ///// <returns></returns>
        //public RJobOptions EveryWeek()
        //{
        //    this.week = -1;
        //    return this;
        //}
        ///// <summary>
        ///// 每天
        ///// </summary>
        ///// <returns></returns>
        //public RJobOptions EveryDay()
        //{
        //    this.day = -1;
        //    return this;
        //}
        ///// <summary>
        ///// 每小时
        ///// </summary>
        ///// <returns></returns>
        //public RJobOptions EveryHours()
        //{
        //    this.hours = -1;
        //    return this;
        //}
        ///// <summary>
        ///// 每分钟
        ///// </summary>
        ///// <returns></returns>
        //public RJobOptions EveryMinutes()
        //{
        //    this.minutes = -1;
        //    return this;
        //}
        ///// <summary>
        ///// 每秒
        ///// </summary>
        ///// <returns></returns>
        //public RJobOptions EverySeconds()
        //{
        //    this.seconds = -1;
        //    return this;
        //}
        ///// <summary>
        ///// 在某一年
        ///// </summary>
        ///// <returns></returns>
        //public RJobOptions AtYears(int year)
        //{
        //    this.years = year;
        //    return this;
        //}
        ///// <summary>
        ///// 在某一月（1-12）
        ///// </summary>
        ///// <param name="week"></param>
        ///// <returns></returns>
        //public RJobOptions AtMonth(int mouth)
        //{
        //    this.month = month;
        //    return this;
        //}

        ///// <summary>
        ///// 在某一天（动态月1-x）
        ///// </summary>
        ///// <param name="day"></param>
        ///// <returns></returns>
        //public RJobOptions AtDay(int day)
        //{
        //    this.day = day;
        //    return this;
        //}
        ///// <summary>
        ///// 在某一小时（值范围0-23）
        ///// </summary>
        ///// <param name="hours"></param>
        ///// <returns></returns>
        //public RJobOptions AtHours(int hours)
        //{
        //    this.hours = hours;
        //    return this;
        //}
        ///// <summary>
        ///// 在某一分（值范围0-59）
        ///// </summary>
        ///// <param name="minutes"></param>
        ///// <returns></returns>
        //public RJobOptions AtMinutes(int minutes)
        //{
        //    this.minutes = minutes;
        //    return this;
        //}
        ///// <summary>
        ///// 在某一秒（值范围0-59）
        ///// </summary>
        ///// <param name="seconds"></param>
        ///// <returns></returns>
        //public RJobOptions AtSeconds(int seconds)
        //{
        //    this.seconds = seconds;
        //    return this;
        //}
        /// <summary>
        /// 启动有立即运行
        /// </summary>
        /// <returns></returns>
        public RJobOptions RunNow()
        {
            this.runNow = true;
            return this;
        }
        /// <summary>
        /// job可重入（在上一次执行没有完成时进入下一次执行）
        /// </summary>
        /// <returns></returns>
        public RJobOptions CanReentry()
        {
            this.canReeentry = true;
            return this;
        }

        class kvMap<T>
        {
            public int? k { get; set; }
            public T v { get; set; }
        }
    }
}
