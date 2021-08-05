using Flow.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flow.Core.Contracts
{
    public interface IFriendRelationRepository
    {
        Task AddFriendRelationAsync(FriendRelation r);
        Task<IEnumerable<FriendRelation>> GetByUserId(Guid userId);
        Task<long> GetEstimatedCollectionSize();
    }
}