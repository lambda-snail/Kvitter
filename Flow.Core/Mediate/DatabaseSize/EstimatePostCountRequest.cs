using MediatR;

namespace Flow.Core.Mediate.DatabaseSize
{
    public class EstimatePostCountRequest : IRequest<long> {}
}
