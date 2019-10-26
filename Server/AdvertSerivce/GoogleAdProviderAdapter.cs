using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AdvertSerivce
{
    public class GoogleAdProviderAdapter : IAdProvider
    {
        GoogleAdProvider provider;
        public GoogleAdProviderAdapter()
        {
            provider = new GoogleAdProvider();
        }

        public string GetAd()
        {
            var ad = provider.GetGoogleAd();
        
            return ad.title + ad.subtitle + ad.text;
        }
    }
}