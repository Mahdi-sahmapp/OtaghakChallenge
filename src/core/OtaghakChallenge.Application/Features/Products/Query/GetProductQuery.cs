using MediatR;


namespace OtaghakChallenge.Application.Features.Products.Query
{
    public class GetProductQuery:IRequest<List<ProductDto>>
    {
    }
}
