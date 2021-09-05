using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AK.Application.Commands.Drug;
using AK.Application.DTOs;
using AK.Application.Queries.Drug;
using AK.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AK.Application.Handlers.Commands.Drugs
{
    public class DrugCreateCommandHandler : IRequestHandler<DrugCreateCommand, DrugDto>
    {
        private readonly IMapper _mapper;
        private readonly IDrugRepository _repo;

        public DrugCreateCommandHandler(IMapper mapper, IDrugRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<DrugDto> Handle(DrugCreateCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return null;
            var model = _mapper.Map<Domain.Models.Drug>(request);
            var result = await _repo.AddAsync(model);
            return _mapper.Map<DrugDto>(result);
        }
    }
}
