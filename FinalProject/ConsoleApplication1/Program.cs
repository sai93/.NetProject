using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLogModel;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            sjinkaDataBaseEntities con = new sjinkaDataBaseEntities();
            var q = from b in con.Profiles select b;

            

            
        }
    }
}
