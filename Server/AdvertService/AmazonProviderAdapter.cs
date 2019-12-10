using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AdvertSerivce
{
    public class AmazonProviderAdapter : IAdProvider
    {
        AmazonAdProvider provider;
        IAdProvider FallbackAdProvider;

        public AmazonProviderAdapter(IAdProvider fallbackAdProvider)
        {
            provider = new AmazonAdProvider();
            FallbackAdProvider = fallbackAdProvider;
        }

        public string GetAd()
        {
            if (!provider.IsAdAvailable())
            {
                return FallbackAdProvider.GetAd();
            }

            string result = "";
            foreach(string item in provider.RequestAmazonAd())
            {
                result += item;
            }
            return result;
        }
    }
}