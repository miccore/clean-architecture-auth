using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Application.Handlers.User.CommandHandlers;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;
using Miccore.CleanArchitecture.Auth.UnitTest.Role;
using Xunit;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User.Commands
{
    public class UserCommandHandlerTest_Create
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly UserMockClass _mock;
        private readonly RoleMockClass _roleMock;
        private readonly UserRepository _repository;
        private readonly RoleRepository _roleRepository;
        private readonly CreateUserCommandHandler _handler;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public UserCommandHandlerTest_Create(){
            _mock = new UserMockClass();
            _roleMock = new RoleMockClass();

            var mockDb = _mock.GetDbContext().Object;
            var rolemockdb = _roleMock.GetDbContext().Object;
            
            _repository = new UserRepository(mockDb);
            
            _roleRepository = new RoleRepository(rolemockdb);
            
            _handler = new CreateUserCommandHandler(_repository, _roleRepository);
        }

        /// <summary>
        /// test user creation with entity mapping return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void UserCommandHandlerTest_Create_Invalid_Mapping(){
            // arrange
            var command = new CreateUserCommand(){};
            command = null;

            // act
             var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(command, CancellationToken.None));

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
            _roleMock.GenerateData(2);

            var command = new CreateUserCommand(){
                FirstName = "User 1",
                Password = "Password",
                RoleId = 1
            };

            // act
             var result = await  _handler.Handle(command, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("User 1");
            result.CreatedAt.Should().NotBe(0);
            result.UpdatedAt.Should().BeNull();
            result.DeletedAt.Should().BeNull();
        }


        
    }
}