using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Handlers.User.QueryHandlers;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.User;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User.Queries
{
    public class UserQueryTestHandler_GetById
    {
        
        /// <summary>
        /// mock class
        /// </summary>
        private UserMockClass _mock;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public UserQueryTestHandler_GetById(){
            // databse d=context
            _mock = new UserMockClass();
        }


        /// <summary>
        /// get user by id and throw not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserQueryTestHandler_GetById_throw_not_found(int id){
            // arrange
            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDbContext);
            var handler = new GetUserByIdQueryHandler(repository);
            var request = new GetUserByIdQuery(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }


        /// <summary>
        /// get user by id and throw not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void UserQueryTestHandler_GetById_throw_not_found_with_Data_Deleted(int id){
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
            

            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDbContext);
            var handler = new GetUserByIdQueryHandler(repository);
            var request = new GetUserByIdQuery(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }


        /// <summary>
        /// get user by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(12)]
        public async void UserQueryTestHandler_GetById_found(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                    Id = 12,
                    FirstName = "User 2",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = 0,
                    UpdatedAt = 0
                }
            );
            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDbContext);
            var handler = new GetUserByIdQueryHandler(repository);
            var request = new GetUserByIdQuery(id);

            // act
            var result = await handler.Handle(request, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.FirstName.Should().BeEquivalentTo("User 2");
        }
    }
}