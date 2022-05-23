using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperisMovieProject.Models
{
    public class Products
    {
        [Index(0)]
        public int id { get; set; }
        [Index(1)]
        public string name { get; set; }
        [Index(2)]
        public int year { get; set; }
        [Index(3)]
        public string keywordOne { get; set; }
        [Index(4)]
        public string keywordTwo { get; set; }
        [Index(5)]
        public string keywordThree { get; set; }
        [Index(6)]
        public string keywordFour { get; set; }
        [Index(7)]
        public string keywordFive { get; set; }
        [Index(8)]
        public float rating { get; set; }
        [Index(9)]
        public int price { get; set; }

        public Products(int id, string name, int year, string keywordOne, string keywordTwo,
            string keywordThree, string keywordFour, string keywordFive, float rating, int price)
        {
            this.id = id;
            this.name = name;
            this.year = year;
            this.keywordOne = keywordOne;
            this.keywordTwo = keywordTwo;
            this.keywordThree = keywordThree;
            this.keywordFour = keywordFour;
            this.keywordFive = keywordFive;
            this.rating = rating;
            this.price = price;
        }

        public Products()
        {
        }

        public List<string> getGenresAsList()
        {
            List<string> genres = new List<string>();

            if (!string.IsNullOrWhiteSpace(this.keywordOne)) genres.Add(this.keywordOne);
            if (!string.IsNullOrWhiteSpace(this.keywordTwo)) genres.Add(this.keywordTwo);
            if (!string.IsNullOrWhiteSpace(this.keywordThree)) genres.Add(this.keywordThree);
            if (!string.IsNullOrWhiteSpace(this.keywordFour)) genres.Add(this.keywordFour);
            if (!string.IsNullOrWhiteSpace(this.keywordFive)) genres.Add(this.keywordFive);

            return genres;
        }
    }



}
