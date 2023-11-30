using Client.API.Managers.RoleManager;
using GeneralCommon.Dtos.Admin.Role;
using GeneralCommon.Dtos.Admin.SysServer;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using GeneralCommon.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.SysManager
{
    /// <summary>
    /// 系统服务
    /// </summary>
    public class SysManager : BaseApiManager, ISysManager
    {
        private readonly string resourceName = "/api/sysServer";
        private IApiConfig _userConfig;
        public SysManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
            _userConfig = userConfig;
        }

        /// <summary>
        /// 获取服务器配置信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器配置信息")]
        public async Task<AdminCodeResult<ServerEnvInfo>> GetServerBase(bool isSingleApp)
        {
            if (isSingleApp)
            {
                try
                {
                    var server = new ServerEnvInfo
                    {
                        HostName = Environment.MachineName, // 主机名称
                        SystemOs = RuntimeInformation.OSDescription, // 操作系统
                        OsArchitecture = Environment.OSVersion.Platform.ToString() + " " + RuntimeInformation.OSArchitecture.ToString(), // 系统架构
                        ProcessorCount = Environment.ProcessorCount + " 核", // CPU核心数

                        RemoteIp =await ComputerUtil.GetIpFromOnline(), // 外网地址




                    };
                    return new AdminCodeResult<ServerEnvInfo>
                    {
                        Code = 200,
                        Result = server
                    };
                }
                catch(Exception ex)
                {
                    return new AdminCodeResult<ServerEnvInfo>();
                }
            }
            var client = CreateHttpClient();
            var url = $"{resourceName}/serverBase";


            return await client.GetAdminData<ServerEnvInfo>(url, _userConfig, null);
        }

        /// <summary>
        /// 获取服务器使用信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器使用信息")]
        public async Task<AdminCodeResult<ServerUsed>> GetServerUsed(bool isSingleApp)
        {
            if (isSingleApp)
            {
                var programStartTime = Process.GetCurrentProcess().StartTime;
                var totalMilliseconds = (DateTime.Now - programStartTime).TotalMilliseconds.ToString();
                var ts = totalMilliseconds.Contains('.') ? totalMilliseconds.Split('.')[0] : totalMilliseconds;
                var programRunTime = DateTimeUtil.FormatTime(ts.ParseToLong());

                var memoryMetrics = ComputerUtil.GetComputerInfo();
                var server= new ServerUsed
                {
                   FreeRam= memoryMetrics.FreeRam, // 空闲内存
                   UsedRam= memoryMetrics.UsedRam, // 已用内存
                   TotalRam= memoryMetrics.TotalRam, // 总内存
                   RamRate= memoryMetrics.RamRate, // 内存使用率
                   CpuRate= memoryMetrics.CpuRate, // Cpu使用率
                    StartTime = programStartTime.ToString("yyyy-MM-dd HH:mm:ss"), // 服务启动时间
                    RunTime = programRunTime, // 服务运行时间
                };
                return new AdminCodeResult<ServerUsed>
                {
                    Code = 200,
                    Result = server
                };
            }
            var client = CreateHttpClient();
            var url = $"{resourceName}/serverUsed";


            return await client.GetAdminData<ServerUsed>(url, _userConfig, null);
        }

        /// <summary>
        /// 获取服务器磁盘信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器磁盘信息")]
        public async Task<AdminCodeResult<List<DiskInfo>>> GetServerDisk(bool isSingleApp)
        {
            if (isSingleApp)
            {
                var datas= ComputerUtil.GetDiskInfos();
                return new AdminCodeResult<List<DiskInfo>>
                {
                    Code = 200,
                    Result = datas
                };
            }
            var client = CreateHttpClient();
            var url = $"{resourceName}/serverDisk";


            return await client.GetAdminData<List<DiskInfo>>(url, _userConfig, null);
        }
    }
}
