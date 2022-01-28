using System;
using Prism.Mvvm;
using Prism.Navigation;
using StTask.Model;

namespace StTask.ViewModels
{
    public class UserProfileViewModel:BindableBase,INavigationAware
    {
        User selectedUser = new User();
        public User SelectedUser { get { return selectedUser; } set { selectedUser = value; RaisePropertyChanged(); } }
        BirthandGenderModel birthandGenderObject = new BirthandGenderModel(1999, (int)Gender.Male);
        public BirthandGenderModel BirthandGenderObject { get { return birthandGenderObject; } set { birthandGenderObject = value; RaisePropertyChanged(); } }

        public UserProfileViewModel()
        {
        }
        
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters!=null&&parameters.Count>0)
            {
                SelectedUser = parameters["user"] as User;
              
            }
        }
    }
}
