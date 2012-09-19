using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ActionBarSample.Adapters
{
    public class NavigationListener : Java.Lang.Object, ActionBar.IOnNavigationListener
    {
        public bool OnNavigationItemSelected(int itemPosition, long itemId)
        {
            //TODO: Handle list selection

            return false;
        }
    }
}