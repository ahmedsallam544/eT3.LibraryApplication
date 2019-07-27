using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace D6.Models
{
    public class User_Details
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime Date_Of_Birth { get; set; }

        public long Contact_Number { get; set; }
       


    }
}
