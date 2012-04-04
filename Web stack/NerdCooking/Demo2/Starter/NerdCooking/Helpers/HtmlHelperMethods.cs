using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;

namespace NerdCooking
{
    public static class HtmlHelperMethods
    {
        public static string RenderFullMemberName(string username)
        {
            ProfileBase profile = new ProfileBase();
            profile.Initialize(username,true);

            object profilePropertyValue = profile.GetPropertyValue("FullName");
            string outputValue = username;

            if(profilePropertyValue != null)
            {
                if(!String.IsNullOrEmpty(profilePropertyValue.ToString()))
                {
                    outputValue = profilePropertyValue.ToString();
                }
            }

            return outputValue;
        }


    }
}