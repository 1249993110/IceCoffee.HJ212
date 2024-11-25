namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 拆分包及应答标志
    /// </summary>
    public class PackageFlag
    {
        public byte V5
        {
            get { return GetBit(Value, 7); }
            set { Value = value == 1 ? SetBit(Value, 7) : ClearBit(Value, 7); }
        }
        public byte V4
        {
            get { return GetBit(Value, 6); }
            set { Value = value == 1 ? SetBit(Value, 6) : ClearBit(Value, 6); }
        }
        public byte V3
        {
            get { return GetBit(Value, 5); }
            set { Value = value == 1 ? SetBit(Value, 5) : ClearBit(Value, 5); }
        }
        public byte V2
        {
            get { return GetBit(Value, 4); }
            set { Value = value == 1 ? SetBit(Value, 4) : ClearBit(Value, 4); }
        }
        public byte V1
        {
            get { return GetBit(Value, 3); }
            set { Value = value == 1 ? SetBit(Value, 3) : ClearBit(Value, 3); }
        }
        public byte V0
        {
            get { return GetBit(Value, 2); }
            set { Value = value == 1 ? SetBit(Value, 2) : ClearBit(Value, 2); }
        }

        /// <summary>
        /// 是否有数据包序号：1 - 数据包中包含包号和总包数两部分，0 - 数据包中不包含包号和总包数两部分
        /// </summary>
        public byte D
        {
            get { return GetBit(Value, 1); }
            set { Value = value == 1 ? SetBit(Value, 1) : ClearBit(Value, 1); }
        }

        /// <summary>
        /// 命令是否应答：1－应答，0－不应答
        /// </summary>
        public byte A
        {
            get { return GetBit(Value, 0); }
            set { Value = value == 1 ? SetBit(Value, 0) : ClearBit(Value, 0); }
        }

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

        public PackageFlag()
        {
        }
        public PackageFlag(byte flag)
        {
            Value = flag;
        }

        /// <summary>
        /// 获取第index位
        /// </summary>
        /// <remarks>
        /// index从0开始
        /// </remarks>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static byte GetBit(byte b, int index) 
        {
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
