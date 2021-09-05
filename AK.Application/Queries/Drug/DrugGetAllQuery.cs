using System;
using System.Collections.Generic;
using System.Text;
using AK.Application.DTOs;

namespace AK.Application.Queries.Drug
{
    public class DrugGetAllQuery : DrugQueryBase<IReadOnlyList<DrugDto>>
    {
        public DrugGetAllQuery() : base()
        {
            
        }
    }
}
