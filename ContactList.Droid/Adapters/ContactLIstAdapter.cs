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

namespace ContactList.Droid
{
    public class ContactListAdapter : BaseAdapter<Contact>
    {
        private List<Contact> contacts;
        private Activity parent;

        public ContactListAdapter(List<Contact> contacts, Activity parent)
        {
            this.contacts = contacts;
            this.parent = parent;
        }
        public override Contact this[int position]
        {
            get
            {
                return contacts[position];
            }
        }

        public override int Count
        {
            get
            {
                return contacts.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ContactViewHolder viewHolder = null;

            if (convertView == null)
            {
                convertView = this.parent.LayoutInflater.Inflate(Resource.Layout.view_contact, null);

                viewHolder = new ContactViewHolder();
                viewHolder.NameTextView = convertView.FindViewById<TextView>(Resource.Id.nameTextView);
                viewHolder.PhoneNumberTextView = convertView.FindViewById<TextView>(Resource.Id.phoneNumberTextView);
                viewHolder.EmailImageView = convertView.FindViewById<ImageView>(Resource.Id.emailImageView);
                viewHolder.PhoneImageView = convertView.FindViewById<ImageView>(Resource.Id.phoneImageView);

                viewHolder.EmailImageView.Click += EmailImageView_Click;
                viewHolder.PhoneImageView.Click += PhoneImageView_Click;

                convertView.Tag = viewHolder;
            }

            if (viewHolder == null)
            {
                viewHolder = convertView.Tag as ContactViewHolder;
            }

            var contact = contacts[position];

            viewHolder.NameTextView.Text = contact.Name;
            viewHolder.PhoneNumberTextView.Text = contact.PhoneNumber;

            viewHolder.EmailImageView.Tag = position;
            viewHolder.PhoneImageView.Tag = position;

            return convertView;
        }

        private void EmailImageView_Click(object sender, EventArgs args)
        {
            var contact = contacts[(int)(sender as ImageView).Tag];

            Intent intent = new Intent(Intent.ActionSend);
            intent.SetType("plain/text");
            intent.PutExtra(Intent.ExtraEmail, new string[] { contact.Email });
            parent.StartActivity(intent);

        }

        private void PhoneImageView_Click(object sender, EventArgs args)
        {
            var contact = contacts[(int)(sender as ImageView).Tag];

            var intent = new Intent(Intent.ActionDial);
            intent.SetData(Android.Net.Uri.Parse(string.Format("tel:{0}", contact.PhoneNumber)));
            parent.StartActivity(intent);
        }
    }
}