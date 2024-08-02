using AutoMapper;
using MediatR;
using OtaghakChallenge.Application.Interfaces;
using OtaghakChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.Features.Products.Command
{
    public class ProductCommandHandler : IRequestHandler<ProductCommand, int>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly ISmsServices _smsServices;
        public ProductCommandHandler(IRepository<Product> repository, IMapper mapper, ICurrentUser currentUser, ISmsServices smsServices)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
            _smsServices = smsServices;
        }

        public async Task<int> Handle(ProductCommand request, CancellationToken cancellationToken)
        {
            Product newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
            };
            await _repository.AddAsync(newProduct, cancellationToken);
            await _repository.ContextSaveChangesAsync(cancellationToken);

            await _smsServices.SendSmsAsync(_currentUser.PhoneNumber, cancellationToken);

            return newProduct.Id;
        }
    }
}
