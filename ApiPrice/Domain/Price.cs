using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPrice.Domain
{
    public class Price
    {
        [Key]

        public string Code { get; set; }
        public decimal Value{ get; set; }
   
        
    }
}
