using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{

    
    public  class Photo
    {
        public Photo(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
[Geo(10, 20)]
        public Photo(string path, string name)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
      public  void Add(string path, string name) { }

        public string  Path { get; set; }
      
        public string  Name { get; set; }

    }
}
