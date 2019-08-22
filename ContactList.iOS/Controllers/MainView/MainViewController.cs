using ContactList.iOS.Controllers.MainView;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using ContactList.SharedProject;

namespace ContactList.iOS
{
    public partial class MainViewController : UIViewController
    {
        private List<Contact> contacts;
        public MainViewController (IntPtr handle) : base (handle)
        {
            contacts = ContactListDataSource.GetContacts();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            AutomaticallyAdjustsScrollViewInsets = false;
            ContactTableView.Source = new ContactTableViewSource(ContactTableView, contacts, this);
            ContactTableView.AllowsSelection = false;
        }

        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
            ContactTableView.ReloadData();
        }
    }
}