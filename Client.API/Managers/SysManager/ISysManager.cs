using XT.Common.Dtos.Admin.SysServer;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.SysManager
{
    public interface ISysManager: IApiManager
    {
        /// <summary>
        /// 获取服务器磁盘信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器磁盘信息")]
        Task<AdminCodeResult<List<DiskInfo>>> GetServerDisk(bool isSingleApp);
        /// <summary>
        /// 获取服务器使用信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器使用信息")]
        Task<AdminCodeResult<ServerUsed>> GetServerUsed(bool isSingleApp);


        /// <summary>
        /// 获取服务器配置信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器配置信息")]
        Task<AdminCodeResult<ServerEnvInfo>> GetServerBase(bool isSingleApp);
    }
}
