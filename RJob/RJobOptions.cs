using System;
using System.Collections.Generic;
using System.Text;

namespace RJob
{
    public class RJobOptions
    {
        public Action Run { get; set; }//定时任务
        /// <summary>
        /// 生成一个运行用的key标记，每一个新的标记可以触发一次运行，如果返回重复的key则不执行
        /// 此函数每500毫秒调用一次
        /// </summary>
        public Func<string> GetRunKey { get; set; }
        public bool canReeentry { get; set; } = false;//是否可重入
        private string lastRunkey { get; set; }//最后一次执行使用的key
        /// <summary>
        /// 是否到了可运行状态，返回true任务将进入执行队列（1秒内执行）
        /// </summary>
        /// <returns></returns>
        public bool CanRun()
        {
            if (Run == null || GetRunKey == null) return false;

            var nowkey = GetRunKey();
            if (nowkey == null) return false;//null不运行
            if (nowkey != lastRunkey)
            {
                lastRunkey = nowkey;
                return true;
            }
            return false;
        }
        private bool isruning = false;
        public void Run2()
        {
            isruning = true;
            if (!canReeentry)
            {
                if (isruning)
                {
                    return;
                }
            }

            try
            {
                this.Run();
            }
            finally
            {
                isruning = false;
            }
        }
    }
}
