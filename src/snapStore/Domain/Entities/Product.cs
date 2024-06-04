using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Product: Entity<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<ProductDescription> Descriptions { get; set; }
    public virtual ICollection<ProductReview>? Reviews { get; set; }
    //public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    public virtual ICollection<ProductImage> Images { get; set; }
    public virtual ICollection<ProductSize> Sizes { get; set; }
}

//public class Order : Entity<Guid>
//{
//    public DateTime OrderDate { get; set; }
//    public OrderStatus Status { get; set; }

//    public virtual User User { get; set; }
//    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
//    public virtual Payment Payment { get; set; }
//}

//public class Payment : Entity<Guid>
//{
//    public decimal Amount { get; set; }
//    public string? Description { get; set; }
//    public string OrderCode { get; set; }
//    public DateTime PaymentDate { get; set; }
//    public PaymentMethod PaymentMethod { get; set; }  // Ödeme yöntemi (Kredi Kartı, Banka Transferi vs.)

//    public virtual Order Order { get; set; }
//}

//public class OrderDetail : Entity<Guid>
//{
//    public int Quantity { get; set; }
//    public decimal UnitPrice { get; set; }

//    public virtual Order Order { get; set; }
//    public virtual Product Product { get; set; }
//}

//public enum PaymentMethod
//{
//    CreditCard,
//    BankTransfer,
//    PayPal,
//    CashOnDelivery
//}

//public enum OrderStatus
//{
//    Confirmed,
//    Preparing,
//    Shipped,
//    Completed
//}



