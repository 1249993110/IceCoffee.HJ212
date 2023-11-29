using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.HJ212.Models
{
    /// <summary>
    /// 命令编码
    /// </summary>
    public enum CommandNumber
    {
        /// <summary>
        /// 心跳包
        /// </summary>
        HeartbeatPackage = 1062,

        /// <summary>
        /// 工控机向上位机上传实时数据
        /// </summary>
        UploadRealTimeData = 2011,

        /// <summary>
        /// 工控机向上位机上传分钟数据
        /// </summary>
        UploadMinuteData = 2051,

        /// <summary>
        /// 工控机向上位机上传小时数据
        /// </summary>
        UploadHourlyData = 2061,

        /// <summary>
        /// 工控机向上位机上传日数据
        /// </summary>
        UploadDailyData = 2031,

        /// <summary>
        /// 上位机向工控机返回应答
        /// </summary>
        DataResponse = 9014   
    }
}
