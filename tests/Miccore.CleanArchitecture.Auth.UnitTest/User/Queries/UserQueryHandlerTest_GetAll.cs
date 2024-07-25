using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Handlers.User.QueryHandlers;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.User;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.Pagination.Model;
using Xunit;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User.Queries
{

    /// <summary>
    /// user query handler test class for get all users
    /// </summary>
    public class UserQueryTestHandler_GetAll
    {
        private readonly UserRepository _repository;
        private readonly GetAllUserQueryHandler _handler;

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

            var mockDbContext = _mock.GetDbContext().Object;
            
            _repository = new UserRepository(mockDbContext);
            
            _handler = new GetAllUserQueryHandler(_repository);
            
        }

        /// <summary>
        /// get users in empty list and assert return null
        /// </summary>
        [Fact]
        public async void UserQueryTestHandler_GetAll_ReturnEmptyElements(){
            // arrange

            //act
            // get servie data
            var handle = await _handler.Handle(_query, CancellationToken.None);
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
            _mock.GenerateData(10);
            _mock._data.Add(
                new Core.Entities.User(){
                    Id = 10,
                    FirstName = "User 10",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );
            _query.query.limit = 5;
           
            //act
            var handle = await _handler.Handle(_query, CancellationToken.None);
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
            _mock.GenerateData(10);
            _mock._data.Add(
                new Core.Entities.User(){
                    Id = 10,
                    FirstName = "User 10",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );
            _query.query.paginate = true;
            _query.query.limit = 5;
           
            //act
            var handle = await _handler.Handle(_query, CancellationToken.None);
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