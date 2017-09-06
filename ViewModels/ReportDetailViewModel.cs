using System;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Interfaces.Platform;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.ViewModels;
using QuickSilver.Core.Interfaces;
using QuickSilver.Core.Services.wcf;

namespace QuickSilver.Core.ViewModels
{
    public class ReportDetailViewModel
        : BaseViewModel
        , IMvxServiceConsumer<IMvxSimpleFileStoreService>
        , IMvxServiceConsumer<IPdfFileOpener>
    {
        private const string ThePdfFile = "CurrentReport.pdf";

        public ReportDetailViewModel(string reportCode, string reportDescription, string reportFilename)
        {
            this.ReportDescription = reportDescription;
			this.ReportFilename = reportFilename;
			
			StartRequest();
		}
		
		public void StartRequest()
		{
			if (IsRequesting || CanOpen)
			{
				// request is either already started, or the file is already available
				return;
			}
			
            IsRequesting = true;
            var auth = AuthInfoService.Auth;
            RequestService.BeginGetReport(auth, ReportFilename, OnReportAvailable);
        }

        private string _reportDescription;
        public string ReportDescription
        {
            get { return _reportDescription; }
            set { _reportDescription = value; FirePropertyChanged("ReportDescription"); }
        }

        private string _reportFilename;
        public string ReportFilename
        {
            get { return _reportFilename; }
            set { _reportFilename = value; FirePropertyChanged("ReportFilename"); }
        }

        private bool _isRequesting;
        public bool IsRequesting
        {
            get { return _isRequesting; }
            set { _isRequesting = value; FirePropertyChanged("IsRequesting"); }
        }

        private bool _canOpen;
        public bool CanOpen
        {
            get { return _canOpen; }
            set { _canOpen = value; FirePropertyChanged("CanOpen"); }
        }

        private string _reportFileUrl;
        public string ReportFileUrl
        {
            get { return _reportFileUrl; }
            set { _reportFileUrl = value; ; FirePropertyChanged("ReportFileUrl"); }
        }

        public IMvxCommand OpenFileCommand
        {
            get
            {
                return new MvxRelayCommand(DoFileOpen);
            }
        }

        private void DoFileOpen()
        {
            this.GetService<IPdfFileOpener>().Open(ReportFileUrl);
        }

        private void OnReportAvailable(GetReportCompletedEventArgs args)
        {
            IsRequesting = false;
            if (args.Error != null)
            {
                RequestClose(this);
                ReportError("Sorry - Unable to get report " + args.Error.ToString());
                return;
            }

            var file = this.GetService<IMvxSimpleFileStoreService>();
            file.WriteFile(ThePdfFile, args.Result.Result);

            ReportFileUrl = ThePdfFile;
            CanOpen = true;
        }
    }
}