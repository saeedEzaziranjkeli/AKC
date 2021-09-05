using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AK.Domain.Models
{
    public class Drug
    {
        public string Id { get; set; }
        [MaxLength(30)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Label { get; set; }
        public string Description { get; set; }
        [Range(0, double.PositiveInfinity)]
        public decimal Price { get; set; }
    }
}
