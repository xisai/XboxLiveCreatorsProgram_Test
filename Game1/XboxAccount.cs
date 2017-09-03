using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xbox.Services.System;

namespace Game1
{
    public class XboxAccount
    {
        private XboxLiveUser xboxUser;

        public XboxAccount()
        {
            xboxUser = new XboxLiveUser();
        }

        public async Task<SignInStatus> LoginAsync()
        {
            var coreDispatcher = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.Dispatcher;

            var result = await xboxUser.SignInAsync(coreDispatcher);

            return (SignInStatus)result.Status;
        }

        public async Task<SignInStatus> LoginSilentlyAsync()
        {
            var coreDispatcher = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.Dispatcher;

            var result = await xboxUser.SignInSilentlyAsync(coreDispatcher);

            return (SignInStatus)result.Status;
        }

        public string Gamertag()
        {
            return xboxUser.Gamertag;
        }

        public bool IsSignedIn()
        {
            return xboxUser.IsSignedIn;
        }
    }
}
