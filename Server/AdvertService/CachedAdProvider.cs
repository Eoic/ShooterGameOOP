using Server.AdvertSerivce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AdvertService
{
    public class CachedAdProvider : IAdProvider
    {
        private IAdProvider AdProvider;
        private string CachedAd;

        public CachedAdProvider(IAdProvider adProvider)
        {
                AdProvider = adProvider;
        }

        public string GetAd()
        {
            if (CachedAd == "")
            {
                CachedAd = AdProvider.GetAd();
            }

            return CachedAd;
        }
    }
}