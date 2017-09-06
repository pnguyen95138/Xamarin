using System;
using System.Collections.Generic;
using QuickSilver.Core.Services.wcf;

namespace QuickSilver.Core.ViewModels
{
    public class BaseReportListViewModel<T>
        : BaseViewModel
    {
        public BaseReportListViewModel()
        {
        }

        private bool _isRequesting;
        public bool IsRequesting
        {
            get { return _isRequesting; }
            set { _isRequesting = value; FirePropertyChanged("IsRequesting"); }
        }

        private IList<ItemWithCommand<T>> _reports;
        public IList<ItemWithCommand<T>> Reports
        {
            get { return _reports; }
            set { _reports = value; FirePropertyChanged("Reports"); }
        }

		private DateTime _hackTimeLastNavigated = DateTime.MinValue;
		
        protected void DoShowReportDetail(ReportInfo reportInfo)    
		{
			if (!this.IsVisible)
			{
				System.Console.WriteLine("Do Show blocked - currently hidden");
				return;
			}
			
			var timeSinceLast = DateTime.UtcNow - _hackTimeLastNavigated;
			if (timeSinceLast < TimeSpan.FromMilliseconds(1000))
			{
				// hackhackhack - something is giving us double hits right now :/
				System.Console.WriteLine("Do Show failed " + timeSinceLast);
				return;
			}
			System.Console.WriteLine("Do Show " + timeSinceLast);
			
			Console.WriteLine("Sending nav");
			_hackTimeLastNavigated = DateTime.UtcNow;
            this.RequestNavigate<ReportDetailViewModel>(new { reportCode = reportInfo.ReportCode, reportFilename = reportInfo.Filename, reportDescription = reportInfo.ReportDescription });
		}
    }
}