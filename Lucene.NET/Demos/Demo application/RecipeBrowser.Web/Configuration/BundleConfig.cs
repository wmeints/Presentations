using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RecipeBrowser.Web.Configuration
{
    public class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            var scriptBundle = new Bundle("~/Application/Scripts", new JsMinify())
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/knockout-{version}.js")
                .Include("~/Scripts/knockout.mapping-latest.js")
                .Include("~/Scripts/amplify.js")
                .Include("~/Scripts/sammy-{version}.js")
                .Include("~/Scripts/require.js")
                .Include("~/Scripts/viewmodels/*.js")
                .Include("~/Scripts/common/*.js");

            bundles.Add(scriptBundle);
        }
    }
}