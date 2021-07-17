using Flow.Core.DomainModels;
using MediatR;

namespace Flow.Core.Mediate.InsertPost
{
    public class InsertPostRequest : IRequest
    {
        public Post Post { get; set; }
    }
}
