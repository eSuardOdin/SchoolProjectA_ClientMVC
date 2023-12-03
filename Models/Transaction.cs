using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectA_ClientMVC.Models;

public class Transaction
{
    public int TransactionId { get; set; }
    public int BankAccountId { get; set; }
    public decimal TransactionAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionLabel { get; set; } = null!;
    public string? TransactionDescription { get; set; }


    /*public void ToListItem(ListView list)
    {
        ListViewItem item = new(TransactionLabel);
        item.SubItems.Add(TransactionDate.ToShortDateString());
        item.SubItems.Add(TransactionAmount.ToString() + "€");
        item.SubItems.Add(TransactionDescription);
        list.Items.Add(item);
    }*/
}

