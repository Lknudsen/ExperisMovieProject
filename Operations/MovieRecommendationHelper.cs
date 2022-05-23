using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperisMovieProject.Models;

namespace ExperisMovieProject.Operations
{
    public static class MovieRecommendationHelper
    {
        public static Dictionary<string, int> RecommendedMoviesListPerSoldUnit(List<Users> users, List<Products> products)
        {
            Dictionary<string, int> SoldUnits = new Dictionary<string, int>();

            //Fill soldUnits into dictionary
            foreach (Products product in products)
            {
                foreach (Users user in users)
                {
                    //The purchased products will be loaded as "x;y;z;k" Now we will split it.
                    string[] values = user.purchasedProducts.Split(';');

                    foreach (var item in values)
                    {
                        if (item.Replace(" ", "").Equals(product.id.ToString()))
                        {
                            //Check if the products already exist, if yes then update
                            if (SoldUnits.Keys.Contains(product.name))
                            {
                                SoldUnits[product.name] = SoldUnits[product.name] + 1;
                            }
                            else
                            {
                                SoldUnits.Add(product.name, 1);
                            }
                        }
                    }
                }
            }

           
            var sortedDict = from entry in SoldUnits orderby entry.Value descending select entry;
            return SoldUnits = sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public static Dictionary<string, int> RecommendedMoviesBasedOnViewedUnits(List<Users> users, List<Products> products)
        {
            Dictionary<string, int> ViewedUnits = new Dictionary<string, int>();

            //Fill soldUnits dictionary...
            foreach (Products product in products)
            {
                foreach (Users user in users)
                {
                    //Purchased products are loaded in as "x;y;z;k" we now have to split it.
                    string[] values = user.viewedProducts.Split(';');

                    foreach (var item in values)
                    {
                        if (item.Replace(" ", "").Equals(product.id.ToString()))
                        {
                            //Check for product existence, if yes update, otherwhise add.
                            if (ViewedUnits.Keys.Contains(product.name))
                            {
                                ViewedUnits[product.name] = ViewedUnits[product.name] + 1;
                            }
                            else
                            {
                                ViewedUnits.Add(product.name, 1);
                            }
                        }
                    }
                }
            }

            //Sort soldUnits by value and convert back to Dict. The most sold item is now the first item.
            var sortedDict = from entry in ViewedUnits orderby entry.Value descending select entry;
            return ViewedUnits = sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public static Dictionary<string, float> RecommendedMoviesListUserrating(List<Users> users, List<Products> products)
        {
            Dictionary<string, float> moviesByRating = new Dictionary<string, float>();

            foreach (Products product in products)
            {
                if (moviesByRating.Keys.Contains(product.name))
                {
                    moviesByRating[product.name] = product.rating;
                }
                else
                {
                    moviesByRating.Add(product.name, product.rating);
                }
            }

            var sortedDict = from entry in moviesByRating orderby entry.Value descending select entry;
            return moviesByRating = sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public static Dictionary<string, List<string>> RecommendedMovieGenres(List<Users> users, List<Products> products, List<CurrentUserSession> userSessions)
        {
            //Dictionary returned.
            Dictionary<string, List<string>> recommendations = new Dictionary<string, List<string>>();

            foreach (CurrentUserSession session in userSessions)
            {
                //Initialize variables
                Dictionary<int, string> currentSessionGenres = new Dictionary<int, string>();
                List<string> tempProducts = new List<string>();
                Products currentMovie = new Products();
                Users currentUser = new Users();
                List<string> genres = new List<string>();

                //Acquire info from the current Movie.
                products.ForEach(product =>
                {
                    if (session.productID == product.id)
                    {
                        currentMovie = product;
                    }
                });

                //Acquire the info from the current user.
                users.ForEach(user =>
                {
                    if (session.userID == user.id)
                    {
                        currentUser = user;
                    }
                });

                //Add currentMovie genres to the local list variable genres.
                genres = currentMovie.getGenresAsList();

                //Acquire recommended movies based on genres..
                foreach (Products product in products)
                {
                    int genreMatches = 0;

                    //Check all the genres and count all matches if they exist.
                    if (genres.Any(genre => (product.keywordOne.Contains(genre)))) genreMatches++;

                    if (genres.Any(genre => (product.keywordTwo.Contains(genre)))) genreMatches++;

                    if (genres.Any(genre => (product.keywordThree.Contains(genre)))) genreMatches++;

                    if (genres.Any(genre => (product.keywordFour.Contains(genre)))) genreMatches++;

                    if (genres.Any(genre => (product.keywordFive.Contains(genre)))) genreMatches++;

                    //Add movies to the recommendationlist if two or more genres match.
                    if (genreMatches >= 3)
                    {
                        tempProducts.Add(product.name);
                    }
                }

                //Check if the products exists, if yes then update or add.
                if (recommendations.ContainsKey(currentUser.name))
                {
                    recommendations[currentUser.name] = tempProducts;
                }
                else
                {
                    recommendations.Add(currentUser.name, tempProducts);
                }
            }

            return recommendations;
        }
        public static List<Products> RecommendedMoviesBasedOnSalesAndUserreviews(List<Users> users, List<Products> products)
        {
            List<Products> moviesHighlyRated = new List<Products>();
            List<Products> amountOfMoviesSold = new List<Products>();

            for (int y = 0; y < products.Count; y++)
            {
                if (products.ElementAt(y).rating > 3.5)
                {
                    moviesHighlyRated.Add(products.ElementAt(y));
                }
            }

            users.ForEach(user =>
            {
                user.productsPurchaseSplit.ForEach(productId =>
                {
                    products.ForEach(product =>
                    {
                        if (productId.Equals(product.id))
                        {
                            amountOfMoviesSold.Add(product);
                        }
                    });
                });
            });

            List<Products> movieRecommendationList = amountOfMoviesSold.Union(moviesHighlyRated).ToList();
            return movieRecommendationList;
        }


    }
}
