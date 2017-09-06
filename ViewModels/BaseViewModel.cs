using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.ViewModels;
using QuickSilver.Core.Interfaces;
using QuickSilver.Core.Services;

namespace QuickSilver.Core.ViewModels
{
    public class BaseViewModel
        : MvxViewModel
        , IMvxServiceConsumer<IAuthInfoService>
        , IMvxServiceConsumer<IRequestService>
        , IMvxServiceConsumer<IErrorReporter>
    {
        private IAuthInfoService _authInfoService;
        protected IAuthInfoService AuthInfoService
        {
            get
            {
                if (_authInfoService == null)
                    _authInfoService = this.GetService<IAuthInfoService>();
                return _authInfoService;
            }
        }

        private IRequestService _requestService;
        protected IRequestService RequestService
        {
            get
            {
                if (_requestService == null)
                    _requestService = this.GetService<IRequestService>();
                return _requestService;
            }
        }

        protected void ReportError(string text)
        {
            this.GetService<IErrorReporter>().ReportError(text);
        }
    }
}