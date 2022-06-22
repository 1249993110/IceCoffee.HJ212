using IceCoffee.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// CP指令
    /// </summary>
    public class CpCommand
    {
        public ResponseCode ExeRtn { get; set; }

        /// <summary>
        /// 数据时间信息
        /// </summary>
        public DateTime DataTime { get; set; }

        /// <summary>
        /// 污染物信息
        /// </summary>
        public List<PollutantInfo> PollutantInfo { get; set; }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public static CpCommand Parse(string cp)
        {
            try
            {
                var cpCommand = new CpCommand();

                cpCommand.PollutantInfo = new List<PollutantInfo>();

                cpCommand.DataTime = DateTime.ParseExact(cp.GetMidStr("DataTime=", ";"), "yyyyMMddHHmmss", null);

                cp = cp.Substring(24);
                foreach (string project in cp.Split(new char[';'], StringSplitOptions.RemoveEmptyEntries))
                {
                    var pollutantInfo = new PollutantInfo();

                    string[] classes = project.Split(',');
                    foreach (string @class in classes)
                    {
                        string[] keyValue = @class.Split('=');
                        string key = keyValue[0];
                        string value = keyValue[1];

                        string[] factorCodeType = key.Split('-');
                        string factorCode = factorCodeType[0];
                        string type = factorCodeType[1];

                        pollutantInfo.FactorCode = (FactorCode)Enum.Parse(typeof(FactorCode), factorCode);

                        switch (type)
                        {
                            case nameof(Models.PollutantInfo.Rtd):
                                if (string.IsNullOrEmpty(value) == false && decimal.TryParse(value, out decimal rtd))
                                {
                                    pollutantInfo.Rtd = rtd;
                                }
                                break;
                            case nameof(Models.PollutantInfo.Avg):
                                if (string.IsNullOrEmpty(value) == false && decimal.TryParse(value, out decimal avg))
                                {
                                    pollutantInfo.Avg = avg;
                                }
                                break;
                            case nameof(Models.PollutantInfo.Max):
                                if (string.IsNullOrEmpty(value) == false && decimal.TryParse(value, out decimal max))
                                {
                                    pollutantInfo.Max = max;
                                }
                                break;
                            case nameof(Models.PollutantInfo.Min):
                                if (string.IsNullOrEmpty(value) == false && decimal.TryParse(value, out decimal min))
                                {
                                    pollutantInfo.Min = min;
                                }
                                break;
                            case nameof(Models.PollutantInfo.Cou):
                                if (string.IsNullOrEmpty(value) == false && decimal.TryParse(value, out decimal cou))
                                {
                                    pollutantInfo.Cou = cou;
                                }
                                break;
                            case nameof(Models.PollutantInfo.Flag):
                                pollutantInfo.Flag = (InstrumentationDataFlag)Enum.Parse(typeof(InstrumentationDataFlag), value);
                                break;
                            default:
                                throw new Exception("无效的CP指令字段名");
                        }
                    }

                    cpCommand.PollutantInfo.Add(pollutantInfo);
                }

                return cpCommand;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CpCommand.Parse", ex);
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return "ExeRtn=" + (int)ExeRtn;
        }
    }
}
