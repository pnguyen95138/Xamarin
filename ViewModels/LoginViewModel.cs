using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;
using QuickSilver.Core.Models;
using QuickSilver.Core.Services.wcf;

namespace QuickSilver.Core.ViewModels
{
    public class LoginViewModel
        : BaseViewModel
    {
        public AuthInfo Auth { get { return AuthInfoService.Auth; }}

        private bool _isLoggingIn;
        public bool IsLoggingIn
        {
            get { return _isLoggingIn; }
            set { _isLoggingIn = value; FirePropertyChanged("IsLoggingIn"); }
        }

        public IMvxCommand LoginCommand
        {
            get
            {
                return new MvxRelayCommand(DoLogin);
            }
        }

        private void DoLogin()
        {
            if (IsLoggingIn)
            {
                return;
            }

            IsLoggingIn = true;
            this.RequestService.BeginAuthenticate(this.Auth, OnLoginSuccess);
        }

        private void OnLoginSuccess(AuthenticateCompletedEventArgs authenticateCompletedEventArgs)
        {
            try
            {
                if (authenticateCompletedEventArgs.Error != null)
                {
                    this.ReportError("Login failed " + authenticateCompletedEventArgs.Error.Message);
                    return;
                }

                this.RequestNavigate<MainMenuViewModel>();
            }
            finally 
            {
                this.IsLoggingIn = false;
            }
        }
    }
}