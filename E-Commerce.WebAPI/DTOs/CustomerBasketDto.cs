using Store.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.WebAPI.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }

    }
}
