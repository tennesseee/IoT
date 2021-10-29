using System.Text.Json.Serialization;


namespace IoTApp
{
    class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Amount { get; set; }

        [JsonConstructor]
        public Item (int id, string itemName, int amount)
        {
            Id = id;
            ItemName = itemName;
            Amount = amount;
        }

        public override string ToString()
        {
            return "Id " + this.Id + "ItemName " + this.ItemName + "Amount " + this.Amount;
        }
    }
}
