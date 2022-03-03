using System;
using System.ComponentModel.DataAnnotations;

namespace MarriageAPI.Models
{
    public class Marriage
    {
        public int Id { get; set; }

        public Person Person1 { get; set; }

        public Person Person2 { get; set; }

        public DateTime Date { get; set; }
    }
}
