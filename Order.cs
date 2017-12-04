using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   
    public class Order
    {
        public int OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public int Branch { get; set; }
        public MyEnums.Hechsher OrderHechsher { get; set; }
        public string CostumerName { get; set; }
        public string Adress { get; set; }
        public string Position { get; set; }
        public bool provided { get; set; }
        public string CreditCard { get; set; }
        //public Order(int a, DateTime b, int c, MyEnums.Hechsher d, string e, string f, string f1, bool g, string h)
        //{
        //    OrderCode = a;
        //    OrderDate = b;
        //    Branch = c;
        //    OrderHechsher = d;
        //    CostumerName = e;
        //    Adress = f;
        //    Position = f1;
        //    provided = g;
        //    CreditCard = h;
        //}
        //public Order()
        //{

        //}

        public override string ToString()
        {
            string str = string.Format("\norder code: {0}\ndate: {7}\nbranch: {1}\nhechsher: {2}\ncostumer name: {3}\nncostumer adress: {4}\nncostumer position: {5}\ncredit card: {6}\nprovided: {8}\n", OrderCode, Branch, OrderHechsher, CostumerName, Adress, Position, CreditCard, OrderDate,provided);
            return str;
        }
    }
}
