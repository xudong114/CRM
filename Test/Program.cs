using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Dictionary<string, string>();
            data.Add("UserName", "13962475261");
            data.Add("Password", "123321");
            data.Add("UserType", "0");
            var url = "http://api.gojiaju.com/api/user/register";
            var result = Ingenious.Infrastructure.Helper.WebApiHelper.Post(url, data);

        }
    }
}
