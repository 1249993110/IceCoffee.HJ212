using IceCoffee.Common.Extensions;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// CP指令参数
    /// </summary>
    public class CpCommand
    {
        public CpCommand()
        {
            RawText = string.Empty;
        }

        public CpCommand(string rawText)
        {
            RawText = rawText;
        }

        /// <summary>
        /// 原始文本
        /// </summary>
        public string RawText { get; private set; }

        /// <summary>
        /// 分号分割后的集合
        /// </summary>
        public string[] GetValues()
        {
#if NETCOREAPP
            return RawText.Split(';', StringSplitOptions.RemoveEmptyEntries);
#else
            return RawText.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
#endif
        }

        /// <summary>
        /// 获取日期时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTime(string dateTimeKey = "DataTime")
        {
            string dateTimeStr = RawText.GetMidStr(dateTimeKey + "=", ";");
            return DateTime.ParseExact(dateTimeStr, "yyyyMMddHHmmss", null);
        }

        /// <summary>
        /// 尝试获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out string value, string end = ";")
        {
            return TryGetValue(RawText, key, out value, end);
        }

        /// <summary>
        /// 尝试获取值
        /// </summary>
        /// <param name="rawText"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool TryGetValue(string rawText, string key, out string value, string end = ";")
        {
            value = rawText.GetMidStr(key + "=", end);
            return string.IsNullOrEmpty(value) == false;
        }
    }
}
