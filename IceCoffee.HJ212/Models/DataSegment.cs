using IceCoffee.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 数据段
    /// </summary>
    public class DataSegment
    {
        /// <summary>
        /// 默认应答系统编码
        /// </summary>
        public const string ResponseST = "91";

        /// <summary>
        /// 请求编号
        /// </summary>
        /// <remarks>
        /// yyyyMMddHHmmssZZZ 取当前系统时间， 精确到毫秒值， 用来唯一标识一次命令交互
        /// </remarks>
        public string QN { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public string ST { get; set; }

        /// <summary>
        /// 命令编码
        /// <para>详见附录 2</para>
        /// </summary>
        public CommandNumber CN { get; set; }

        /// <summary>
        /// 访问密码
        /// <para>对接时提供给各个对接站点</para>
        /// </summary>
        public string PW { get; set; }

        /// <summary>
        /// 设备唯一标识
        /// <para>对接时提供给各个对接站点</para>
        /// </summary>
        public string MN { get; set; }

        /// <summary>
        /// 拆分包及应答标志
        /// </summary>
        public PackageFlag PackageFlag { get; set; }

        /// <summary>
        /// 总包数
        /// <para>PNUM 指示本次通讯中总共包含的包数，注：不分包时可以没有本字段，与标志位有关</para>
        /// </summary>
        public int PNUM { get; set; }

        /// <summary>
        /// 包号
        /// <para>PNO 指示当前数据包的包号，注： 不分包时可以没有本字段，与标志位有关</para>
        /// </summary>
        public int PNO { get; set; }

        /// <summary>
        /// 指令
        /// <para>CP=＆＆数据区＆＆（ 详见表 5 )</para>
        /// </summary>
        public CpCommand CpCommand { get; set; }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="data"></param>
        /// <param name="unpackCacheFunc"></param>
        /// <returns></returns>
        public static DataSegment Parse(string data, Func<StringBuilder> unpackCacheFunc)
        {
            try
            {
                DataSegment dataSegment = new DataSegment();

                int outEnd;
                dataSegment.QN = data.GetMidStr("QN=", ";", out outEnd);

                if(outEnd < 0)
                {
                    outEnd = 0;
                }

                dataSegment.ST = data.GetMidStr("ST=", ";", out outEnd, outEnd);
                dataSegment.CN = (CommandNumber)int.Parse(data.GetMidStr("CN=", ";", out outEnd, outEnd));
                dataSegment.PW = data.GetMidStr("PW=", ";", out outEnd, outEnd);
                dataSegment.MN = data.GetMidStr("MN=", ";", out outEnd, outEnd);

                string packageFlag = data.GetMidStr("Flag=", ";", out outEnd, outEnd);
                if (string.IsNullOrEmpty(packageFlag) || int.TryParse(packageFlag, out _) == false || outEnd < 0)
                {
                    outEnd = 0;
                }
                else
                {
                    dataSegment.PackageFlag = PackageFlag.Parse(packageFlag);
                }

                if (dataSegment.PackageFlag != null && dataSegment.PackageFlag.D == 1)
                {
                    // 分包
                    dataSegment.PNUM = int.Parse(data.GetMidStr("PNUM=", ";", out outEnd, outEnd));
                    dataSegment.PNO = int.Parse(data.GetMidStr("PNO=", ";", out outEnd, outEnd));

                    string cp = data.GetMidStr("CP=&&", "&&", out outEnd, outEnd);
                    var cache = unpackCacheFunc.Invoke();
                    if(dataSegment.PNO == 1)// 第一个包
                    {
                        cache.Append(cp);
                    }
                    else if (dataSegment.PNUM == dataSegment.PNO)// 最后一个包
                    {
                        cache.Append(cp.Substring(23));
                        dataSegment.CpCommand = CpCommand.Parse(cache.ToString());
                        cache.Clear();
                    }
                    else// 中间的包
                    {
                        cache.Append(cp.Substring(23));// 23 - DataTime=20170920100000; 留分号
                    }
                }
                else
                {
                    string cp = data.GetMidStr("CP=&&", "&&", out outEnd, outEnd);
                    // 过滤心跳包
                    dataSegment.CpCommand = dataSegment.CN == CommandNumber.HeartbeatPackage ? new CpCommand() : CpCommand.Parse(cp);
                }

                return dataSegment;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DataSegment.Parse", ex);
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public virtual string Serialize()
        {
            return $"QN={QN};ST={ST};CN={(int)CN};PW={PW};MN={MN};Flag={PackageFlag.Serialize()};CP=&&{CpCommand.Serialize()}&&";
        }
    }
}
