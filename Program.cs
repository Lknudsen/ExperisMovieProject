using System;
using ExperisMovieProject.Models;
using ExperisMovieProject.Operations;

namespace Experis_movie
{
    public class Movie
    {
        public static void Main(string[] args)
        {
            List<Products> products = new List<Products>();
            List<Users> users = new List<Users>();
            List<CurrentUserSession> currentUserSessions = new List<CurrentUserSession>();
            Dictionary<string, int> MoviesRecommendationList = new Dictionary<string, int>();
            Dictionary<string, float> MoviesRecommendedBasedOnRating = new Dictionary<string, float>();
            List<Products> MoviesRecommendedBasedOnUserReviewsAndSoldUnits = new List<Products>();
            Dictionary<string, List<string>> MoviesRecommendedBasedOnUsersession = new Dictionary<string, List<string>>();


            try
            {
                users = CsvOperations.ParseCsvToUsers();
                products = CsvOperations.ParseCsvToProducts();
                currentUserSessions = CsvOperations.ParseCsvToUserSession();
            }
            catch (Exception e)
            {
                //If it fails, exit.
                Console.WriteLine("Something failed.... \n" + e.Message);
                Console.WriteLine("Exiting after any button pressed....");
                Console.ReadKey();
                Environment.Exit(0);
            }
            foreach (var item in MoviesRecommendedBasedOnUsersession)
            {
                Console.WriteLine(item);
            }

            //This will print the recommended movies based per unit sold
            MoviesRecommendationList = MovieRecommendationHelper.RecommendedMoviesListPerSoldUnit(users, products);
            Console.WriteLine("Recommended movies based on amount of units sold:");
            foreach (var item in MoviesRecommendationList.Take(4))
            {
                Console.WriteLine($"Movie: {item.Key}, Sold amount: {item.Value}");
            }
            Console.ReadKey();
            Console.WriteLine("\n");

            //This will print the recommended movies based on the rating from the users
            MoviesRecommendedBasedOnRating = MovieRecommendationHelper.RecommendedMoviesListUserrating(users, products);
            Console.WriteLine("\n\nRecommended movies based on our user ratings:");
            foreach (var item in MoviesRecommendedBasedOnRating.Take(4))
            {
                Console.WriteLine($"Movie: {item.Key}, user rating: {item.Value}");
            }

            Console.ReadKey();
            Console.WriteLine("\n");

            //This will print the recommended movies for each user based on genres
            MoviesRecommendedBasedOnUsersession = MovieRecommendationHelper.RecommendedMovieGenres(users, products, currentUserSessions);
            foreach (var recommendation in MoviesRecommendedBasedOnUsersession.Keys)
            {
                Console.WriteLine($"Recommended movies for: {recommendation}");
                foreach (var product in MoviesRecommendedBasedOnUsersession[recommendation])
                {
                    Console.WriteLine($"{product}");
                }
                Console.WriteLine("\n");
            }
            Console.ReadKey();
        }


    }
}