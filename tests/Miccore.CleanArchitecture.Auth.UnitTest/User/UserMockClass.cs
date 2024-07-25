using System.Collections.Generic;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Auth.UnitTest.User
{
    public class UserMockClass
    {   

        public List<Core.Entities.User> _data;

        public UserMockClass(){
            // iquerable data
            _data = new List<Core.Entities.User>() {};
        }

        /// <summary>
        /// mock database context with empty data
        /// </summary>
        /// <returns></returns>
         public Mock<AuthApplicationDbContext> GetDbContext(){
            
            // context database setup
            var options = new DbContextOptionsBuilder<AuthApplicationDbContext>().Options;
            var mockDbContext = new Mock<AuthApplicationDbContext>(options);
            mockDbContext.SetupSequence(x => x.Set<Core.Entities.User>())
                        .ReturnsDbSet(_data);
            
            // return mock
            return mockDbContext;
        }

        /// <summary>
        /// générate data from users
        /// </summary>
        /// <param name="size"></param> <summary>
        public void GenerateData(int size){

            for (int i = 0; i < 9; i++)
            {
                _data.Add(
                        new Core.Entities.User(){
                            Id = i,
                            FirstName = "User " + i,
                            CreatedAt = DateUtils.GetCurrentTimeStamp(),
                            DeletedAt = 0,
                            UpdatedAt = 0
                        }
                );
            }
        }

    }
}