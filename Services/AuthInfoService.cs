using QuickSilver.Core.Interfaces;
using QuickSilver.Core.Models;

namespace QuickSilver.Core.Services
{
    public class AuthInfoService : IAuthInfoService
    {
		public AuthInfoService ()
		{
			Auth = new AuthInfo();
		}
				
        public AuthInfo Auth { get; private set; }
    }
}