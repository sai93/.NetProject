using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLLLibrary;


namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            DLL lib = new DLL(1);
            lib.debug("Test","This is Fake",new DivideByZeroException());
        }
    }
}
