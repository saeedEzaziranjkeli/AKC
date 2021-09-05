using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AK.Application.DTOs;
using AK.Application.Queries.Drug;
using AK.Application.Queries.User;
using AK.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AK.Application.Handlers.Queries.Drug
{
    public class DrugGetByIdQueryHandler : IRequestHandler<DrugGetByIdQuery, DrugDto>
    {
        private readonly IMapper _mapper;
        private readonly IDrugRepository _repo;

        public DrugGetByIdQueryHandler(IMapper mapper, IDrugRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<DrugDto> Handle(DrugGetByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return null;
            var model = _mapper.Map<Domain.Models.Drug>(request);
            var result = await _repo.GetByIdAsync(model.Id);
            var modelDto = _mapper.Map<DrugDto>(result);
            return modelDto;
        }
    }
}
