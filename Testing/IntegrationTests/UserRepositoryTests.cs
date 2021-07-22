using Flow.Core.DomainModels;
using Flow.Infrastructure.DataAccess;
using Flow.Infrastructure.DataAccess.Repositories;
using Mongo2Go;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Testing
{
    public class UserRepositoryTests : IDisposable
    {

        private MongoDbRunner _runner;
        private IMongoClient _client;
        private UserRepository _repository;

        public UserRepositoryTests()
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

        private UserRepository InitUserRepository()
        {
            return new UserRepository(_client);
        }

        // Four users with known properties that can be used by the tests
        private User u1 = new User { UserId = Guid.NewGuid(), FirstName = "Aragorn", LastName = "Son of Arathorn", Age = 90, SignUpTime = DateTime.Now, Description = "The king that will return", ShortDescription = "Strider" };
        private User u2 = new User { UserId = Guid.NewGuid(), FirstName = "Bilbo", LastName = "Baggins", Age = 70, SignUpTime = DateTime.Now, Description = "The previous ring owner", ShortDescription = "Hobbit" };
        private User u3 = new User { UserId = Guid.NewGuid(), FirstName = "Frodo", LastName = "Baggins", Age = 50, SignUpTime = DateTime.Now, Description = "A brave hobbit", ShortDescription = "Hobbit" };
        private User u4 = new User { UserId = Guid.NewGuid(), FirstName = "Gandalf", LastName = "The Grey", Age = 900, SignUpTime = DateTime.Now, Description = "Just tea, thank you", ShortDescription = "Wizard" };

        // Adds the users to the DB
        private async Task InitData()
        {
            await _repository.UpsertUserAsync(u1);
            await _repository.UpsertUserAsync(u2);
            await _repository.UpsertUserAsync(u3);
            await _repository.UpsertUserAsync(u4);
        }

        [Fact]
        public async Task InsertUserWithoutId_ShouldCreateNewUser()
        {
            // Arrange
            string firstName    = "xxx";
            string lastName     = "yyy";
            int age             = 15;

            User user = new User { FirstName=firstName, LastName=lastName, Age=age, SignUpTime=DateTime.Now };
            
            Assert.True(user.UserId == Guid.Empty); // Check integrity of data
            long DBSizeBeforeInsert = await _repository.GetEstimatedNumberOfUsers();

            // Act
            await _repository.UpsertUserAsync(user);
            long DBSizeAfterInsert = await _repository.GetEstimatedNumberOfUsers();

            //Assert
            Assert.Equal(DBSizeBeforeInsert + 1, DBSizeAfterInsert);
            
            var users = await _repository.FindUsersAsync(u => u.FirstName == firstName);
            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task InsertUserWithId_ShouldModifyExistingUser()
        {
            // Arrange
            await InitData();

            User userToChange        = u1;
            userToChange.FirstName   = "Snail";
            userToChange.Age         = 200;

            // Act
            await _repository.UpsertUserAsync(userToChange);

            // Assert
            User changedUser = await _repository.GetUserByIdAsync(u1.UserId);

            Assert.Equal(userToChange.FirstName, changedUser.FirstName);
            Assert.Equal(userToChange.Age, changedUser.Age);
        }
    }
}
