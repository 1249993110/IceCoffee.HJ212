namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 请求命令返回
    /// <para>6.6.3 请求命令返回（可扩充）</para>
    /// </summary>
    public struct RequestCommandReturn
    {
        /// <summary>
        /// 准备执行请求
        /// </summary>
        public const int PreparingExecuteRequest = 1;

        /// <summary>
        /// 请求被拒绝
        /// </summary>
        public const int RequestDenied = 2;

        /// <summary>
        /// PW 错误
        /// </summary>
        public const int PW_Error = 3;

        /// <summary>
        /// MN 错误
        /// </summary>
        public const int MN_Error = 4;

        /// <summary>
        /// ST 错误
        /// </summary>
        public const int ST_Error = 5;

        /// <summary>
        /// Flag 错误
        /// </summary>
        public const int Flag_Error = 6;

        /// <summary>
        /// QN 错误
        /// </summary>
        public const int QN_Error = 7;

        /// <summary>
        /// CN 错误
        /// </summary>
        public const int CN_Error = 8;

        /// <summary>
        /// CRC 校验错误
        /// </summary>
        public const int CRC_CheckError = 9;

        /// <summary>
        /// 未知错误
        /// </summary>
        public const int UnknownError = 100;
    }
}
