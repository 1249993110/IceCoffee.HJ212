namespace IceCoffee.HJ212
{
    public static class Utils
    {
        /// <summary>
        /// CRC16校验
        /// </summary>
        /// <param name="str">需要校验的字符串</param>
        /// <returns>CRC16 校验码</returns>
        public static string CRC16(string str)
        {
            char[] chars = str.ToCharArray();
            int len = chars.Length;
            uint i, j, crc_reg, check;
            crc_reg = 0xFFFF;
            for (i = 0; i < len; i++)
            {
                crc_reg = (crc_reg >> 8) ^ chars[i];
                for (j = 0; j < 8; j++)
                {
                    check = crc_reg & 0x0001;
                    crc_reg >>= 1;
                    if (check == 0x0001)
                    {
                        crc_reg ^= 0xA001;
                    }
                }
            }

            return crc_reg.ToString("X2").PadLeft(4, '0');
        }
    }
}
