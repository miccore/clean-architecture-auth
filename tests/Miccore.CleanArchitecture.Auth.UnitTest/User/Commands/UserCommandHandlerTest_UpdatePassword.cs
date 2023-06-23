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
using BC = BCrypt.Net.BCrypt;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User.Commands
{
    public class UserCommandHandlerTest_UpdatePassword
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly UserMockClass _mock;

        /// <summary>
        /// initialisation
        /// </summary>
        public UserCommandHandlerTest_UpdatePassword(){
            _mock = new UserMockClass();
        }

        /// <summary>
        /// test update method with not found id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_UpdatePassword_not_found(int id){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var handler = new UpdateUserPasswordCommandHandler(repository);
            var command = new UpdateUserPasswordCommand(){
                Id = id
            };

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }

        /// <summary>
        /// test update method with deleted value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_UpdatePassword_deleted(int id){
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
            var handler = new UpdateUserPasswordCommandHandler(repository);
            var command = new UpdateUserPasswordCommand(){
                Id = id
            };

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }

        /// <summary>
        /// test update method with password don't match result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_UpdatePassword_dont_match(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                    Id = 1,
                    FirstName = "User 1",
                    Password = BC.HashPassword("password"),
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = 0,
                    UpdatedAt = 0
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var handler = new UpdateUserPasswordCommandHandler(repository);
            var command = new UpdateUserPasswordCommand(){
                Id = id,
                NewPassword = "Password",
                OldPassword = "passord",
            };

              // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.USER_NOT_FOUND_OR_PASSWORD_INCORRECT.ToString());
        }

        /// <summary>
        /// test update method with successfull result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserCommandHandlerTest_UpdatePassword_successfull(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                    Id = 1,
                    FirstName = "User 1",
                    Password = BC.HashPassword("password"),
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = 0,
                    UpdatedAt = 0
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var handler = new UpdateUserPasswordCommandHandler(repository);
            var command = new UpdateUserPasswordCommand(){
                Id = 1,
                NewPassword = "Password",
                OldPassword = "password",
            };

            // act
            var result = await  handler.Handle(command, CancellationToken.None);

            // assert
           result.Id.Should().Be(id);
           result.UpdatedAt.Should().NotBe(0);
        }
    }
}