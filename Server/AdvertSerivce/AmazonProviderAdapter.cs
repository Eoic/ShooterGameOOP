using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AdvertSerivce
{
    public class AmazonProviderAdapter : IAdProvider
    {
        AmazonAdProvider provider;
        public AmazonProviderAdapter()
        {
            provider = new AmazonAdProvider();
        }

        public string GetAd()
        {
            string result = "";
            foreach(string item in provider.RequestAmazonAd())
            {
                result += item;
            }
            return result;
        }
    }
}