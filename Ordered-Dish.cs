using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Ordered_Dish
    {
        public int OrderCode { get; set; }
        public int DishCode { get; set; }
        public int Amount { get; set; }
        public override string ToString()
        {
            return string.Format("\norder code: {0}\ndish code: {1}\namount: {2}\n",OrderCode,DishCode,Amount);
        }
    }
}
