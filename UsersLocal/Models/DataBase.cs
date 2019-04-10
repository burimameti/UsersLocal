using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;
using UsersLocal.Models;
namespace UsersLocal.Models
{
    public class DataBase
    {

        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool createDataBase()
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "User.db")))
                {
                    conn.CreateTable<User>();
                    return true;

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlLiteEX", ex.Message);
                return false;
            }
        }
        public bool InsertIntoTableUsers(User user)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "User.db")))
                {
                    conn.Insert(user);
                    return true;

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlLiteEX", ex.Message);
                return false;
            }

        }

        public List<User> selectTableUsers()
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "User.db")))
                {
                    return conn.Table<User>().ToList();

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlLiteEX", ex.Message);
                return null;
            }

        }

        public bool updateTableUsers(User user)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "User.db")))
                {
                    conn.Query<User>("UPDATE User set Firstname=?,Lastame=?, Address=?,Email=? Where Id =?",user.Firstname, user.Lastname, user.Address, user.Email, user.Id);
                    return true;

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlLiteEX", ex.Message);
                return false;
            }

        }
        public bool deleteTableUser(User user)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "User.db")))
                {
                    conn.Delete(user);
                    return true;

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlLiteEX", ex.Message);
                return false;
            }

        }

        public bool selectQueryTableUser(int Id)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "User.db")))
                {
                    conn.Query<User>("SELECT * FROM User Where Id =?", Id);
                    return true;

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlLiteEX", ex.Message);
                return false;
            }

        }
    }
}