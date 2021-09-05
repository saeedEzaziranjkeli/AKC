using System;
using System.Collections.Generic;
using System.Text;
using AK.Application.DTOs;

namespace AK.Application.Queries.Drug
{
    public class DrugGetByIdQuery : DrugQueryBase<DrugDto>
    {
        public DrugGetByIdQuery(DrugDto model) : base(model)
        {
            
        }
    }
}
