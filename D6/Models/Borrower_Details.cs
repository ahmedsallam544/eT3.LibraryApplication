using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace D6.Models
{
    public class Borrower_Details
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("Book_Details")]
        public int Book_ID { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string User_ID { get; set; }

        public DateTime Borrowed_From_Date { get; set; }

        public DateTime Borrowed_To_Date { get; set; }

        public DateTime Actual_Return_date { get; set; }


    }
}
