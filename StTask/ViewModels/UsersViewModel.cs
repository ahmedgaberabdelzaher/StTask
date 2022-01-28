using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using StTask.Model;
using StTask.Services.Interface;

namespace StTask.ViewModels
{
    public class UsersViewModel:BindableBase
    {
        ObservableCollection<User> usersLst=new ObservableCollection<User>();
       public ObservableCollection<User> UsersLst
        {
            get { return usersLst;  }
            set { usersLst = value; RaisePropertyChanged(); }
        }
        User selectedUser=null;
        public User SelectedUser { get {return selectedUser; } set { selectedUser = value; RaisePropertyChanged(); } }
        IUserService _userService;
        INavigationService _navigationService;
        public UsersViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
        }
        public DelegateCommand FetchUsersDataCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await FetchUsersData();
                  
                });
            }
        }
        public DelegateCommand UserSelctionChangedCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {

                    if (SelectedUser!=null)
                    {
                        var navParameters = new NavigationParameters();
                        navParameters.Add("user", SelectedUser);
                        await _navigationService.NavigateAsync("UserProfile",navParameters);
                        SelectedUser = null;
                    }
                });
            }
        }
        public async Task FetchUsersData()
        {
            try
            {
                var result =await _userService.GetUsersData();
                
                if (result.Item2)
                {
                    var users= result.Item1.Data;
                    UsersLst = users;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
