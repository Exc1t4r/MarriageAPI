using System;

namespace MarriageAPI.Models
{
    public class Marriage
    {
        public int Id { get; set; }

        public Person Person1 { get; set; }

        public Person Person2 { get; set; }

        public DateTime Date { get; set; }

        public Marriage(Person person1, Person person2)
        {
            Person1 = person1;
            Person2 = person2;

            Date = DateTime.Now;

            GetMaried();
        }

        public void Divorce()
        {
            Person1.BecomeSingle();
            Person2.BecomeSingle();
        }

        private void GetMaried()
        {
            Person1.BecomeMarried();
            Person2.BecomeMarried();
        }

    }
}
