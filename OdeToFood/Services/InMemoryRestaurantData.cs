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

        public Restaurant AddOrUpdate(Restaurant input)
        {
            Restaurant item = null;

            if (input.Id == 0)
            {
                // add
                int nextId = list.Max(x => x.Id) + 1;

                input.Id = nextId;

                item = new Restaurant { Id = input.Id, Name = input.Name, Cuisine = input.Cuisine };
                (list as List<Restaurant>).Add(item);
            }
            else
            {
                // update
                item = list.Where(x => x.Id == input.Id).FirstOrDefault();
                if (item != null)
                {
                    item.Name = input.Name;
                    item.Cuisine = input.Cuisine;
                }
            }

            return item;
        }
    }
}
