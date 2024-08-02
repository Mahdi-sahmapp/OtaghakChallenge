using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OtaghakChallenge.Application.Interfaces;
using OtaghakChallenge.Domain.Entities;
using OtaghakChallenge.Domain.Enums;
using OtaghakChallenge.Persistence.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.Features.Products.Query
{
    public class GetProductListHandler : IRequestHandler<GetProductQuery, List<ProductDto>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetProductListHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products1 = await _repository.GetAll().ToListAsync(cancellationToken);
            var products = await _repository.GetAll().Where(a=>a.Status == Status.Active).ToListAsync(cancellationToken);

            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
