using System;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using StTask.Services.Class;
using StTask.Services.Interface;
using StTask.View;
using StTask.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StTask
{
    public partial class App : PrismApplication
    {
        public App() { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/Registration");

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Registration,RegistrationViewModel>();
            containerRegistry.RegisterForNavigation<UsersPage, UsersViewModel>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.RegisterForNavigation<UserProfile, UserProfileViewModel>();

        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
