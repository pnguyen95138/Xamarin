using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;

namespace QuickSilver.Core.ViewModels
{
    public class MainMenuViewModel
        : BaseViewModel
    {
        public IMvxCommand ViewStoreReportsCommand
        {
            get
            {
                return new MvxRelayCommand(DoViewStoreReports);
            }
        }

        private void DoViewStoreReports()
        {
            this.RequestNavigate<StoreReportListViewModel>();
        }

        public IMvxCommand ViewCompanyReportsCommand
        {
            get
            {
                return new MvxRelayCommand(DoViewCompanyReports);
            }
        }

        private void DoViewCompanyReports()
        {
            this.RequestNavigate<CompanyReportListViewModel>();
        }
    }
}