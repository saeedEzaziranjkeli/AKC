using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AK.Application.DTOs;
using AK.Application.Queries.Drug;
using AK.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AK.Application.Handlers.Queries.Drug
{
    public class DrugGetAllQueryHandler : IRequestHandler<DrugGetAllQuery, List<DrugDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDrugRepository _repo;

        public DrugGetAllQueryHandler(IMapper mapper, IDrugRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<List<DrugDto>> Handle(DrugGetAllQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return null;
            var result = await _repo.GetAllAsync();
            var modelDto = _mapper.Map<List<DrugDto>>(result);
            return modelDto;
        }
    }
}
