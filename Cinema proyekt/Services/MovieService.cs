﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Cinema_proyekt.Models;

namespace MovieApp.Services
{
    public class MovieService
    {
        public static dynamic Data { get; set; }
        public static dynamic SingleData { get; set; }
        public static List<Movie> GetMovies(string movie)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            response = httpClient.GetAsync($@"http://www.omdbapi.com/?i=tt3896198&apikey=e60fabf0&s={movie}&plot=full").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            Data = JsonConvert.DeserializeObject(str);
            List<Movie> movies = new List<Movie>();
            try
            {
                for (int i = 0; i < 25; i++)
                {
                    response = httpClient.GetAsync($@"http://www.omdbapi.com/?i=tt3896198&apikey=e60fabf0&t={Data.Search[i].Title}&plot=full").Result;
                    str = response.Content.ReadAsStringAsync().Result;
                    SingleData = JsonConvert.DeserializeObject(str);
                    var mymovie = new Movie();
                    mymovie.Description = SingleData.Plot;
                    mymovie.ImagePath = SingleData.Poster;
                    mymovie.Name = SingleData.Title;
                    mymovie.Rating = SingleData.imdbRating;
                    movies.Add(mymovie);
                }
            }
            catch (Exception)
            {
            }
            return movies;
        }
    }
}