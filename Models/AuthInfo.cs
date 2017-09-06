using Cirrious.MvvmCross.ViewModels;

namespace QuickSilver.Core.Models
{
    public class AuthInfo 
        : MvxNotifyPropertyChanged
    {
        public AuthInfo()
        {
#if DEBUG
            UserName = "mgb";
            Password = "3SXXSuigD.s";
#endif
            // DEBUG
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; FirePropertyChanged("UserName"); FirePropertyChanged("IsValid"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; FirePropertyChanged("Password"); FirePropertyChanged("IsValid"); }
        }

        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password); }
        }
    }
}