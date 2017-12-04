using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    
    public class Dish
    {
        public int DishCode { get; set; }
        public string DishName { get; set; }
        public MyEnums.Size DishSize { get; set; }
        public int DishPrice { get; set; }
        public MyEnums.Hechsher DishHechsher { get; set; }
        public int stars { get; set; }
        public MyEnums.Type DishType {get;set;}
        public override string ToString()
        {
            string str = string.Format("\nDish code: {0}\nDish name: {1}\nDish size: {2}\nDish price: {3}\nHechsher: {4}\nstars: {5}\ntype: {6}\n", DishCode, DishName, DishSize, DishPrice, DishHechsher, stars,DishType);
            return str;
        }
    }
}
