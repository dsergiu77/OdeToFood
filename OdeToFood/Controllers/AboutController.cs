using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    //[Route("about")]
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        //[Route("")]
        public string Phone()
        {
            return "+1+555+333+5555";
        }

        //[Route("address")]
        public string Address()
        {
            return "USA";
        }
    }
}