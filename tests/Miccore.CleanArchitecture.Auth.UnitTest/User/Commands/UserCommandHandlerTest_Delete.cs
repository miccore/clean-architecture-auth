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
    public class UserCommandHandlerTest_Delete
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly UserMockClass _mock;

        /// <summary>
        /// initialisation
        /// </summary>
        public UserCommandHandlerTest_Delete(){
            _mock = new UserMockClass();
        }

        /// <summary>
        /// test delete method with not found id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_Delete_not_found(int id){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var handler = new DeleteUserCommandHandler(repository);
            var command = new DeleteUserCommand(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }

        /// <summary>
        /// test delete method with already deleted value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_Delete_already_deleted(int id){
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
            var handler = new DeleteUserCommandHandler(repository);
            var command = new DeleteUserCommand(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }

        /// <summary>
        /// test delete method with successfull result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_Delete_successful(int id){
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
            var handler = new DeleteUserCommandHandler(repository);
            var command = new DeleteUserCommand(id);

            // act
            var result =  await handler.Handle(command, CancellationToken.None);

            // assert
            result.DeletedAt.Should().NotBe(0);
        }
    }
}