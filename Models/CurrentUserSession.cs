using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperisMovieProject.Models
{
    public class CurrentUserSession
    {
        [Index(0)]
        public int userID { get; set; }
        [Index(0)]
        public int productID { get; set; }

        public CurrentUserSession(int userID, int productID)
        {
            this.userID = userID;
            this.productID = productID;
        }
    }



}
