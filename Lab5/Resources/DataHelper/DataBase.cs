using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using SQLiteException = SQLite.SQLiteException;
using Android.Util;
using Lab5.Resources.model;

namespace Lab5.Resources.DataHelper
{
    class DataBase
    {
        private string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool CreateDatabase()
        {
            try
            {
                using SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Database.db"));
                connection.CreateTable<DataModel>();
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }
        public List<DataModel> SelectTable()
        {
            try
            {
                using SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Database.db"));
                return connection.Table<DataModel>().ToList();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }
        public bool Delete(DataModel data)
        {
            try
            {
                using SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Database.db"));
                //connection.Query<DataModel>("DELETE FROM DataModel Where Id=?", data.Id);
                connection.Delete(data);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }
        public bool Update(DataModel data)
        {
            try
            {
                using SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Database.db"));
                connection.Query<DataModel>("UPDATE DataModel set Name=?,Age=?,Email=? Where Id=?", data.Name, data.Age, data.Email, data.Id);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }
        public bool Select(DataModel data)
        {
            try
            {
                using SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Database.db"));
                connection.Query<DataModel>("SELECT * FROM DataModel Where Id=?", data.Id);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }
        public bool Insert(DataModel item)
        {
            try
            {
                using SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Database.db"));
                connection.Insert(item);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }
    }
}