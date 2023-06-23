using System.Collections.Generic;
using System.Linq;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Data;
using Miccore.Pagination.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Auth.UnitTest.Role
{
    public class RoleMockClass
    {   

        public List<Miccore.CleanArchitecture.Auth.Core.Entities.Role> _data;

        public RoleMockClass(){
            // iquerable data
            _data = new List<Core.Entities.Role>(){
               
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
            mockDbContext.SetupSequence(x => x.Set<Miccore.CleanArchitecture.Auth.Core.Entities.Role>())
                        .ReturnsDbSet(_data);
            
            // return mock
            return mockDbContext;
        }

    }
}