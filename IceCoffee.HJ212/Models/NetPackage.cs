using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 通讯包
    /// </summary>
    public class NetPackage
    {
        /// <summary>
        /// 默认头
        /// </summary>
        public const string FixedHead = "##";

        /// <summary>
        /// 默认尾
        /// </summary>
        public const string FixedTail = "\r\n";

        /// <summary>
        /// 包头
        /// <para>固定为"##"</para>
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// 数据段长度
        /// </summary>
        public int DataSegmentLength { get; set; }

        /// <summary>
        /// 数据段
        /// </summary>
        public DataSegment DataSegment { get; set; }

        /// <summary>
        /// CRC校验码
        /// </summary>
        public string CrcCode { get; set; }

        /// <summary>
        /// 包尾
        /// <para>固定为&lt;CR&gt;&lt;LF&gt;（回车、换行）</para>
        /// </summary>
        public string Tail { get; set; }

        public NetPackage()
        {
            Head = FixedHead;
            DataSegment = new DataSegment();
            CrcCode = string.Empty;
            Tail = FixedTail;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="rawText"></param>
        /// <param name="unpackCacheFunc">分包缓存</param>
        /// <returns></returns>
        public static NetPackage Parse(string rawText, Func<StringBuilder> unpackCacheFunc)
        {
            try
            {
                var netPackage = new NetPackage();
                netPackage.Head = rawText.Substring(0, 2);
                netPackage.DataSegmentLength = int.Parse(rawText.Substring(2, 4));

                string dataSegment = rawText.Substring(6, netPackage.DataSegmentLength);

                string actualCrc = rawText.Substring(6 + netPackage.DataSegmentLength, 4);
                string expectedCrc = Utils.CRC16(dataSegment);
                if (actualCrc != expectedCrc)
                {
                    throw new Exception($"CRC校验失败, 实际值: {actualCrc}, 预期值: {expectedCrc}");
                }

                netPackage.DataSegment = DataSegment.Parse(dataSegment, unpackCacheFunc);
                netPackage.CrcCode = actualCrc;
                netPackage.Tail = FixedTail;

                return netPackage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in NetPackage.Parse", ex);
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public virtual string Serialize()
        {
            string dataSegment = DataSegment.Serialize();
            DataSegmentLength = dataSegment.Length;
            CrcCode = Utils.CRC16(dataSegment);

            return $"{Head}{DataSegmentLength.ToString().PadLeft(4, '0')}{dataSegment}{CrcCode}{Tail}";
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public virtual NetPackage Clone()
        {
            return new NetPackage()
            {
                Head = this.Head,
                DataSegment = DataSegment.Clone(),
                Tail = this.Tail
            };
        }
    }
}
