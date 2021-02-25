namespace Application.Products.Queries
{
    using DeliVeggieSharedLibrary.Models;
    using MediatR;
    internal sealed class GetAllProductByIdQuery : IRequest<ProductDetailsResponse>
    {
        public int ProductId { get; }
        public GetAllProductByIdQuery(int pid)
            => ProductId = pid;
    }
}
