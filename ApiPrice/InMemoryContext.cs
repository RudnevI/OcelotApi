using ApiPrice.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrice
{

    public static class InMemoryContext
    {
        public static ConcurrentDictionary<string, Price> Dictionary = new();


        static InMemoryContext()
        {
            Dictionary.GetOrAdd("A", new Price {Code="A", Value=114.99M});
            Dictionary.GetOrAdd("B", new Price { Code = "B", Value = 156.99M });
            Dictionary.GetOrAdd("C", new Price { Code = "C", Value = 99.99M });
            Dictionary.GetOrAdd("T", new Price { Code = "T", Value = 103.99M });


        }

        public static IEnumerable<Price> GetPrices()
        {
            return Dictionary.Values.AsEnumerable();
        }

        public static Price GetPriceByCode(string code)
        {
            return Dictionary.Values.AsEnumerable().FirstOrDefault(p => p.Code == code);
        }

        public static bool AddPrice(Price price)
        {
            PriceValidation(price);
             return Dictionary.TryAdd(price.Code, price);
        }

        public static bool DeletePrice(Price price)
        {
            PriceValidation(price);
            return Dictionary.TryRemove(price.Code, out price);
        }

        public static void PriceValidation(Price price)
        {
            
            if (price is null)
            {
                throw new ArgumentNullException(nameof(price), "invalid_price_value");
            }
        }

        public static bool UpdatePrice(Price price)
        {
            PriceValidation(price);
            Price currentPrice = Dictionary.Values.AsEnumerable().FirstOrDefault(p => p.Code == price.Code);
            return Dictionary.TryUpdate(price.Code, price, currentPrice);
        }

    }
}
