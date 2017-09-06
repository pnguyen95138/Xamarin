using System.Linq;
using Cirrious.MvvmCross.Commands;
using QuickSilver.Core.Services.wcf;

namespace QuickSilver.Core.ViewModels
{
    public class CompanyReportListViewModel
        : BaseReportListViewModel<CompanyReportInfo>
    {
        public CompanyReportListViewModel()
        {
            IsRequesting = true;
            var auth = AuthInfoService.Auth;
            RequestService.BeginGetListOfAvailableCompanyReportsForUser(auth, OnListCompleted);
        }

        private void OnListCompleted(GetListOfAvailableCoStructureReportsForUserCompletedEventArgs args)
        {
            IsRequesting = false;
            if (args.Error != null)
            {
                this.RequestClose(this);
                ReportError("Sorry - Unable to get report " + args.Error.ToString());
                return;
            }

            Reports = args.Result.Result.Select(x => new ItemWithCommand<CompanyReportInfo>(x, new MvxRelayCommand(() => DoShowReportDetail(x)))).ToList();
        }
    }
}