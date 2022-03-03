using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 回应代码集
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        ExecSucceeded = 1,

        /// <summary>
        /// 执行失败，但不知道原因
        /// </summary>
        ExecutionFailed_DoNotKnowReason = 2,

        /// <summary>
        /// 执行失败，命令请求条件错误
        /// </summary>
        ExecutionFailed_InvalidCommand = 3,

        /// <summary>
        /// 通讯超时
        /// </summary>
        CommunicationTimeout = 4,

        /// <summary>
        /// 系统繁忙不能执行
        /// </summary>
        SystemBusy = 5,

        /// <summary>
        /// 系统时间异常
        /// </summary>
        InvalidSystemTime = 6,

        /// <summary>
        /// 没有数据
        /// </summary>
        NoneData = 100,

        /// <summary>
        /// 心跳包
        /// </summary>
        HeartbeatPackage = 300
    }
}
