using System.Text.Json.Serialization;


namespace RequestHandlerAPI
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Amount { get; set; }

        [JsonConstructor]
        public Item(int id, string itemName, int amount)
        {
            Id = id;
            ItemName = itemName;
            Amount = amount;
        }
    }
}
