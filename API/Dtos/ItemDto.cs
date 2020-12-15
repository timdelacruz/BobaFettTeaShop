namespace API.Dtos
{
    public class ItemDto
    {
        public int TransactionId { get; set; }
        public string BaseTea { get; set; }
        public string Flavor { get; set; }
        public string Size { get; set; }
        public float Price { get; set; }
    }
}