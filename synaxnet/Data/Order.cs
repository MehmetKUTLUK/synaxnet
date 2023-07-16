using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace synaxnet.Data;
public enum OrderStatus
{
    New, Cancelled, Cargo, Completed,Again
}
public class Order
{ 
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required string Address { get; set; }
    public required string City { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public string IpAdres { get; set; }
   
   public DateTime Date { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; }
}
public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        
    }
}