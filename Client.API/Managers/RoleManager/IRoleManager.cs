using XT.Common.Dtos.Admin;
using XT.Common.Dtos.Admin.Menu;
using XT.Common.Dtos.Admin.Org;
using XT.Common.Dtos.Admin.Role;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.RoleManager
{
    public interface IRoleManager: IApiManager
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        Task<AdminCodeResult<List<RoleOutput>>> GetList();
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<SqlSugarPagedList<SysRole>>> GetPage(PageRoleInput input);

        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<AddOrgInput>>> GetOrgPage(OrgInput input);

      
        /// <summary>
        /// 根据角色Id获取机构Id集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<long>>> GetRoleOrgIdList(RoleInput input);

        /// <summary>
        /// 授权数据范围
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> GrantDataScope(RoleOrgInput input);
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<AddMenuInput>>> GetMenus(MenuInput input);
        /// <summary>
        /// 根据角色Id获取菜单Id集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<long>>> GetMenuIdList(RoleInput input);

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> AddRole(AddRoleInput input);
        
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> UpdateRole(UpdateRoleInput input);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> DeleteRole(DeleteRoleInput input);
    }
}
