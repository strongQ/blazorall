using BlazorComponent;
using BlazorShared.Pages.Admin.User;
using Client.API.Managers.RoleManager;
using Client.API.Managers.UserManager;
using XT.Common.Interfaces;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorShared.Pages.Admin.UserCenter
{
    public partial class UserCenterPage
    {
        private bool _show = false;
        private bool _showBirth = false;
        private DateOnly _birthDate = DateOnly.FromDateTime(DateTime.Now);

        private bool _show1= false;
        private bool _show2= false;
        private bool _show3= false;
        /// <summary>
        /// 选中的tab
        /// </summary>
        private StringNumber _tab = 1;
        public UserCenterPageVM PageVM { get; set; }
        public UserCenterPage()
        {

        }

       

        [Inject]
        IPopupService PopupService { get; set; }

        [Inject]
        public IUserManager UserManager { get; set; }

        [Inject]
        public IRoleManager RoleManager { get; set; }
      



        protected async override Task OnInitializedAsync()
        {
            PageVM = new UserCenterPageVM( UserManager, PopupService);
            PageVM.RefreshEvent -= _pageVM_RefreshEvent;
            PageVM.RefreshEvent += _pageVM_RefreshEvent;
            await PageVM.InitData();
           
            await base.OnInitializedAsync();
        }

        private void _pageVM_RefreshEvent(object? sender, EventArgs e)
        {
            this.StateHasChanged();
        }
    }
}
