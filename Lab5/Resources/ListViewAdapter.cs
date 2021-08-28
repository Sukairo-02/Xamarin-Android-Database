using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Lab5.Resources.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.Resources
{
    public class ListViewAdapter : BaseAdapter<DataModel>
    {
        private Activity activity;
        private List<DataModel> dataList;
        public ListViewAdapter(Activity activity, List<DataModel> dataList)
        {
            this.activity = activity;
            this.dataList = dataList;
        }

        public void LoadData(List<DataModel> dataSource)
        {
            dataList = dataSource;
            NotifyDataSetChanged();
        }

        public override int Count
        {
            get
            {
                return dataList.Count;
            }
        }

        public override DataModel this[int position]
        {
            get
            {
                return dataList[position];
            }
        }

        public override long GetItemId(int position)
        {
            return dataList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_dataTemplate, parent, false);

            TextView txtName = view.FindViewById<TextView>(Resource.Id.textView1);
            TextView txtAge = view.FindViewById<TextView>(Resource.Id.textView2);
            TextView txtEmail = view.FindViewById<TextView>(Resource.Id.textView3);
            TextView txtId = view.FindViewById<TextView>(Resource.Id.textView4);

            txtName.Text = dataList[position].Name;
            txtAge.Text = dataList[position].Age.ToString();
            txtEmail.Text = dataList[position].Email;
            txtId.Text = dataList[position].Id.ToString();

            return view;
        }
    }
}