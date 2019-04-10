using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using UsersLocal.Models;


namespace UsersLocal
{
    [Activity(Label = "User List", MainLauncher = false, Icon = "@drawable/icon")]
    public class UserActivity : Activity
    {
        Button btnAdd, btnSearch;
        EditText txtSearch;
        ListView lv;
        IList<User> listItems = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.UserDetail);
            btnAdd = FindViewById<Button>(Resource.Id.contactList_btnAdd);
            btnSearch = FindViewById<Button>(Resource.Id.contactList_btnSearch);
            txtSearch = FindViewById<EditText>(Resource.Id.contactList_txtSearch);
            lv = FindViewById<ListView>(Resource.Id.contactList_listView);
            btnAdd.Click += delegate
            {
                var activityAddEdit = new Intent(this, typeof(AddEditActivity));
                StartActivity(activityAddEdit);

            };
            btnSearch.Click += delegate
            {
                LoadUserInList();
            };

            LoadUserInList();
        }
        private void LoadUserInList()
        {
            UserDBHelper dbVals = new UserDBHelper(this);
            if (txtSearch.Text.Trim().Length < 1)
            {
                listItems = dbVals.GetAllUsers();
            }
            else
            {
                listItems = dbVals.GetUsersByName(txtSearch.Text.Trim());
            }
            lv.Adapter = new UserListBaseAdapter(this, listItems);
            lv.ItemLongClick += lv_ItemLongClick;
        }
        private void lv_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            User o = listItems[e.Position];
            //  Toast.MakeText(this, o.Id.ToString(), ToastLength.Long).Show();
            var activityAddEdit = new Intent(this, typeof(AddEditActivity));
            activityAddEdit.PutExtra("Id", o.Id.ToString());
            activityAddEdit.PutExtra("FirstName", o.Firstname);
            StartActivity(activityAddEdit);
        }
    }
}

