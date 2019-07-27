using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace D6.Models
{
    public class Book_Details
    {
        [Key]
        public int ID { get; set; }
        public string Book_Title { get; set; }

        public DateTime Publication_year { get; set; }

        public string Language { get; set; }

        public int No_Of_Copies_Actual { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int Category_Type { get; set; }

        public int No_Of_Copies_Current { get; set; }

        public virtual Category Category { get; set; }

    }
}
