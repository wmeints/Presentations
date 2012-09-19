using System;
using ActionBarSample.Adapters;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ActionBarSample
{
    [Activity(Label = "@string/MainActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestWindowFeature(WindowFeatures.ActionBar);

            SetContentView(Resource.Layout.Main);
            InitializeActionBar();
        }

        private void InitializeActionBar()
        {
            // Uncomment this line to create an action bar with tabs.
            // CreateTabbedActionBar();

            // Uncomment this to create an action bar with a dropdown.
            // CreateDropDownActionBar();
        }

        #region Dropdown action bar

        private void CreateDropDownActionBar()
        {
            ActionBar.NavigationMode = ActionBarNavigationMode.List;

            ActionBar.SetListNavigationCallbacks(
                new NavigationSpinnerAdapter(this),
                new NavigationListener());

        }

        #endregion

        #region Tabbed action bar

        private void CreateTabbedActionBar()
        {
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            var homeTab = ActionBar.NewTab();

            homeTab.SetText(Resource.String.HomeTab);

            // New since 4.2.x is the possibility to add event handlers directly.
            // You no longer need to use the TabListener component.
            homeTab.TabSelected += OnHomeTabSelected;

            ActionBar.AddTab(homeTab, true);
        }

        private void OnHomeTabSelected(object sender, ActionBar.TabEventArgs e)
        {
            var alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetMessage("Home tab selected!");

            var dialog = alertDialogBuilder.Create();
            dialog.Show();
        }

        #endregion

        #region Action bar menu

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_save)
            {
                //TODO: Handle the save action

                return true;
            }

            return false;
        }

        // If you want a dropdown menu, enable this code and update the actionitems.xml file.
        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.ActionItems, menu);
        //    return true;
        //}

        #endregion

        #region Search view

        // If you want a search menu, enable this code and update the actionitems.xml file.
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SearchItems, menu);
            var searchView = (SearchView)menu.FindItem(Resource.Id.menu_search).ActionView;
            searchView.SearchClick += OnSearchClicked;

            return true;
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            //TODO: implement search action
        }

        #endregion
    }
}

