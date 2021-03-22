using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS
{
    class DBconnection
    {
        public string myConnection()
        {
            string conn = "server=localhost;user id=root;password=;database=inventory";
            return conn;
        }

        public string myConnectionHimpro()
        {
            string conn = "server=192.168.0.2;user id=root;password=boom123;database=opd";
            return conn;
        }
    }
}
