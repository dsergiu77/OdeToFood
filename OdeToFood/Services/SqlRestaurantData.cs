using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Entities;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            this.context = context;
        }

        public Restaurant Add(Restaurant input)
        {
            context.Add(input);

            return input;
        }

        public Restaurant Update(Restaurant input)
        {
            context.Attach(input).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return input;
        }

        public Restaurant Get(int id)
        {
            return context.Restaurants.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return context.Restaurants.OrderBy(x => x.Name);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
