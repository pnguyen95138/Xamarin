using System.Linq;
using Cirrious.MvvmCross.Commands;
using QuickSilver.Core.Services.wcf;

namespace QuickSilver.Core.ViewModels
{
    public class StoreReportListViewModel
        : BaseReportListViewModel<StoreReportInfo>
    {
        public StoreReportListViewModel()
        {
            IsRequesting = true;
            var auth = AuthInfoService.Auth;
            RequestService.BeginGetListOfAvailableStoreReportsForUser(auth, OnListCompleted);
        }

        private void OnListCompleted(GetListOfAvailableStoreReportsForUserCompletedEventArgs args)
        {
            IsRequesting = false;
            if (args.Error != null)
            {
                this.RequestClose(this);
                ReportError("Sorry - Unable to get report " + args.Error.ToString());
                return;
            }

            Reports = args.Result.Result.Select(x => new ItemWithCommand<StoreReportInfo>(x, new MvxRelayCommand(() => DoShowReportDetail(x)))).ToList();
        }

        private void DoShowReportDetail(StoreReportInfo reportInfo)
        {
            this.RequestNavigate<ReportDetailViewModel>(new { reportCode = reportInfo.ReportCode, reportFilename = reportInfo.Filename, reportDescription = reportInfo.ReportDescription });
        }
    }
}