namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 拆分包及应答标志
    /// </summary>
    public class PackageFlag
    {
        public byte V5 => GetBit(Value, 7);
        public byte V4 => GetBit(Value, 6);
        public byte V3 => GetBit(Value, 5);
        public byte V2 => GetBit(Value, 4);
        public byte V1 => GetBit(Value, 3);
        public byte V0 => GetBit(Value, 2);

        /// <summary>
        /// 是否有数据包序号：1 - 数据包中包含包号和总包数两部分，0 - 数据包中不包含包号和总包数两部分
        /// </summary>
        public byte D => GetBit(Value, 1);

        /// <summary>
        /// 命令是否应答：1－应答，0－不应答
        /// </summary>
        public byte A => GetBit(Value, 0);

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
        /// Flag值
        /// </summary>
        public byte Value { get; private set; }

        public PackageFlag(byte flag)
        {
            Value = flag;
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
