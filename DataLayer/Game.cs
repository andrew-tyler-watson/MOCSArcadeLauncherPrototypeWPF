using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Game : object
    {
        public string Name { get; set; }
        public string PathToRunScript { get; set; }
        public string PathToIconFile { get; set; }
        public bool IsSelected { get; set; }
    }
}
