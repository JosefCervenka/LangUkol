using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentsSystem.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public IdentityUser User { get; set; }
        public bool IsPaid { get; set; }
        public DateTime Date { get; set; }
    }
}
