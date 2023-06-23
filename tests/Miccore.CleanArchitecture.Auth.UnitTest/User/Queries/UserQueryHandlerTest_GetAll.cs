using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Miccore.CleanArchitecture.Auth.Api.Controllers;
using Miccore.CleanArchitecture.Auth.Application.Handlers.User.QueryHandlers;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.User;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Repositories;
using Miccore.Pagination.Model;
using Moq;
using Xunit;
using System;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Data;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User.Queries
{
    
    /// <summary>
    /// user query handler test class for get all users
    /// </summary>
    public class UserQueryTestHandler_GetAll
    {
        /// <summary>
        /// query element
        /// </summary>
        private GetAllUserQuery _query;
        /// <summary>
        /// mock class
        /// </summary>
        private UserMockClass _mock;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public UserQueryTestHandler_GetAll(){
            // query initialisation
            _query = new GetAllUserQuery(new PaginationQuery());  
            _query.query.paginate = false;
            _query.query.page = 1;
            _query.query.limit = 10;

            // databse d=context
            _mock = new UserMockClass();
            
        }

        /// <summary>
        /// get users in empty list and assert return null
        /// </summary>
        [Fact]
        public async void UserQueryTestHandler_GetAll_ReturnEmptyElements(){
            // arrange
            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDbContext);
            var handler = new GetAllUserQueryHandler(repository);

            //act
            // get servie data
            var handle = await handler.Handle(_query, CancellationToken.None);
            var result = UserMapper.Mapper.Map<PaginationModel<UserResponse>>(handle);

            // assert
            result.Items.Should().BeNullOrEmpty();
            result.Items.Count.Should().Be(0);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.TotalItems.Should().Be(0);
            result.TotalPages.Should().Be(0);
        }

        /// <summary>
        /// get users in empty list and assert return null
        /// </summary>
        [Fact]
        public async void UserQueryTestHandler_GetAll_ReturnListOfElements_NotPaginated(){
            //arrange
            for (int i = 0; i < 9; i++)
            {
                _mock._data.Add(
                        new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                            Id = i,
                            FirstName = "User " + i,
                            CreatedAt = DateUtils.GetCurrentTimeStamp(),
                            DeletedAt = 0,
                            UpdatedAt = 0
                        }
                );
            }
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                    Id = 10,
                    FirstName = "User 10",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );

            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDbContext);
            var handler = new GetAllUserQueryHandler(repository);
            _query.query.limit = 5;
           
            //act
            var handle = await handler.Handle(_query, CancellationToken.None);
            var result = UserMapper.Mapper.Map<PaginationModel<UserResponse>>(handle);
            
            
            // assert
            result.Items.Should().NotBeNullOrEmpty();
            result.Items.Count.Should().Be(9);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(5);
            result.TotalItems.Should().Be(9);
            result.TotalPages.Should().Be(2);
        }

        /// <summary>
        /// get users in empty list and assert return null
        /// </summary>
        [Fact]
        public async void UserQueryTestHandler_GetAll_ReturnListOfElements_Paginated(){
            //arrange
            for (int i = 0; i < 9; i++)
            {
                _mock._data.Add(
                        new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                            Id = i,
                            FirstName = "User " + i,
                            CreatedAt = DateUtils.GetCurrentTimeStamp(),
                            DeletedAt = 0,
                            UpdatedAt = 0
                        }
                );
            }
            _mock._data.Add(
                new Miccore.CleanArchitecture.Auth.Core.Entities.User(){
                    Id = 10,
                    FirstName = "User 10",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );

            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new UserRepository(mockDbContext);
            var handler = new GetAllUserQueryHandler(repository);
            _query.query.paginate = true;
            _query.query.limit = 5;
           
            //act
            var handle = await handler.Handle(_query, CancellationToken.None);
            var result = UserMapper.Mapper.Map<PaginationModel<UserResponse>>(handle);
            
            
            // assert
            result.Items.Should().NotBeNullOrEmpty();
            result.Items.Count.Should().Be(5);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(5);
            result.TotalItems.Should().Be(9);
            result.TotalPages.Should().Be(2);
        }

        
    }
}