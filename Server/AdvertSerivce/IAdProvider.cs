using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AdvertSerivce
{
    public interface IAdProvider
    {
        String GetAd();
    }
}