using XT.Common.Dtos.Admin.Org;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.Org
{
    public interface IOrgManager: IApiManager
    {
        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<AddOrgInput>>> GetOrgList(OrgInput input);

        /// <summary>
        /// 增加机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<bool>> AddOrg(AddOrgInput input);

        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> UpdateOrg(UpdateOrgInput input);

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> DeleteOrg(DeleteOrgInput input);
    }
}
