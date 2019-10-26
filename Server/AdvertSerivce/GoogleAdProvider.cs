using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AdvertSerivce
{
    public class GoogleAdProvider
    {
        public GoogleAdObject GetGoogleAd()
        {
            return new GoogleAdObject("New", "Cheap", "Very good item");
        }
    }

    public class GoogleAdObject
    {
        public string title;
        public string subtitle;
        public string text;

        public GoogleAdObject(string title, string subtitle, string text)
        {
            this.title = title;
            this.subtitle = subtitle;
            this.text = text;
        }
    }
}