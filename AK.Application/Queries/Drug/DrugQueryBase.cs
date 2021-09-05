using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AK.Application.DTOs;
using MediatR;

namespace AK.Application.Queries.Drug
{
    public class DrugQueryBase<T> : IRequest<T>
        where T : class
    {
        public DrugQueryBase(DrugDto model)
        {
            Code = model.Code;
            Label = model.Label;
            Description = model.Description;
            Price = model.Price;
        }
        public DrugQueryBase()
        {

        }
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
