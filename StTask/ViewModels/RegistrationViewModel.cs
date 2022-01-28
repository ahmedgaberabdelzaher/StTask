using System;
using System.ComponentModel;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using StTask.Helper;

namespace StTask.ViewModels
{
    public class RegistrationViewModel: BindableBase
    {
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatablePair<string> Password { get; set; } = new ValidatablePair<string>();
        public ValidatableObject<string> YearOfBirth { get; set; } = new ValidatableObject<string>();
        public string FullName { get; set; }
        public void AddValidationRules()
        {
            YearOfBirth.Validations.Add(new IsValidYear<string> { ValidationMessage = "YearOfBirth Not valid" });
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email or Phone Required" });
            Email.Validations.Add(new IsValidEmailOrPhone<string>() { ValidationMessage = "Email or Phone not Valid" });
            Password.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
            Password.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Confirm Password Required" });
            Password.Validations.Add(new MatchPairValidationRule<string> { ValidationMessage = "Password and confirm password not matched" });
         
        }
        public DelegateCommand SignUpCommand
        {
            get
            {
                return new DelegateCommand(async() =>
                {
                    if (IsFormFieldsValid())
                    {
                       await _navigationService.NavigateAsync("../UsersPage");
                    }
                    else
                    {
                        try
                        {
                            var message =
                     (!Email.IsValid&& Email.Errors.Count>0 ? "\n" + Email.Errors.First() + "\n " : "")
                     + (!YearOfBirth.IsValid && YearOfBirth.Errors.Count > 0 ? YearOfBirth.Errors.First() + "\n " : "")
                     + (!Password.Item1.IsValid && Password.Item1.Errors.Count > 0 ? Password.Item1.Errors.First() + "\n " : "")
                     + (!Password.Item2.IsValid&& Password.Item2.Errors.Count>0 ? Password.Item1.Errors.First() + "\n " : "")
                     + (!Password.IsValid && Password.Errors.Count>0 ? Password.Errors.First() : ""
                     );

                            await _pageDialogServices.DisplayAlertAsync("", "Please Check Field Validations \n" + message, "Ok");

                        }
                        catch (Exception ex)
                        {

                        }
                     
                    }
                });
            }
        }
        INavigationService _navigationService;
        IPageDialogService _pageDialogServices;
        public RegistrationViewModel(INavigationService navigationService,IPageDialogService pageDialogService)
        {
            _pageDialogServices = pageDialogService;
            _navigationService = navigationService;
            AddValidationRules();
        }
        bool IsFormFieldsValid()
        {
            return Email.Validate() && Password.Validate()&&YearOfBirth.Validate();
        }

        }
}
