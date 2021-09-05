using System;
using System.Collections.Generic;
using System.Text;
using AK.Domain.Interfaces;
using AK.Domain.Models;
using AK.Infrastructure.Data;

namespace AK.Infrastructure.Repository
{
    public class DrugRepository : Repository<Drug> , IDrugRepository
    {
        public DrugRepository(EFDbContext dbContext) : base(dbContext)
        {
        }
    }
}
