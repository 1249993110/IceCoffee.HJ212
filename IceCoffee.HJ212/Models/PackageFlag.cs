using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 拆分包及应答标志
    /// </summary>
    public class PackageFlag
    {
        public byte V5 { get; set; }
        public byte V4 { get; set; }
        public byte V3 { get; set; }
        public byte V2 { get; set; }
        public byte V1 { get; set; }
        public byte V0 { get; set; }

        /// <summary>
        /// 命令是否应答：1－应答，0－不应答
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// 是否有数据包序号：1 - 数据包中包含包号和总包数两部分，0 - 数据包中不包含包号和总包数两部分
        /// </summary>
        public byte D { get; set; }

        /// <summary>
        /// 标准版本号
        /// <para>000000 表示标准 HJ/T212-2005</para>
        /// <para>000001 表示本次标准修订版本号</para>
        /// </summary>
        public string Version
        {
            get
            {
                return $"{V5}{V4}{V3}{V2}{V1}{V0}";
            }
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static PackageFlag Parse(string data)
        {
            byte flag = byte.Parse(data);
            return new PackageFlag()
            {

                V5 = GetBit(flag, 7),
                V4 = GetBit(flag, 6),
                V3 = GetBit(flag, 5),
                V2 = GetBit(flag, 4),
                V1 = GetBit(flag, 3),
                V0 = GetBit(flag, 2),
                D = GetBit(flag, 1),
                A = GetBit(flag, 0)
            };
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public virtual string Serialize()
        {
            return Convert.ToInt32($"{V5}{V4}{V3}{V2}{V1}{V0}{D}{A}", 2).ToString();
        }

        /// <summary>
        /// 获取取第index位
        /// </summary>
        /// <remarks>
        /// index从0开始
        /// </remarks>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static byte GetBit(byte b, int index) 
        {
            // (byte)((from & (0xFF << (index * 8))) >> (index * 8))
            return ((b & (1 << index)) > 0) ? (byte)1 : (byte)0; 
        }

        /// <summary>
        /// 将第index位设为1
        /// </summary>
        /// <remarks>
        /// index从0开始
        /// </remarks>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static byte SetBit(byte b, int index) { return (byte)(b | (1 << index)); }

        /// <summary>
        /// 将第index位设为0
        /// </summary>
        /// <remarks>
        /// index从0开始
        /// </remarks>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static byte ClearBit(byte b, int index) { return (byte)(b & (byte.MaxValue - (1 << index))); }

        /// <summary>
        /// 将第index位取反
        /// </summary>
        /// <remarks>
        /// index从0开始
        /// </remarks>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static byte ReverseBit(byte b, int index) { return (byte)(b ^ (byte)(1 << index)); }
    }
}
