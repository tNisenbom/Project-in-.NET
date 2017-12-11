using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BL
{
    public class BL_imp : IBL
    {
        DAL.Idal dal;
       public BL_imp()
        {
           dal = DAL.FactoryDal.getDal();

           //Order o1 = new Order
           //{
           //    OrderCode = 0,
           //    OrderDate = DateTime.Now,
           //    Branch = 10001,
           //    OrderHechsher = MyEnums.Hechsher.medium,
           //    CostumerName = "צבי הלוי",
           //    Adress = "asdf",
           //    Position = "asdf",
           //    provided = false,
           //    CreditCard = "11111111"
           //};
           //Order o2 = new Order
           //{
           //    OrderCode = 0,
           //    OrderDate = DateTime.Now,
           //    Branch = 10001,
           //    OrderHechsher = MyEnums.Hechsher.best,
           //    CostumerName = "יעקוב שמיר",
           //    Adress = "asdf",
           //    Position = "asdf",
           //    provided = true,
           //    CreditCard = "55555555"
           //};
           //Order o3 = new Order
           //{
           //    OrderCode = 0,
           //    OrderDate = DateTime.Now,
           //    Branch = 10001,
           //    OrderHechsher = MyEnums.Hechsher.best,
           //    CostumerName = "מירים יעקובי",
           //    Adress = "asdf",
           //    Position = "asdf",
           //    provided = false,
           //    CreditCard = "6666666666666666"
           //};
           //Order o4 = new Order
           //{
           //    OrderCode = 0,
           //    OrderDate = DateTime.Now,
           //    Branch = 10001,
           //    OrderHechsher = MyEnums.Hechsher.best,
           //    CostumerName = "שירה אזרחי",
           //    Adress = "asdf",
           //    Position = "asdf",
           //    provided = true,
           //    CreditCard = "6666666666666666"
           //};
           //this.AddOrder(o1);
           //this.AddOrder(o2);
           //this.AddOrder(o3);
           //this.AddOrder(o4);
        }
       

        #region Dish Function
        public void AddDish(Dish d)
        {
            if (d.DishName == null)
                throw new Exception("you must enter a name");
            if (d.stars < 1 || d.stars > 5)
                throw new Exception("the stars number is wrong");
            if (d.DishPrice < d.stars * 5)
                throw new Exception("the price is too cheap compare to the stars");
            dal.AddDish(d);
        }

        public void DeleteDish(int code)
        {
            dal.DeleteDish(code);
            bool tmp = false;
            var v = (from o in dal.GetListOrderDish()    //מחפש את כל המנות המוזמנות באותו קוד שלא סופקו ומוחק אותם
                     let s = this.OrderProvided(o.OrderCode)
                     where s == false && o.DishCode == code
                     select o).ToList<Ordered_Dish>();
            int k = v.Count();
            for (int i = 0; i < k; i++)
            {
                dal.GetListOrderDish().Remove(v[i]);
                tmp = true;
            }
            if (tmp == true)
            {
                throw new Exception("there are ordered dishes that deleted...");
            }
        }

        public void ApdateDish(Dish d)
        {
            if (d.stars < 1 || d.stars > 5)
                throw new Exception("the stars number is wrong");
            if (d.DishPrice < d.stars * 5)
                throw new Exception("the price is too cheap compare to the stars");

            dal.ApdateDish(d);
        }

        public Dish GetDish(int code)
        {
            if (code < 0)
            {
                throw new Exception("the code must be positiv");
            }
            return dal.GetDish(code);
        }

        public void CopyToDish(Dish os, Dish od)
        {

            od.DishCode = os.DishCode;
            od.DishHechsher = os.DishHechsher;
            od.DishName = os.DishName;
            od.DishPrice = os.DishPrice;
            od.DishSize = os.DishSize;
            od.DishType = os.DishType;
            od.stars = os.stars;
        }
        #endregion

        #region branch function
        public void AddBranch(Branch b)
        {
            if ((b.Phone).Length > 10 || b.Phone.Length < 9 )
                throw new Exception("the phone number is wrong!");
            if (b.Employees < 1)
                throw new Exception("the employees number is wrong!");
            dal.AddBranch(b);
        }

        public Branch GetBranch(int code)
        {
            if (code < 0)
            {
                throw new Exception("the code must be positiv");
            }
            return dal.GetBranch(code);
        }


        public void DeleteBranch(int code)
        {
            dal.DeleteBranch(code);
            dal.GetListOrder().RemoveAll(D => D.Branch == code);
            //  dal.DeleteDish(code);
            bool tmp = false;
            var v = (from o in dal.GetListOrder()    //מחפש את כל  ההזמנות באותו קוד  ומוחק אותם
                     where o.Branch == code
                     select o).ToList<Order>();
            int k = v.Count();
            for (int i = 0; i < k; i++)
            {
                try
                {
                    DeleteOrder(v[i].OrderCode);
                    tmp = true;
                }
                catch (Exception)
                {
                }

            }
            if (tmp == true)
            {
                throw new Exception("there are ordered  that deleted...");
            }

        }

        public void ApdateBranch(Branch b)
        {
            if ((b.Phone).Length > 10 || b.Phone.Length < 9)
                throw new Exception("the phone number is wrong!");
            if (b.Employees < 1)
                throw new Exception("the employees number is wrong!");

            dal.ApdateBranch(b);
        }

        public void CopyToBranch(Branch os, Branch od)
        {
            od.AdressBranch = os.AdressBranch;
            od.BranchCode = os.BranchCode;
            od.Employees = os.Employees;
            od.HechsherBranch = os.HechsherBranch;
            od.Manager = os.Manager;
            od.NameBranch = os.NameBranch;
            od.Phone = os.Phone;
          //  dal.CopyToBranch(os, od);
        }
        #endregion

        #region order function
        public void CopyToOrder(Order os, Order od)
        {
           // dal.CopyToOrder(os, od);
            od.Adress = os.Adress;
            od.Branch = os.Branch;
            od.CostumerName = os.CostumerName;
            od.CreditCard = os.CreditCard;
            od.OrderCode = os.OrderCode;
            od.OrderDate = os.OrderDate;
            od.OrderHechsher = os.OrderHechsher;
            od.Position = os.Position;
            od.provided = os.provided;
        }
        public Order GetOrder(int code)
        {
            if (code < 0)
            {
                throw new Exception("the code must be positiv");
            }
            return dal.GetOrder(code);
        }
        public void AddOrder(Order o)
        {
            List<Branch> branchH = (dal.GetListBranch().Where(i => i.BranchCode == o.Branch)).ToList<Branch>();
            if (branchH.Count() != 0)
                if (branchH[0].HechsherBranch > o.OrderHechsher)
                    throw new Exception("the order hechsher is not appropriate to the branch");
            if (o.CreditCard.Length != 8 && o.CreditCard.Length != 16)
                throw new Exception("the digits number is wrong! ");

            dal.AddOrder(o);
        }

        public void DeleteOrder(int code)
        {
            dal.DeleteOrder(code);
            // int index = dal.GetListOrder().FindIndex(s => s.OrderCode == code);
            // if (index != -1 && dal.GetListOrder()[index].provided == false)
            // {

            dal.GetListOrderDish().RemoveAll(D => D.OrderCode == code);
            throw new Exception("ordered dishes deleted...");
            // }
            //  dal.DeleteOrder(code);
            //int index = dal.GetListOrder().FindIndex(s => s.OrderCode == code);
            //    if (index != -1 && dal.GetListOrder()[index].provided == false)
            //    {
            //        dal.GetListOrderDish().RemoveAll(D => D.OrderCode == code);
            //        dal.DeleteOrder(code);
            //        throw new Exception("ordered dishes deleted...");
            //    }
            //dal.DeleteOrder(code);
        }

        public bool OrderProvided(int code)
        {
            int index = dal.GetListOrder().FindIndex(s => s.OrderCode == code);
            return ((dal.GetListOrder()[index]).provided);
        }
        public void ApdateOrder(Order o)
        {
            List<Branch> branchH = (dal.GetListBranch().Where(i => i.BranchCode == o.Branch)).ToList<Branch>();
            if (branchH.Count() != 0)
                if (branchH[0].HechsherBranch > o.OrderHechsher)
                    throw new Exception("the order hechsher is not appropriate to the branch");
            if (o.CreditCard.Length != 8 && o.CreditCard.Length != 16)
                throw new Exception("the digits number is wrong! ");


            dal.ApdateOrder(o);
        }
        #endregion

        #region order_dish function
        public void AddOrderedDish(Ordered_Dish o)
        {
            if (order_by_high_price(o.OrderCode) == false)
            {
                throw new Exception("it's imposible to order order at this price");
            }
            if (o.Amount < 1)
                throw new Exception("the amount is wrong!");
            //if (order_by_hechsher(o.OrderCode, o.DishCode) == false)
            //    throw new Exception("the hechsher dish is not suitable for this order...");
            dal.AddOrderedDish(o);
        }

        public void DeleteOrderDish(int o, int d)
        {
            if (o < 0 || d < 0)
                throw new Exception("you cant enter negative code");
            dal.DeleteOrderDish(o, d);
        }

        public void ApdateOrderDish(Ordered_Dish o)
        {
            if (o.Amount < 1)
                throw new Exception("the amount is wrong!");

            dal.ApdateOrderDish(o);
        }
        public void CopyToOrderedDish(Ordered_Dish os, Ordered_Dish od)
        {
           // dal.CopyToOrderedDish(os, od);

            od.Amount = os.Amount;
            od.DishCode = os.DishCode;
            od.OrderCode = os.OrderCode;
        }
        #endregion

        public List<Order> GetListOrder()
        {
            if (dal.GetListOrder().Count() == 0)
                throw new Exception("there are no dishes");
            return dal.GetListOrder();
        }

        public List<Dish> GetListDish()
        {
            if (dal.GetListDish().Count() == 0)
                throw new Exception("there are no dishes");
            return dal.GetListDish();
        }

        public List<Branch> GetListBranch()
        {
            if (dal.GetListBranch().Count() == 0)
                throw new Exception("there are no branches");
            return dal.GetListBranch();
        }

        public List<Ordered_Dish> GetListOrderDish()
        {
            if (dal.GetListOrderDish().Count() == 0)
                throw new Exception("there are no dishes");
            return dal.GetListOrderDish();
        }

        public int payment(int o)
        {
            int sum = 0;
            IEnumerable<Ordered_Dish> spec = from item in dal.GetListOrderDish()
                                             where item.OrderCode == o
                                             select new Ordered_Dish
                                             {
                                                 OrderCode = item.OrderCode,
                                                 DishCode = item.DishCode,
                                                 Amount = item.Amount
                                             };

            IEnumerable<int> endPrice = from item in spec
                                        select Price_By_Code(item.DishCode, item.Amount);
            foreach (var item in endPrice)
            {
                sum += item;
            }
            return sum;
        }
        public IEnumerable<Ordered_Dish> specipicOrderDish(int o)
        {
            return from item in dal.GetListOrderDish()
                   where item.OrderCode == o
                   select new Ordered_Dish
                   {
                       OrderCode = item.OrderCode,
                       DishCode = item.DishCode,
                       Amount = item.Amount
                   };


        }

        public bool order_by_high_price(int o, int max = 1000)
        {
            return !(payment(o) > max);

        }

        public bool order_by_hechsher(int o, int d)
        {
            return (getHechsherDish(d) >= getHechsherOrder(o));
        }

        public IEnumerable<Order> condition(Func<Order, bool> func)
        {
            var v = dal.GetListOrder().Where(func);
            return v;
        }

        public int earn_by_Hechsher(MyEnums.Hechsher h)
        {
            var lst = from o in dal.GetListOrder()
                      group o by o.OrderHechsher into g
                      where g.Key == h
                      select g;

            int sum = 0;
            foreach (var item in lst)
            {
                foreach (var item1 in item)
                    sum += payment(item1.OrderCode);
            }
            return sum;

        }

        public int earn_by_Date(DateTime d)
        {
            DateTime date = d.Date;
            var lst = from o in dal.GetListOrder()
                      group o by o.OrderDate.Date into g
                      where g.Key == date.Date
                      select g;

            int sum = 0;
            foreach (var item in lst)
            {
                foreach (var item1 in item)
                    sum += payment(item1.OrderCode);
            }
            return sum;
        }

        public int earn_by_Adress(string str)
        {
            var lst = from o in dal.GetListOrder()
                      group o by o.Adress into g
                      where g.Key == str
                      select g;

            int sum = 0;
            foreach (var item in lst)
            {
                foreach (var item1 in item)
                    sum += payment(item1.OrderCode);
            }
            return sum;
        }

        public bool check_age(int a)
        {
            if (a < 18)
                return false;
            return true;
        }
        public bool Dish_exist_code(int c)
        {
            int index = dal.GetListDish().FindIndex(s => s.DishCode == c);
            return (index != -1);
        }

        public IEnumerable<BE.Dish> GetAllDish(Func<BE.Dish, bool> predicat = null)
        {
            if (predicat == null)
                return dal.GetListDish().AsEnumerable();
            return dal.GetListDish().Where(predicat);
        }


        public IEnumerable<Ordered_Dish> GetOrderDishes(int o)
        {
            int index = dal.GetListOrder().FindIndex(s => s.OrderCode == o);
            if (index == -1)
            {
                throw new Exception("there are no order with this code");
            }
            return dal.GetOrderDishes(o);
        }

        //public int Same_City(string c)
        //{
        //    int index = dal.GetListBranch().FindIndex(s => s.city == c);
        //    if (index == -1)
        //        return dal.GetListBranch()[0].BranchCode;
        //    return dal.GetListBranch()[index].BranchCode;
        //}

        //public string city1(int code)
        //{
        //    int index = dal.GetListBranch().FindIndex(s => s.BranchCode == code);
        //    return dal.GetListBranch()[index].city;
        //}

        //internal void Move_Branch(int s, int d)
        //    {
        //      //  צריך להעביר את כל ההזמנות מסניף שנמחק לסניף חדש
        //    }
        public int Price_By_Code(int code, int am)
        {
            int index = dal.GetListDish().FindIndex(s => s.DishCode == code);
            return ((dal.GetListDish()[index].DishPrice) * am);
        }
        public MyEnums.Hechsher getHechsherDish(int d)
        {
            int index = dal.GetListDish().FindIndex(s => s.DishCode == d);
            if (index == -1)
                throw new Exception("the dish is not exist!");
            return dal.GetListDish()[index].DishHechsher;
        }
        public MyEnums.Hechsher getHechsherOrder(int o)
        {
            int index = dal.GetListOrder().FindIndex(s => s.OrderCode == o);
            if (index == -1)
                throw new Exception("the order is not exist!");
            return dal.GetListOrder()[index].OrderHechsher;
        }
        public List<Dish> Dish_Till_price(int p)//מחזיר את כל המנות עד סכום מסוים
        {
            var lst = (from d in dal.GetListDish()
                       where d.DishPrice <= p
                       select d).ToList<Dish>();
            return lst;
        }
        public IEnumerable<Dish> Dish_By_Hechsher(MyEnums.Hechsher h)//מחזיר רשימה של כל המנות לפי הכשר מסוים
        {
            var lst = from d in dal.GetListDish()
                      where d.DishHechsher == h
                      select d;
            return lst;
        }
        public string Order_Over_Price(int p)//פונקציה שמחזירה את כל האנשים שהזמינו מעל סכום מסוים
        {
            var lst = from o in dal.GetListOrder()
                      where payment(o.OrderCode) > p
                      select o;
            string str = "";
            foreach (var item in lst)
            {
                string str1 = string.Format("name: {0,-10} addres:{1,-10}\n", item.CostumerName, item.Adress);
                str += str1;
            }
            return str;
        }
        public IEnumerable<Order> Not_Provided()//פונקציה שמחזירה את כל המנות שלא סופקו
        {
            var lst = from d in dal.GetListOrder()
                      where d.provided == false
                      select d;
            return lst;
        }
        public List<Dish> Sort_By_Stars()//מחזיר את כל ההזמנות ממוינות לפי דירוג
        {
            var lst = (from d in dal.GetListDish()
                       orderby d.stars descending
                       select d).ToList<Dish>();
            return lst;

        }
        public List<Dish> Sort_By_Price()//מחזיר את כל ההזמנות ממוינות לפי מחיר
        {
            var lst = (from d in dal.GetListDish()
                       orderby d.DishPrice descending
                       select d).ToList<Dish>();
            return lst;
        }
        public IGrouping<MyEnums.Type, Dish> Show_By_Type(MyEnums.Type t)///מחזיר את כל המנות שבהכשר ספציפי
        {
            var lst = from d in dal.GetListDish()
                      group d by d.DishType into g
                      where g.Key == t
                      select g;
            foreach (var item in lst)
            {
                return item;
            }
            throw new Exception("no dishes to show on " + t + " type");
            // return lst;
        }
    }
}

