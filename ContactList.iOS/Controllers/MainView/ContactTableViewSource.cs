using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ContactList.iOS.Controllers.MainView
{
    public class ContactTableViewSource : UITableViewSource
    {
        private List<Contact> contacts;
        private MainViewController parent;
        public ContactTableViewSource(UITableView table, List<Contact> contacts, MainViewController parent)
        {
            table.RegisterNibForCellReuse(ContactTableViewCell.Nib, ContactTableViewCell.Key);
            this.contacts = contacts;
            this.parent = parent;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ContactTableViewCell.Key, indexPath);

            (cell as ContactTableViewCell).Initialize(contacts[indexPath.Row]);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return contacts.Count;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            var contact = contacts[indexPath.Row];

            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                var alertController = UIAlertController.Create("Delete Contact", 
                                                        string.Format("Are you sure you want to delete {0}", contact.Name), 
                                                        UIAlertControllerStyle.Alert);

                alertController.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, delegate
                {
                    contacts.Remove(contact);
                    tableView.ReloadData();
                }));

                alertController.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Cancel, delegate{ }));

                parent.PresentViewController(alertController, true, null);
            }
            else
            {
                base.CommitEditingStyle(tableView, editingStyle, indexPath);
            }
        }
    }
}