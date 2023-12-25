using Store.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.WebAPI.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
