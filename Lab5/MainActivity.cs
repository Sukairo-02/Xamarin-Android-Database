using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Database.Sqlite;
using Android.Content;
using System;
using Lab5.Resources.model;
using System.Collections.Generic;
using Lab5.Resources.DataHelper;
using Lab5.Resources;
using Android.Util;

namespace Lab5
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ListView dataList;
        List<DataModel> dataSource = new List<DataModel>();
        DataBase db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            db = new DataBase();
            db.CreateDatabase();

            dataList = FindViewById<ListView>(Resource.Id.listView);

            EditText editName = FindViewById<EditText>(Resource.Id.editName);
            EditText editAge = FindViewById<EditText>(Resource.Id.editAge);
            EditText editEmail = FindViewById<EditText>(Resource.Id.editEmail);
            TextView idText = FindViewById<TextView>(Resource.Id.viewId);

            Button addBtn = FindViewById<Button>(Resource.Id.btnAdd);
            Button editBtn = FindViewById<Button>(Resource.Id.btnEdit);
            Button deleteBtn = FindViewById<Button>(Resource.Id.btnDelete);

            //Initialize adapter
            ListViewAdapter adapter = new ListViewAdapter(this, dataSource);
            dataList.Adapter = adapter;

            //Load data to form
            LoadData();

            //Assign events
            addBtn.Click += delegate
            {
                if (editName.Text.ToString().Length > 0 && editAge.Text.ToString().Length > 0 && editEmail.Text.ToString().Length > 0)
                {
                    DataModel data = new DataModel()
                    {
                        Name = editName.Text,
                        Age = int.Parse(editAge.Text),
                        Email = editEmail.Text
                    };
                    db.Insert(data);
                    LoadData();
                    editName.Text = "";
                    editAge.Text = "";
                    editEmail.Text = "";
                    idText.Text = GetString(Resource.String.id);
                }
                else
                {
                    Toast.MakeText(this, Resource.String.nullFieldsErr, ToastLength.Long).Show();
                }

            };

            editBtn.Click += delegate
            {
                int idNum;
                if (int.TryParse(idText.Text, out idNum))
                {
                    if (editName.Text.ToString().Length > 0 && editAge.Text.ToString().Length > 0 && editEmail.Text.ToString().Length > 0)
                    {
                        DataModel data = new DataModel()
                        {
                            Id = idNum,
                            Name = editName.Text,
                            Age = int.Parse(editAge.Text),
                            Email = editEmail.Text
                        };
                        db.Update(data);
                        LoadData();
                        editName.Text = "";
                        editAge.Text = "";
                        editEmail.Text = "";
                        idText.Text = GetString(Resource.String.id);
                    }
                    else
                    {
                        Toast.MakeText(this, Resource.String.nullFieldsErr, ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, Resource.String.nullIdErr, ToastLength.Long).Show();
                }
            };

            deleteBtn.Click += delegate
            {
                int idNum;
                if (int.TryParse(idText.Text, out idNum))
                {
                    DataModel data = new DataModel()
                    {
                        Id = idNum,
                        Name = editName.Text,
                        Age = int.Parse(editAge.Text),
                        Email = editEmail.Text
                    };
                    db.Delete(data);
                    LoadData();
                    editName.Text = "";
                    editAge.Text = "";
                    editEmail.Text = "";
                    idText.Text = GetString(Resource.String.id);
                }
                else
                {
                    Toast.MakeText(this, Resource.String.nullIdErr, ToastLength.Long).Show();
                }
            };

            dataList.ItemClick += (o, e) =>
            {
                if (e != null)
                {
                    //Transfer data to edits
                    TextView txtName = e.View.FindViewById<TextView>(Resource.Id.textView1);
                    TextView txtAge = e.View.FindViewById<TextView>(Resource.Id.textView2);
                    TextView txtEmail = e.View.FindViewById<TextView>(Resource.Id.textView3);
                    TextView idTextLv = e.View.FindViewById<TextView>(Resource.Id.textView4);
                    editName.Text = txtName.Text;
                    editAge.Text = txtAge.Text;
                    editEmail.Text = txtEmail.Text;
                    idText.Text = idTextLv.Text;
                }
            };
        }
        private void LoadData()
        {
            dataSource = db.SelectTable();

            ListViewAdapter adapter = new ListViewAdapter(this, dataSource);
            dataList.Adapter = adapter;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}