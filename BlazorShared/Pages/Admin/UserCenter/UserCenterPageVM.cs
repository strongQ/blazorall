using BlazorShared.Helper;
using Client.API.Managers.UserManager;
using XT.Common.Dtos.Admin.User;
using XT.Common.Interfaces;
using Masa.Blazor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Pages.Admin.UserCenter
{
    public class UserCenterPageVM: BaseVM
    {
        private readonly IUserManager _userManager;
       

        public AddUserInput SelectUser { get; set; }=new AddUserInput();

        public ChangePwdInput ChangePwd { get; set; } = new ChangePwdInput();

        public string ConfirmPwd { get; set; }
        public UserCenterPageVM(IUserManager userManager,IPopupService popupService):base(popupService) 
        {
            
            _userManager = userManager;       
        }

        public override async Task InitData()
        {
            SelectUser = (await _userManager.GetBaseInfo()).GetResult();
            ResetPwd();
            return;
        }
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <returns></returns>
        public async Task SaveUser()
        {
           var admin= await _userManager.UpdateBaseInfo(SelectUser);
            admin.ShowMessage(PopupService);
        }
        /// <summary>
        /// 保存密码
        /// </summary>
        /// <returns></returns>
        public async Task SavePwd()
        {
            if(ChangePwd.PasswordNew!=ConfirmPwd)
            {
               await PopupService.EnqueueSnackbarAsync("两次密码不一致", BlazorComponent.AlertTypes.Warning);
            }
            var admin=await _userManager.ChangePwd(ChangePwd);
            admin.ShowMessage(PopupService);

        }
        /// <summary>
        /// 重置密码
        /// </summary>
        public void ResetPwd()
        {
            ChangePwd = new ChangePwdInput();
            ConfirmPwd=string.Empty;
        }
    }
}
