using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AK.Application.Commands.Drug;
using AK.Application.DTOs;
using AK.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AK.Application.Handlers.Commands.Drugs
{
    public class DrugDeleteCommandHandler : IRequestHandler<DrugDeleteCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IDrugRepository _repo;

        public DrugDeleteCommandHandler(IMapper mapper, IDrugRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<bool> Handle(DrugDeleteCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return false;
            var model = _mapper.Map<Domain.Models.Drug>(request);
            var result = await _repo.DeleteAsync(model);
            return result;
        }
    }
}
