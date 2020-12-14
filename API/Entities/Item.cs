using System;

namespace API.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string BaseTea { get; set; }
        public string Flavor { get; set; }
        public string Size { get; set; }
        public float Price { get; set; }
    }
}