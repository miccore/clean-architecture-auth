using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;
using Miccore.CleanArchitecture.Auth.Application.Handlers.Role.CommandHandlers;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Auth.UnitTest.Role.Commands
{
    public class RoleCommandHandlerTest_Create
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly RoleMockClass _mock;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public RoleCommandHandlerTest_Create(){
            _mock = new RoleMockClass();
        }

        /// <summary>
        /// test role creation with entity mapping return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void RoleCommandHandlerTest_Create_Invalid_Mapping(){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new RoleRepository(mockDb);
            var handler = new CreateRoleCommandHandler(repository);

            var command = new CreateRoleCommand(){};
            command = null;

            // act
             var ex = await Assert.ThrowsAsync<ApplicationException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.MAPPER_ISSUE.ToString());
        }

        /// <summary>
        /// test role creation with successfull creation
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void RoleCommandHandlerTest_Create_successful(){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new RoleRepository(mockDb);
            var handler = new CreateRoleCommandHandler(repository);

            var command = new CreateRoleCommand(){
                Name = "Role 1"
            };

            // act
             var result = await  handler.Handle(command, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Role 1");
            result.CreatedAt.Should().NotBe(0);
            result.UpdatedAt.Should().Be(0);
            result.DeletedAt.Should().Be(0);
        }


        
    }
}