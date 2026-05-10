using System.ComponentModel.DataAnnotations.Schema;

namespace SportsShoesEcommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int AddressId { get; set; }
        public Address? Address { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public string OrderStatus { get; set; } = "Pending";

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ICollection<OrderItem>? OrderItems { get; set; }

        public Payment? Payment { get; set; }

    }
}
