using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace D6.Models
{
    public class Category
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string Category_Name { get; set; }

        //public virtual List<Book_Details> Books { get; set; }



    }
}
