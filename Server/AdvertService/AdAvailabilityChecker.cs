using Server.AdvertSerivce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AdvertService
{
    public class AdAvailabilityChecker : IAdProvider
    {
        private const int MinUserAge = 18;
        private IAdProvider AdProvider;
        private int UserAge;
      

        public AdAvailabilityChecker(IAdProvider adProvider, int userAge)
        {
            AdProvider = adProvider;
            UserAge = userAge;
        }

        public string GetAd()
        {
            if (UserAge >= MinUserAge)
            {
                return AdProvider.GetAd();
            }

            return "";
        }
    }
}