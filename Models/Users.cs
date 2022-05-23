using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperisMovieProject.Models
{
    public class Users
    {
        [Index(0)]
        public int id { get; set; }
        [Index(1)]
        public string name { get; set; }
        [Index(2)]
        public string viewedProducts { get; set; }
        [Index(3)]
        public string purchasedProducts { get; set; }

        public List<string> productsPurchaseSplit { get; set; }

        public Users(int id, string name, string viewedProducts, string purchasedProducts)
        {
            this.id = id;
            this.name = name;
            this.viewedProducts = viewedProducts;
            this.purchasedProducts = purchasedProducts;
            productsPurchaseSplit = convertPurchasedProductsToSplit(purchasedProducts);
        }

        public Users()
        {
        }

        public List<string> convertPurchasedProductsToSplit(string viewedProducts)
        {
            List<string> returnList = new List<string>();

            string[] values = purchasedProducts.Split(';');

            foreach (string streng in values)
            {
                returnList.Add(streng);
            }
            return returnList;
        }
    }




}
