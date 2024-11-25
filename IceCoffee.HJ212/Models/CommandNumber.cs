namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 命令编码
    /// <para>6.6.5命令编码（可扩充）</para>
    /// </summary>
    public struct CommandNumbers
    {
        /// <summary>
        /// 现场机时间校准请求 
        /// </summary>
        public const int TimeCalibration = 1013;

        /// <summary>
        /// 上传实时数据
        /// </summary>
        public const int UploadRealTimeData = 2011;

        /// <summary>
        /// 上传分钟数据
        /// </summary>
        public const int UploadMinuteData = 2051;

        /// <summary>
        /// 上传小时数据
        /// </summary>
        public const int UploadHourlyData = 2061;

        /// <summary>
        /// 上传日数据
        /// </summary>
        public const int UploadDailyData = 2031;

        /// <summary>
        /// 零点校准、量程校准
        /// </summary>
        public const int ZeroAndRangeCalibration = 3011;

        /// <summary>
        /// 回应通知
        /// </summary>
        public const int NoticeResponse = 9013;

        /// <summary>
        /// 数据应答
        /// </summary>
        public const int DataResponse = 9014;
    }
}
