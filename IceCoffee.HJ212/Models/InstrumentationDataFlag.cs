using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 检测仪器数据标记
    /// </summary>
    public enum InstrumentationDataFlag
    {
        /// <summary>
        /// 正常（有效）
        /// <para>在线监控（监测）仪器仪表工作正常</para>
        /// </summary>
        N,

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表停运</para>
        /// </summary>
        F,

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表处于维护期间产生的数据 </para>
        /// </summary>
        M,

        /// <summary>
        /// 有效
        /// <para>手工输入的设定值</para>
        /// </summary>
        S,

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表故障</para>
        /// </summary>
        D,

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表处于校准状态</para>
        /// </summary>
        C,

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表采样数值超过测量上限</para>
        /// </summary>
        T,

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表与数采仪通讯异常</para>
        /// </summary>
        B,

        /// <summary>
        /// 无效（有效数据不足）
        /// </summary>
        /// <remarks>
        /// 按照5分钟、1小时均值计算要求，所获取的有效数据个数不足
        /// </remarks>
        H
    }
}
