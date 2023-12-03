using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolProjectA_ClientMVC.Models;

public class Moni
{
    public int MoniId { get; set; }
    public string MoniLogin { get; set; } = null!;
    public string MoniPwd { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
