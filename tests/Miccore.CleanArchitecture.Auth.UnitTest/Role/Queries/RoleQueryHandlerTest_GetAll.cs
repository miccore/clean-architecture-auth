using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Auth.Application.Handlers.Role.QueryHandlers;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.Role;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;
using Miccore.Pagination.Model;
using Xunit;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;

namespace Miccore.CleanArchitecture.Auth.UnitTest.Role.Queries
{

    /// <summary>
    /// role query handler test class for get all roles
    /// </summary>
    public class RoleQueryTestHandler_GetAll
    {
        /// <summary>
        /// query element
        /// </summary>
        private GetAllRoleQuery _query;
        /// <summary>
        /// mock class
        /// </summary>
        private RoleMockClass _mock;
        private readonly RoleRepository _repository;
        private readonly GetAllRoleQueryHandler _handler;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public RoleQueryTestHandler_GetAll(){
            // query initialisation
            _query = new GetAllRoleQuery(new PaginationQuery());  
            _query.query.paginate = false;
            _query.query.page = 1;
            _query.query.limit = 10;

            // databse d=context
            _mock = new RoleMockClass();

            var mockDbContext = _mock.GetDbContext().Object;
            
            _repository = new RoleRepository(mockDbContext);
            
            _handler = new GetAllRoleQueryHandler(_repository);
        }

        /// <summary>
        /// get roles in empty list and assert return null
        /// </summary>
        [Fact]
        public async void RoleQueryTestHandler_GetAll_ReturnEmptyElements(){
            // arrange

            //act
            // get servie data
            var handle = await _handler.Handle(_query, CancellationToken.None);
            var result = RoleMapper.Mapper.Map<PaginationModel<RoleResponse>>(handle);

            // assert
            result.Items.Should().BeNullOrEmpty();
            result.Items.Count.Should().Be(0);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.TotalItems.Should().Be(0);
            result.TotalPages.Should().Be(0);
        }

        /// <summary>
        /// get roles in empty list and assert return null
        /// </summary>
        [Fact]
        public async void RoleQueryTestHandler_GetAll_ReturnListOfElements_NotPaginated(){
            //arrange
            _mock.GenerateData(9);
            _mock._data.Add(
                new Core.Entities.Role(){
                    Id = 10,
                    Name = "Role 10",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );
            _query.query.limit = 5;
           
            //act
            var handle = await _handler.Handle(_query, CancellationToken.None);
            var result = RoleMapper.Mapper.Map<PaginationModel<RoleResponse>>(handle);
            
            // assert
            result.Items.Should().NotBeNullOrEmpty();
            result.Items.Count.Should().Be(9);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(5);
            result.TotalItems.Should().Be(9);
            result.TotalPages.Should().Be(2);
        }

        /// <summary>
        /// get roles in empty list and assert return null
        /// </summary>
        [Fact]
        public async void RoleQueryTestHandler_GetAll_ReturnListOfElements_Paginated(){
            //arrange
            _mock.GenerateData(9);
            _mock._data.Add(
                new Core.Entities.Role(){
                    Id = 10,
                    Name = "Role 10",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );
            _query.query.paginate = true;
            _query.query.limit = 5;
           
            //act
            var handle = await _handler.Handle(_query, CancellationToken.None);
            var result = RoleMapper.Mapper.Map<PaginationModel<RoleResponse>>(handle);
            
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