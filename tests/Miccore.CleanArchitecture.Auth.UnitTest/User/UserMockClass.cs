using System.Collections.Generic;
using System.Linq;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Data;
using Miccore.Pagination.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User
{
    public class UserMockClass
    {   

        public List<Miccore.CleanArchitecture.Auth.Core.Entities.User> _data;
        public List<Miccore.CleanArchitecture.Auth.Core.Entities.Role> _role_data;

        public UserMockClass(){
            // iquerable data
            _data = new List<Core.Entities.User>(){
               
            };
            _role_data = new List<Core.Entities.Role>(){
               
            };
        }

        /// <summary>
        /// mock database context with empty data
        /// </summary>
        /// <returns></returns>
         public Mock<AuthApplicationDbContext> GetDbContext(){
            
            // context database setup
            var options = new DbContextOptionsBuilder<AuthApplicationDbContext>().Options;
            var mockDbContext = new Mock<AuthApplicationDbContext>(options);
            mockDbContext.SetupSequence(x => x.Set<Miccore.CleanArchitecture.Auth.Core.Entities.User>())
                        .ReturnsDbSet(_data);
            
            // return mock
            return mockDbContext;
        }

        /// <summary>
        /// mock database context with empty data roles
        /// </summary>
        /// <returns></returns>
         public Mock<AuthApplicationDbContext> GetRoleDbContext(){
            
            // context database setup
            var options = new DbContextOptionsBuilder<AuthApplicationDbContext>().Options;
            var mockDbContext = new Mock<AuthApplicationDbContext>(options);
            mockDbContext.SetupSequence(x => x.Set<Miccore.CleanArchitecture.Auth.Core.Entities.Role>())
                        .ReturnsDbSet(_role_data);
            
            // return mock
            return mockDbContext;
        }

    }
}