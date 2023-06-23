using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Handlers.Role.QueryHandlers;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.Role;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Auth.UnitTest.Role.Queries
{
    public class RoleQueryTestHandler_GetById
    {
        
        /// <summary>
        /// mock class
        /// </summary>
        private RoleMockClass _mock;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public RoleQueryTestHandler_GetById(){
            // databse d=context
            _mock = new RoleMockClass();
        }


        /// <summary>
        /// get role by id and throw not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void RoleQueryTestHandler_GetById_throw_not_found(int id){
            // arrange
            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new RoleRepository(mockDbContext);
            var handler = new GetRoleByIdQueryHandler(repository);
            var request = new GetRoleByIdQuery(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }


        /// <summary>
        /// get role by id and throw not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void RoleQueryTestHandler_GetById_throw_not_found_with_Data_Deleted(int id){
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
            

            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new RoleRepository(mockDbContext);
            var handler = new GetRoleByIdQueryHandler(repository);
            var request = new GetRoleByIdQuery(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }


        /// <summary>
        /// get role by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(12)]
        public async void RoleQueryTestHandler_GetById_found(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.Role(){
                    Id = 12,
                    Name = "Role 2",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = 0,
                    UpdatedAt = 0
                }
            );
            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new RoleRepository(mockDbContext);
            var handler = new GetRoleByIdQueryHandler(repository);
            var request = new GetRoleByIdQuery(id);

            // act
            var result = await handler.Handle(request, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.Name.Should().BeEquivalentTo("Role 2");
        }
    }
}