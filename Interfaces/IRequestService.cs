using System;
using QuickSilver.Core.Models;
using QuickSilver.Core.Services.wcf;

namespace QuickSilver.Core.Interfaces
{
    public interface IRequestService
    {
        void BeginAuthenticate(AuthInfo auth, Action<AuthenticateCompletedEventArgs> onSuccess);
        void BeginGetListOfAvailableCompanyReportsForUser(AuthInfo auth, Action<GetListOfAvailableCoStructureReportsForUserCompletedEventArgs> onSuccess);
        void BeginGetListOfAvailableStoreReportsForUser(AuthInfo auth, Action<GetListOfAvailableStoreReportsForUserCompletedEventArgs> onSuccess);
        void BeginGetReport(AuthInfo auth, string reportCode, Action<GetReportCompletedEventArgs> onSuccess);
    }
}