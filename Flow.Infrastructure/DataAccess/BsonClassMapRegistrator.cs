using Flow.Core.DomainModels;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
