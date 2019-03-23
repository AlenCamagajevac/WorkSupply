using Microsoft.AspNetCore.Mvc;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.Core.Query
{
    public class UsersQuery
    {
        [FromQuery(Name = "Name")]
        public string Name { get; set; }
        
        [FromQuery]
        public int? Page { get; set; }

        [FromQuery]
        public Role? Role { get; set; }
    }
}