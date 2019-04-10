using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UsersLocal.Models;

namespace UsersLocal.Models
{
    public class UserDBHelper:SQLiteOpenHelper
    {
        private const string APP_DATABASENAME = "User.db";
        private const int APP_DATABASE_VERSION = 1;

        public UserDBHelper(Context ctx) :
            base(ctx, APP_DATABASENAME, null, APP_DATABASE_VERSION)
        {

        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(@"CREATE TABLE IF NOT EXISTS User(
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Firstname TEXT NOT NULL,
                            Lastname  TEXT NOT NULL,
                            Address   TEXT NULL,
                            Email TEXT)");

          
            
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS User");
            OnCreate(db);
        }

        //Retrive All Contact Details
        public IList<User> GetAllUsers()
        {

            SQLiteDatabase db = this.ReadableDatabase;

            ICursor c = db.Query("User", new string[] { "Id", "Firstname", "Lastname", "Address", "Email" }, null, null, null, null, null);

            var users = new List<User>();

            while (c.MoveToNext())
            {
                users.Add(new User
                {
                    Id = c.GetInt(0),
                    Firstname = c.GetString(1),
                    Lastname = c.GetString(2),
                    Address = c.GetString(3),
                    Email = c.GetString(4)
                });
            }

            c.Close();
            db.Close();

            return users;
        }


        //Retrive All Contact Details
        public IList<User> GetUsersByName(string nameToSearch)
        {

            SQLiteDatabase db = this.ReadableDatabase;

            ICursor c = db.Query("User", new string[] { "Id", "Firstname", "Lastname", "Address", "Email" }, "upper(Firstname) LIKE ?", new string[] { "%" + nameToSearch.ToUpper() + "%" }, null, null, null, null);

            var users = new List<User>();

            while (c.MoveToNext())
            {
                users.Add(new User
                {
                    Id = c.GetInt(0),
                    Firstname = c.GetString(1),
                    Lastname = c.GetString(2),
                    Address = c.GetString(3),
                    Email = c.GetString(4)
                });
            }

            c.Close();
            db.Close();

            return users;
        }

        //Add New Contact
        public void AddNewUser(User userinfo)
        {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues vals = new ContentValues();
            vals.Put("Firstname", userinfo.Firstname);
            vals.Put("Lastname", userinfo.Lastname);
            vals.Put("Address", userinfo.Address);
            vals.Put("Email", userinfo.Email);
            db.Insert("User", null, vals);
        }

        //Get contact details by contact Id
        public ICursor getUserById(int id)
        {
            SQLiteDatabase db = this.ReadableDatabase;
            ICursor res = db.RawQuery("select * from User where Id=" + id + "", null);
            return res;
        }

        //Update Existing contact
        public void UpdateUser(User useritem)
        {
            if (useritem == null)
            {
                return;
            }

            //Obtain writable database
            SQLiteDatabase db = this.WritableDatabase;

            //Prepare content values
            ContentValues vals = new ContentValues();
            vals.Put("Firstname", useritem.Firstname);
            vals.Put("Lastname", useritem.Lastname);
            vals.Put("Address", useritem.Address);
            vals.Put("Email", useritem.Email);


            ICursor cursor = db.Query("User",
                    new String[] { "Id", "Firstname", "Lastname", "Address", "Email" }, "Id=?", new string[] { useritem.Id.ToString() }, null, null, null, null);

            if (cursor != null)
            {
                if (cursor.MoveToFirst())
                {
                    // update the row
                    db.Update("User", vals, "Id=?", new String[] { cursor.GetString(0) });
                }

                cursor.Close();
            }

        }


        //Delete Existing contact
        public void DeleteUser(string Id)
        {
            if (Id == null)
            {
                return;
            }

            //Obtain writable database
            SQLiteDatabase db = this.WritableDatabase;

            ICursor cursor = db.Query("User",
                    new String[] { "Id", "Firstname", "Lastname", "Address", "Email" }, "Id=?", new string[] { Id }, null, null, null, null);

            if (cursor != null)
            {
                if (cursor.MoveToFirst())
                {
                    // update the row
                    db.Delete("User", "Id=?", new String[] { cursor.GetString(0) });
                }

                cursor.Close();
            }

        }



    }
}
