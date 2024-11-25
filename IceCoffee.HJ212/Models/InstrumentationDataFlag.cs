namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 检测仪器数据标记
    /// <para>6.6.4数据标记（可扩充）</para>
    /// </summary>
    public struct InstrumentationDataFlag
    {
        /// <summary>
        /// 正常（有效）
        /// <para>在线监控（监测）仪器仪表工作正常</para>
        /// </summary>
        public const string N = nameof(N);

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表停运</para>
        /// </summary>
        public const string F = nameof(F);

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表处于维护期间产生的数据 </para>
        /// </summary>
        public const string M = nameof(M);

        /// <summary>
        /// 有效
        /// <para>手工输入的设定值</para>
        /// </summary>
        public const string S = nameof(S);

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表故障</para>
        /// </summary>
        public const string D = nameof(D);

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表处于校准状态</para>
        /// </summary>
        public const string C = nameof(C);

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表采样数值超过测量上限</para>
        /// </summary>
        public const string T = nameof(T);

        /// <summary>
        /// 无效
        /// <para>在线监控（监测）仪器仪表与数采仪通讯异常</para>
        /// </summary>
        public const string B = nameof(B);
    }
}
