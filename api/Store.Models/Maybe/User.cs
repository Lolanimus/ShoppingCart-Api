using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Maybe
{
    public struct Name
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public struct Address
    {
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Zip { get; set; }
    }

    public class User : StoreEntity
    {
        public string? Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public string? Phone { get; set; }
    }
}
