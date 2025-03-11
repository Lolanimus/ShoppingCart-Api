using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Maybe
{
    public class Product : StoreEntity
    {
        public string? Title { get; set; }
        public string? PictureUri { get; set; }
        public string? Description { get; set; }
        public Size? Size { get; set; }
        public string? Price { get; set; }
    }
}
