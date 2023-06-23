using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;
using Miccore.CleanArchitecture.Auth.Application.Handlers.Role.CommandHandlers;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Auth.UnitTest.Role.Commands
{
    public class RoleCommandHandlerTest_Delete
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly RoleMockClass _mock;

        /// <summary>
        /// initialisation
        /// </summary>
        public RoleCommandHandlerTest_Delete(){
            _mock = new RoleMockClass();
        }

        /// <summary>
        /// test delete method with not found id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void RoleCommandHandlerTest_Delete_not_found(int id){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new RoleRepository(mockDb);
            var handler = new DeleteRoleCommandHandler(repository);
            var command = new DeleteRoleCommand(id);

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
        public async void RoleCommandHandlerTest_Delete_already_deleted(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.Role(){
                    Id = 1,
                    Name = "Role 1",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var repository = new RoleRepository(mockDb);
            var handler = new DeleteRoleCommandHandler(repository);
            var command = new DeleteRoleCommand(id);

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
        public async void RoleCommandHandlerTest_Delete_successful(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.Role(){
                    Id = 1,
                    Name = "Role 1",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = 0,
                    UpdatedAt = 0
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var repository = new RoleRepository(mockDb);
            var handler = new DeleteRoleCommandHandler(repository);
            var command = new DeleteRoleCommand(id);

            // act
            var result =  await handler.Handle(command, CancellationToken.None);

            // assert
            result.DeletedAt.Should().NotBe(0);
        }
    }
}