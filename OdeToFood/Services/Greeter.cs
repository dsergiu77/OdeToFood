using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public class Greeter : IGreeter
    {
        string greeting;

        public Greeter(IConfiguration config)
        {
            greeting = config["Greeting"];
        }

        public string GetMessageOfTheDay()
        {
            return this.greeting;
        }
    }
}
