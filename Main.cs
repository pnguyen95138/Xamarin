using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using QuickSilver.Core.ApplicationObjects;
using QuickSilver.Core.Interfaces;
using QuickSilver.Core.Services;

namespace QuickSilver.Core
{
    public class App
        : MvxApplication
        , IMvxServiceProducer<IMvxStartNavigation>
        , IMvxServiceProducer<IRequestService>
        , IMvxServiceProducer<IAuthInfoService>
        , IMvxServiceProducer<IErrorReporter>
        , IMvxServiceProducer<IErrorSource>
    {
        public App()
        {
            this.RegisterServiceInstance<IRequestService>(new RequestService());
            this.RegisterServiceInstance<IAuthInfoService>(new AuthInfoService());

            var startApplicationObject = new StartApplicationObject();
            this.RegisterServiceInstance<IMvxStartNavigation>(startApplicationObject);

            var errorHub = new ErrorApplicationObject();
            this.RegisterServiceInstance<IErrorReporter>(errorHub);
            this.RegisterServiceInstance<IErrorSource>(errorHub);
        }
    }
}
