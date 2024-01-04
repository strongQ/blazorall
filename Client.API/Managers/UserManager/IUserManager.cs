using XT.Common.Dtos.Admin;
using XT.Common.Dtos.Admin.Pos;
using XT.Common.Dtos.Admin.Role;
using XT.Common.Dtos.Admin.User;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client.API.Managers.UserManager
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IUserManager: IApiManager
    {
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<AdminCodeResult<SqlSugarPagedList<AddUserInput>>> GetUserPage(PageUserInput input);

        /// <summary>
        /// 获取用户拥有角色集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [DisplayName("获取用户拥有角色集合")]
        Task<AdminCodeResult<List<long>>> GetOwnRoleList(long userId);

        /// <summary>
        /// 获取用户扩展机构集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [DisplayName("获取用户扩展机构集合")]
        Task<AdminCodeResult<List<SysUserExtOrg>>> GetOwnExtOrgList(long userId);
        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("获取职位列表")]
        Task<AdminCodeResult<List<AddPosInput>>> GetPosList(PosInput input);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> AddUser(AddUserInput input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> UpdateUser(UpdateUserInput input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> DeleteUser(DeleteUserInput input);

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        Task<AdminCodeResult<int>> SetStatus(UserInput userInput);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("重置用户密码")]
        Task<AdminCodeResult<string>> ResetPwd(ResetPwdUserInput input);

        /// <summary>
        /// 查看用户基本信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("查看用户基本信息")]
        Task<AdminCodeResult<AddUserInput>> GetBaseInfo();

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("更新用户基本信息")]
        Task<AdminCodeResult<string>> UpdateBaseInfo(AddUserInput user);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("修改用户密码")]
        Task<AdminCodeResult<string>> ChangePwd(ChangePwdInput input);


    }
}
