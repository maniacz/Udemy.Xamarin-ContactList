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
        public ContactTableViewSource(UITableView table, List<Contact> contacts)
        {
            table.RegisterNibForCellReuse(ContactTableViewCell.Nib, ContactTableViewCell.Key);
            this.contacts = contacts;
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
    }
}