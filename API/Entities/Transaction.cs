using System;

namespace API.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime TimeStamp { get; set; }
        public float TotalPrice { get; set; }
        public bool Paid { get; set; }
    }
}