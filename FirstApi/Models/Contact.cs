using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi
{
    public class Contact
    {
        public string Name { get; set; }

        public string phone { get; set; }

        [Key]
        public string email { get; set; }
    }
}
