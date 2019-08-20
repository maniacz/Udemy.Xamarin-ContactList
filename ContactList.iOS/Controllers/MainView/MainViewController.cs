using ContactList.iOS.Controllers.MainView;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace ContactList.iOS
{
    public partial class MainViewController : UIViewController
    {
        private List<Contact> contacts;
        public MainViewController (IntPtr handle) : base (handle)
        {
            Initialize();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ContactTableView.Source = new ContactTableViewSource(ContactTableView, contacts);
        }

        private void Initialize()
        {
            contacts = new List<Contact>();

            for (int i = 1; i <= 20; i++)
            {
                contacts.Add(new Contact("My Contact " + i,
                                        "contact_email@gmail.com",
                                        "+48 123 123 123"));

            }
        }
    }
}