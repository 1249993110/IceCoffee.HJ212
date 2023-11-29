using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 污染物信息
    /// </summary>
    public class PollutantInfo
    {
        /// <summary>
        /// 约定的无效值
        /// </summary>
        public const decimal InvaildValue = -9999;

        /// <summary>
        /// 污染物因子编码
        /// </summary>
        public FactorCode FactorCode { get; set; }

        /// <summary>
        /// 污染物实时采样数据
        /// </summary>
        /// <remarks>
        /// 默认值为约定的无效值 <see cref="InvaildValue"/>
        /// </remarks>
        public decimal Rtd { get; set; } = InvaildValue;

        /// <summary>
        /// 污染物指定时间内平均值
        /// </summary>
        /// <remarks>
        /// 默认值为约定的无效值 <see cref="InvaildValue"/>
        /// </remarks>
        public decimal Avg { get; set; } = InvaildValue;

        /// <summary>
        /// 污染物指定时间内最大值
        /// </summary>
        /// <remarks>
        /// 默认值为约定的无效值 <see cref="InvaildValue"/>
        /// </remarks>
        public decimal Max { get; set; } = InvaildValue;

        /// <summary>
        /// 污染物指定时间内最小值
        /// </summary>
        /// <remarks>
        /// 默认值为约定的无效值 <see cref="InvaildValue"/>
        /// </remarks>
        public decimal Min { get; set; } = InvaildValue;

        /// <summary>
        /// 污染物指定时间内累计值
        /// </summary>
        /// <remarks>
        /// 默认值为约定的无效值 <see cref="InvaildValue"/>
        /// </remarks>
        public decimal Cou { get; set; } = InvaildValue;

        /// <summary>
        /// 检测仪器数据标记
        /// </summary>
        public InstrumentationDataFlag Flag { get; set; }
    }
}
