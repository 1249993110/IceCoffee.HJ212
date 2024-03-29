using System;
using System.Collections.Generic;
using System.Text;

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
        /// </summary>
        public string Tail { get; set; }

        

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="line"></param>
        /// <param name="unpackCacheFunc"></param>
        /// <returns></returns>
        public static NetPackage Parse(string line, Func<StringBuilder> unpackCacheFunc = null)
        {
            try
            {
                NetPackage netPackage = new NetPackage();
                netPackage.Head = line.Substring(0, 2);
                netPackage.DataSegmentLength = int.Parse(line.Substring(2, 4));

                string dataSegment = line.Substring(6, netPackage.DataSegmentLength);

                string crcCode = line.Substring(6 + netPackage.DataSegmentLength, 4);
                string calcCrcCode = Utils.CRC16(dataSegment);
                if (crcCode != calcCrcCode)
                {
                    throw new Exception("CRC校验失败 " + line);
                }

                netPackage.DataSegment = DataSegment.Parse(dataSegment, unpackCacheFunc);
                netPackage.CrcCode = crcCode;
                netPackage.Tail = line.Substring(10 + netPackage.DataSegmentLength);

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
    }
}
