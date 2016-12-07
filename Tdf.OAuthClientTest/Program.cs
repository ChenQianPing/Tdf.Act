using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.OAuthClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new OAuthClientTest().Get_Accesss_Token_By_Client_Credentials_Grant();
            Console.Read();
        }
    }
}
