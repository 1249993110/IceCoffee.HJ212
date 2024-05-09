namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 执行结果定义
    /// </summary>
    public struct ExecutionResultDefinition
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        public const int ExecSucceeded = 1;

        /// <summary>
        /// 执行失败, 但不知道原因
        /// </summary>
        public const int ExecutionFailed_DoNotKnowReason = 2;

        /// <summary>
        /// 执行失败, 命令请求条件错误
        /// </summary>
        public const int ExecutionFailed_InvalidCommand = 3;

        /// <summary>
        /// 通讯超时
        /// </summary>
        public const int CommunicationTimeout = 4;

        /// <summary>
        /// 系统繁忙不能执行
        /// </summary>
        public const int SystemBusy = 5;

        /// <summary>
        /// 系统时间异常
        /// </summary>
        public const int InvalidSystemTime = 6;

        /// <summary>
        /// 没有数据
        /// </summary>
        public const int NoData = 100;
    }
}
