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
    public class NavigationSpinnerAdapter : BaseAdapter
    {
        private List<Java.Lang.Object> _spinnerItems;
        private LayoutInflater _layoutInflater;

        public NavigationSpinnerAdapter(Context context)
        {
            _spinnerItems = new List<Java.Lang.Object>();

            // Create java strings for this sample.
            // This saves a bit on JNI handles.
            _spinnerItems.Add(new Java.Lang.String("Sample item 1"));
            _spinnerItems.Add(new Java.Lang.String("Sample item 2"));
            _spinnerItems.Add(new Java.Lang.String("Sample item 3"));

            // Retrieve the layout inflater from the provided context
            _layoutInflater = LayoutInflater.FromContext(context);
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return _spinnerItems[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            // Try to reuse views as much as possible.
            // It is alot faster than inflating new views all the time
            // and it saves quite a bit on memory usage aswell.
            if (view == null)
            {
                // inflate a new layout for the view.
                view = _layoutInflater.Inflate(Resource.Layout.SpinnerItem, parent, false);
            }

            var textView = view.FindViewById<TextView>(Resource.Id.DisplayTextLabel);
            textView.Text = _spinnerItems[position].ToString();

            return view;
        }

        public override int Count
        {
            get { return _spinnerItems.Count; }
        }
    }
}