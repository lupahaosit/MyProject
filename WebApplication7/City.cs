using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public City(string name)
        {
            this.Name = name;
        }
    }
}
