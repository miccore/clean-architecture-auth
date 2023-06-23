using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Application.Handlers.User.CommandHandlers;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User.Commands
{
    public class UserCommandHandlerTest_Create
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly UserMockClass _mock;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public UserCommandHandlerTest_Create(){
            _mock = new UserMockClass();
        }

        /// <summary>
        /// test user creation with entity mapping return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void UserCommandHandlerTest_Create_Invalid_Mapping(){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDb);
            var roleRepository = new RoleRepository(mockDb);
            var handler = new CreateUserCommandHandler(repository, roleRepository);

            var command = new CreateUserCommand(){};
            command = null;

            // act
             var ex = await Assert.ThrowsAsync<ApplicationException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.MAPPER_ISSUE.ToString());
        }

        /// <summary>
        /// test user creation with successfull creation
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void UserCommandHandlerTest_Create_successful(){
            // arrange
             _mock._role_data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.Role(){
                    Id = 1,
                    Name = "User"
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var mockRole = _mock.GetRoleDbContext().Object;
            var repository = new UserRepository(mockDb);
            var roleRepository = new RoleRepository(mockRole);
            var handler = new CreateUserCommandHandler(repository, roleRepository);

            var command = new CreateUserCommand(){
                FirstName = "User 1",
                Password = "Password",
                RoleId = 1
            };

            // act
             var result = await  handler.Handle(command, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("User 1");
            result.CreatedAt.Should().NotBe(0);
            result.UpdatedAt.Should().Be(0);
            result.DeletedAt.Should().Be(0);
        }


        
    }
}