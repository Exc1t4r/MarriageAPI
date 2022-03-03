using System;
using System.ComponentModel.DataAnnotations;

namespace MarriageAPI.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Too long!")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Too long!")]
        public string LastName { get; set; }

        [Required]
        [Range(0, 999999999999, ErrorMessage = "Invalid; Max 12 digits")]
        public int PersonalCode { get; set; }

        public bool Married { get; set; } = false;

        public void BecomeSingle()
        {
            Married = false;
        }

        public void BecomeMarried()
        {
            Married = true;
        }

    }
}
