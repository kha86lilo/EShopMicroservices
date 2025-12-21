namespace Ordering.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Customer> GetCustomers()
    {
        return new List<Customer>
        {
            Customer.Create(
                CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")),
                "John Doe",
                "kha86lilo2@test.com"
            ),
            Customer.Create(
                CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")),
                "Jane Smith",
                "jane.smith@example.com"
            ),
        };
    }

    public static IEnumerable<Product> GetProducts()
    {
        return new List<Product>
        {
            Product.Create(
                ProductId.Of(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")),
                "iPhone 14 Pro",
                "Apple iPhone 14 Pro 256GB",
                999.99m
            ),
            Product.Create(
                ProductId.Of(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")),
                "Samsung Galaxy S23",
                "Samsung Galaxy S23 128GB",
                799.99m
            ),
            Product.Create(
                ProductId.Of(new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc")),
                "Sony WH-1000XM5",
                "Wireless Noise Cancelling Headphones",
                349.99m
            ),
            Product.Create(
                ProductId.Of(new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd")),
                "MacBook Pro 14",
                "Apple MacBook Pro 14-inch M2 Pro",
                1999.99m
            ),
        };
    }

    public static IEnumerable<Order> GetOrders()
    {
        var address1 = Address.Of(
            "John",
            "Doe",
            "kha86lilo2@test.com",
            "123 Main Street",
            "Apt 4B",
            "USA",
            "California",
            "90210"
        );

        var address2 = Address.Of(
            "Jane",
            "Smith",
            "jane.smith@example.com",
            "456 Oak Avenue",
            null,
            "USA",
            "New York",
            "10001"
        );

        var payment1 = Payment.Of(
            "1234567890123456",
            "John Doe",
            DateTime.Now.AddYears(2),
            "123",
            "Visa"
        );

        var payment2 = Payment.Of(
            "9876543210987654",
            "Jane Smith",
            DateTime.Now.AddYears(3),
            "456",
            "MasterCard"
        );

        var order1 = Order.Create(
            OrderId.Of(new Guid("33333333-3333-3333-3333-333333333333")),
            OrderName.Of("ORD_001"),
            CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")),
            address1,
            address1,
            payment1,
            new List<OrderItem>()
        );

        var order2 = Order.Create(
            OrderId.Of(new Guid("44444444-4444-4444-4444-444444444444")),
            OrderName.Of("ORD_002"),
            CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")),
            address2,
            address2,
            payment2,
            new List<OrderItem>()
        );

        return new List<Order> { order1, order2 };
    }

    public static IEnumerable<OrderItem> GetOrderItems()
    {
        // OrderItem 1: iPhone 14 Pro x2 for Order 1
        var orderItem1 = (OrderItem)
            Activator.CreateInstance(
                typeof(OrderItem),
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                new object[]
                {
                    ProductId.Of(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")),
                    OrderId.Of(new Guid("33333333-3333-3333-3333-333333333333")),
                    999.99m,
                    2,
                },
                null
            )!;
        orderItem1.Id = OrderItemId.Of(new Guid("11111111-aaaa-1111-aaaa-111111111111"));

        // OrderItem 2: Sony WH-1000XM5 x1 for Order 1
        var orderItem2 = (OrderItem)
            Activator.CreateInstance(
                typeof(OrderItem),
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                new object[]
                {
                    ProductId.Of(new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc")),
                    OrderId.Of(new Guid("33333333-3333-3333-3333-333333333333")),
                    349.99m,
                    1,
                },
                null
            )!;
        orderItem2.Id = OrderItemId.Of(new Guid("22222222-cccc-2222-cccc-222222222222"));

        // OrderItem 3: MacBook Pro 14 x1 for Order 2
        var orderItem3 = (OrderItem)
            Activator.CreateInstance(
                typeof(OrderItem),
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                new object[]
                {
                    ProductId.Of(new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd")),
                    OrderId.Of(new Guid("44444444-4444-4444-4444-444444444444")),
                    1999.99m,
                    1,
                },
                null
            )!;
        orderItem3.Id = OrderItemId.Of(new Guid("33333333-dddd-3333-dddd-333333333333"));

        // OrderItem 4: Samsung Galaxy S23 x1 for Order 2
        var orderItem4 = (OrderItem)
            Activator.CreateInstance(
                typeof(OrderItem),
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                new object[]
                {
                    ProductId.Of(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")),
                    OrderId.Of(new Guid("44444444-4444-4444-4444-444444444444")),
                    799.99m,
                    1,
                },
                null
            )!;
        orderItem4.Id = OrderItemId.Of(new Guid("44444444-bbbb-4444-bbbb-444444444444"));

        return new List<OrderItem> { orderItem1, orderItem2, orderItem3, orderItem4 };
    }
}
