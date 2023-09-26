using SOfCO.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared.Models
{
    public class GetConnectionDB
    {
        private readonly SQL _database;
        public GetConnectionDB()
        {
            string Connection = "Data Source=MOHAMED;Initial Catalog=Yummy;Integrated Security=True";
            _database = new SQL(Connection);
        }
        public SQL GetDatabase()
        {
            return _database;
        }
    }
}
