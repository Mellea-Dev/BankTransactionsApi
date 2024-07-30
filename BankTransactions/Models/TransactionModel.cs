using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactions.Models
{
    public class TransactionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required]
        public decimal Amount { get; set; }                                                                                                                             

        public string Description { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string FromAccountNumber { get; set; }

        [Required]
        public string ToAccountNumber { get; set; }
    }
}
