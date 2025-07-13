using SQLite;

namespace ShopInventory.Models
{
    [Table("PurchasedItems")]
    public class PurchasedItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        [NotNull, MaxLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [NotNull]
        public int Quantity { get; set; }

        [NotNull]
        public decimal Price { get; set; }
    }

    [Table("SoldItems")]
    public class SoldItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        [NotNull, MaxLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [NotNull]
        public int Quantity { get; set; }

        [NotNull]
        public decimal Price { get; set; }
    }
}