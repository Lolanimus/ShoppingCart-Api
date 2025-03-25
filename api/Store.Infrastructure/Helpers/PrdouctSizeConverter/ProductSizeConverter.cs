﻿using Newtonsoft.Json.Linq;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastracture.Helpers.PrdouctSizeConverter
{
    public class ProductSizeConverter
    {
        public string FromIntToStr(int value)
        {
            return value switch
            {
                0 => "unknown",
                1 => "s",
                2 => "m",
                3 => "l",
                4 => "xl"
            };
        }

        public string FromEnum(ProductSize value)
        {
            return value switch
            {
                ProductSize.S => "s",
                ProductSize.M => "m",
                ProductSize.L => "l",
                ProductSize.XL => "xl",
                _ => "unknown"
            };
        }

        public ProductSize ToEnum(string value)
        {
            return value switch
            {
                "s" => ProductSize.S,
                "m" => ProductSize.M,
                "l" => ProductSize.L,
                "xl" => ProductSize.XL,
                _ => ProductSize.Unknown
            };
        }
    }
}
