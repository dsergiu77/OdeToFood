using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private readonly IEnumerable<Restaurant> list;

        public InMemoryRestaurantData()
        {
            list = new List<Restaurant> {
                new Restaurant { Id = 1, Name = "Scott's Pizza Place" },
                new Restaurant { Id = 2, Name = "Asia Restaurant" },
                new Restaurant { Id = 3, Name = "Pizza Hawai" }
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return list.OrderBy(x => x.Name);
        }

        public Restaurant Get(int id)
        {
            return list.FirstOrDefault(x => x.Id == id);
        }

        public Restaurant Add(Restaurant input)
        {
            int nextId = list.Max(x => x.Id) + 1;

            input.Id = nextId;

            var item = new Restaurant { Id = input.Id, Name = input.Name, Cuisine = input.Cuisine };
            (list as List<Restaurant>).Add(item);

            return item;
        }

        public Restaurant Update(Restaurant input)
        {
            // update
            var item = list.FirstOrDefault(x => x.Id == input.Id);
            if (item != null)
            {
                item.Name = input.Name;
                item.Cuisine = input.Cuisine;
            }

            return item;
        }

        public void SaveChanges()
        {
            // do nothing - in memory data
        }
    }
}
