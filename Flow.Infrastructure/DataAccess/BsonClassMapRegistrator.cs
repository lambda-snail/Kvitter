using Flow.Core.DomainModels;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Flow.Infrastructure.DataAccess
{
    public static class BsonClassMapRegistrator
    {
        public static void RegisterBsonClassMaps()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                BsonClassMap.RegisterClassMap<User>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(user => user.UserId);
                    cm.UnmapProperty(user => user.Friends);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Post)))
            {
                BsonClassMap.RegisterClassMap<Post>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIdMember(cm.GetMemberMap(post => post.PostId));
                    cm.IdMemberMap.SetIgnoreIfDefault(true);
                    cm.IdMemberMap.SetIdGenerator(CombGuidGenerator.Instance);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(FriendRelation)))
            {
                BsonClassMap.RegisterClassMap<FriendRelation>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIdMember(cm.GetMemberMap(r => r.Id));
                    cm.IdMemberMap.SetIgnoreIfDefault(true);
                });
            }
        }
    }
}
