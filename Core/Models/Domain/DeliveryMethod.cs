﻿using System.ComponentModel.DataAnnotations;

namespace Core.Models.Domain
{
    public class DeliveryMethod : BaseSimpleEntity
    {
        [Required]
        public string ShortName { get; set; } = string.Empty;
        [Required]
        public string DeliveryTime { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }
}
