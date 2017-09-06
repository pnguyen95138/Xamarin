using System;
using System.ServiceModel;
using QuickSilver.Core.Interfaces;
using QuickSilver.Core.Models;
using QuickSilver.Core.Services.wcf;

namespace QuickSilver.Core.Services
{
    public class RequestService : IRequestService
    {
        public void BeginAuthenticate(AuthInfo auth, Action<AuthenticateCompletedEventArgs> onSuccess)
        {
            var request = CreateClsRequest(auth);

            var service = CreateService();
            service.AuthenticateCompleted += (s, e) => onSuccess(e);
            service.AuthenticateAsync(request);
        }

        public void BeginGetListOfAvailableStoreReportsForUser(AuthInfo auth, Action<GetListOfAvailableStoreReportsForUserCompletedEventArgs> onSuccess)
        {
            var request = CreateClsRequest(auth);

            var service = CreateService();
            service.GetListOfAvailableStoreReportsForUserCompleted += (s, e) => onSuccess(e);
            service.GetListOfAvailableStoreReportsForUserAsync(request);
        }

        public void BeginGetListOfAvailableCompanyReportsForUser(AuthInfo auth, Action<GetListOfAvailableCoStructureReportsForUserCompletedEventArgs> onSuccess)
        {
            var request = CreateClsRequest(auth);

            var service = CreateService();
            service.GetListOfAvailableCoStructureReportsForUserCompleted += (s, e) => onSuccess(e);
            service.GetListOfAvailableCoStructureReportsForUserAsync(request);
        }

        public void BeginGetReport(AuthInfo auth, string reportCode, Action<GetReportCompletedEventArgs> onSuccess)
        {
            var request = new clsRequestWithPayloadOfstring()
            {
                User = auth.UserName,
                Password = auth.Password,
                PasswordEncrypted = true,
                Terminal = String.Empty,
                Trancode = null,
                Timestamp = DateTime.UtcNow,
                Payload = reportCode
            };

            var service = CreateService();
            service.GetReportCompleted += (s, e) => onSuccess(e);
            service.GetReportAsync(request);
        }

        private RetailHubContractClient CreateService()
        {
            var binding = new BasicHttpBinding()
            {
                Name = "basicHttpBinding",
                MaxReceivedMessageSize = 67108864,
            };
            binding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
            {
                MaxArrayLength = 2147483646,
                MaxStringContentLength = 5242880,
            };
            var timeout = new TimeSpan(0, 1, 0);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            binding.TransferMode = TransferMode.Buffered;
            binding.MessageEncoding = WSMessageEncoding.Text;

            var service = new RetailHubContractClient(binding, new EndpointAddress("http://retailhub1.mi9retail.com/MI9-WCF-Test/RetailHubService.svc/basicHttp"));
            return service;
        }

        private static clsRequest CreateClsRequest(AuthInfo auth)
        {
            var request = new clsRequest()
            {
                User = auth.UserName,
                Password = auth.Password,
                PasswordEncrypted = true,
                Terminal = String.Empty,
                Trancode = null,
                Timestamp = DateTime.UtcNow
            };
            return request;
        }
    }
}