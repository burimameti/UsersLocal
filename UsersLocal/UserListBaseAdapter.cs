using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using UsersLocal.Models;


namespace UsersLocal
{
    public partial class UserListBaseAdapter : BaseAdapter<User>
    {
        IList<User> userListArrayList;
        private LayoutInflater mInflater;
        private Context activity;
        public UserListBaseAdapter(Context context, IList<User> results)
        {
            this.activity = context;
            userListArrayList = results;
            mInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
        }
        public override int Count
        {
            get { return userListArrayList.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override User this[int position]
        {
            get { return userListArrayList[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView btnDelete;
            UserViewHolder holder = null;
            if (convertView == null)
            {
                convertView = mInflater.Inflate(Resource.Layout.list_row_user_list, null);
                holder = new UserViewHolder();
                holder.txtFirstname = convertView.FindViewById<TextView>(Resource.Id.lr_Fname);
                holder.txtLastname = convertView.FindViewById<TextView>(Resource.Id.lr_lname);
                holder.txtAddress = convertView.FindViewById<TextView>(Resource.Id.lr_address);
                holder.txtEmail = convertView.FindViewById<TextView>(Resource.Id.lr_email);
                btnDelete = convertView.FindViewById<ImageView>(Resource.Id.lr_deleteBtn);
                btnDelete.Click += (object sender, EventArgs e) =>
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(activity);
                    AlertDialog confirm = builder.Create();
                    confirm.SetTitle("Confirm Delete");
                    confirm.SetMessage("Are you sure delete?");
                    confirm.SetButton("OK", (s, ev) =>
                    {
                        var poldel = (int)((sender as ImageView).Tag);
                        string id = userListArrayList[poldel].Id.ToString();
                        string fname = userListArrayList[poldel].Firstname;
                        userListArrayList.RemoveAt(poldel);
                        DeleteSelectedUser(id);
                        NotifyDataSetChanged();
                        Toast.MakeText(activity, "User Deeletd Successfully", ToastLength.Short).Show();
                    });
                    confirm.SetButton2("Cancel", (s, ev) =>
                    {
                    });

                    confirm.Show();
                };

                convertView.Tag = holder;
                btnDelete.Tag = position;
            }
            else
            {
                btnDelete = convertView.FindViewById<ImageView>(Resource.Id.lr_deleteBtn);
                holder = convertView.Tag as UserViewHolder;
                btnDelete.Tag = position;
            }
            holder.txtFirstname.Text = userListArrayList[position].Firstname.ToString();
            holder.txtLastname.Text = userListArrayList[position].Lastname.ToString();
            holder.txtAddress.Text = userListArrayList[position].Address.ToString();
            holder.txtEmail.Text = userListArrayList[position].Email.ToString();
            if (position % 2 == 0)
            {
                convertView.SetBackgroundResource(Resource.Drawable.list_selector);
            }
            else
            {
                convertView.SetBackgroundResource(Resource.Drawable.list_selector_alternate);
            }

            return convertView;
        }
        public IList<User> GetAllData()
        {
            return userListArrayList;
        }
        public class UserViewHolder : Java.Lang.Object
        {
            public TextView txtFirstname { get; set; }
            public TextView txtLastname{ get; set; }
            public TextView txtAddress { get; set; }
            public TextView txtEmail { get; set; }
        }
        private void DeleteSelectedUser(string Id)
        {
            UserDBHelper _db = new UserDBHelper(activity);
            _db.DeleteUser(Id);
        }

    }
}