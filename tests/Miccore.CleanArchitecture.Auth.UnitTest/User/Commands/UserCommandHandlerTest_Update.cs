using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Application.Handlers.User.CommandHandlers;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User.Commands
{
    public class UserCommandHandlerTest_Update
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly UserMockClass _mock;

        /// <summary>
        /// initialisation
        /// </summary>
        public UserCommandHandlerTest_Update(){
            _mock = new UserMockClass();
        }

        /// <summary>
        /// test user update with entity mapping return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void UserCommandHandlerTest_Update_Invalid_Mapping(){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var handler = new UpdateUserCommandHandler(repository);

            var command = new UpdateUserCommand(){};
            command = null;

            // act
             var ex = await Assert.ThrowsAsync<ApplicationException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.MAPPER_ISSUE.ToString());
        }

        /// <summary>
        /// test update method with not found id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_Update_not_found(int id){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var handler = new UpdateUserCommandHandler(repository);
            var command = new UpdateUserCommand(){
                Id = id
            };

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.USER_NOT_FOUND.ToString());
        }

        /// <summary>
        /// test update method with deleted value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_Update_deleted(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                    Id = 1,
                    FirstName = "User 1",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var handler = new UpdateUserCommandHandler(repository);
            var command = new UpdateUserCommand(){
                Id = id
            };

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.USER_NOT_FOUND.ToString());
        }

        /// <summary>
        /// test update method with successfull result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_Update_successfull(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                    Id = 1,
                    FirstName = "User 1",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = 0,
                    UpdatedAt = 0
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var handler = new UpdateUserCommandHandler(repository);
            var command = new UpdateUserCommand(){
                Id = id,
                FirstName = "User 1 updated"
            };

            // act
            var result = await  handler.Handle(command, CancellationToken.None);

            // assert
           result.Id.Should().Be(id);
           result.FirstName.Should().Be("User 1 updated");
           result.UpdatedAt.Should().NotBe(0);
        }
    }
}