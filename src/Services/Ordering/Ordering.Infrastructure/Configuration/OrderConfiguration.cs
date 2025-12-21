using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(OrderId => OrderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>().WithMany().HasForeignKey(oi => oi.CustomerId).IsRequired();
        builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId).IsRequired();
        builder.ComplexProperty(
            o => o.OrderName,
            nameBuilder =>
            {
                nameBuilder
                    .Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            }
        );
        builder.ComplexProperty(
            o => o.ShippingAddress,
            addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.EmailAddress).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(200).IsRequired();
                addressBuilder.Property(a => a.AddressLine2).HasMaxLength(200);
                addressBuilder.Property(a => a.Country).HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(5);
            }
        );
        builder.ComplexProperty(
            o => o.BillingAddress,
            addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.EmailAddress).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(200).IsRequired();
                addressBuilder.Property(a => a.AddressLine2).HasMaxLength(200);
                addressBuilder.Property(a => a.Country).HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(5);
            }
        );

        builder.ComplexProperty(
            o => o.Payment,
            paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardHolderName).HasMaxLength(100).IsRequired();
                paymentBuilder.Property(p => p.CardNumber).HasMaxLength(24).IsRequired();
                paymentBuilder.Property(p => p.ExpirationDate).HasMaxLength(10).IsRequired();
                paymentBuilder.Property(p => p.CVV).HasMaxLength(3).IsRequired();
            }
        );

        builder
            .Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Pending)
            .HasConversion(
                status => status.ToString(),
                dbStatus => Enum.Parse<OrderStatus>(dbStatus)
            );
        builder.Property(o => o.TotalPrice);
    }
}
