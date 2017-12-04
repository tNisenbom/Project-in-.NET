using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Branch
    {
        public int BranchCode { get; set; }
        public string NameBranch { get; set; }
        public string AdressBranch { get; set; }
      //  public string city { get; set; }
        public string Phone { get; set; }
        public string Manager { get; set; }
        public int Employees { get; set; }
        public MyEnums.Hechsher? HechsherBranch { get; set; }
        public override string ToString()
        {
            string str = string.Format("\nbranch code: {0}\nname branch: {1}\nadress branch:{2}\nphone number: {3}\nmanager: {4}\nEmployees: {5}\nhechsher: {6}\n", BranchCode, NameBranch, AdressBranch, Phone, Manager, Employees, HechsherBranch);
            return str;
        }
    }
}
