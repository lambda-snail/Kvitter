using Flow.Core.DomainModels;
using Flow.Infrastructure.DataAccess;
using Flow.Infrastructure.DataAccess.Repositories;
using Mongo2Go;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Testing.IntegrationTests
{
    // TODO: Refactor tests to avoid code duplication!
    public class FriendRelationRepositoryTest : IDisposable
    {
        private MongoDbRunner _runner;
        private IMongoClient _client;
        private FriendRelationRepository _repository;

        public FriendRelationRepositoryTest()
        {
            _runner = MongoDbRunner.Start();
            BsonClassMapRegistrator.RegisterBsonClassMaps();
            _client = new MongoClient(_runner.ConnectionString);
            _repository = InitUserRepository();
        }

        public void Dispose()
        {
            _client = null;
            _runner.Dispose();
            _runner = null;
        }

        private FriendRelationRepository InitUserRepository()
        {
            return new FriendRelationRepository(_client);
        }

        private FriendRelation f1 = new FriendRelation { Users = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, ConnectedDate = DateTime.Parse("2021-01-01") };
        private FriendRelation f2 = new FriendRelation { Users = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, ConnectedDate = DateTime.Parse("2021-02-02") };
        private FriendRelation f3 = new FriendRelation { Users = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, ConnectedDate = DateTime.Parse("2021-03-03") };

        // Adds the users to the DB
        private async Task InitData()
        {
            await _repository.AddFriendRelationAsync(f1);
            await _repository.AddFriendRelationAsync(f2);
            await _repository.AddFriendRelationAsync(f3);
        }

        [Fact]
        public async Task AddNewRelationship_ShouldIncreaseDBSize()
        {
            // Arrange
            await InitData();
            long initialDbSize = await _repository.GetEstimatedCollectionSize();
            FriendRelation r = new FriendRelation { Users = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, ConnectedDate = DateTime.Parse("2021-04-04") };

            // Act
            await _repository.AddFriendRelationAsync(r);
            long modifiedDbSize = await _repository.GetEstimatedCollectionSize();

            // Assert
            Assert.Equal(modifiedDbSize, 1 + initialDbSize);
        }

        [Fact]
        public async Task AddNewRelationship_ShouldBeRetreivableUsingTwoIds()
        {
            // Arrange
            await InitData();
            FriendRelation r = new FriendRelation { Users = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, ConnectedDate = DateTime.Parse("2021-04-04") };

            // Act
            await _repository.AddFriendRelationAsync(r);
            Assert.Equal(4, await _repository.GetEstimatedCollectionSize()); // Check integrity of db
            IEnumerable<FriendRelation> insertedRelation1 = await _repository.GetByUserId(r.Users[0]);
            IEnumerable<FriendRelation> insertedRelation2 = await _repository.GetByUserId(r.Users[1]);

            // Assert
            foreach (var relationEnumerable in new List<IEnumerable<FriendRelation>> { insertedRelation1, insertedRelation2 })
            {
                foreach (var relation in relationEnumerable)
                {
                    Assert.True(r.Users.Contains(relation.Users[0]) || r.Users.Contains(relation.Users[1]));
                }
            }
        }
    }
}
