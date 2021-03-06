using AutoMapper;

namespace Miccore.CleanArchitecture.Auth.Application.Mappers
{
    /// <summary>
    /// general class for profile adding
    /// </summary>
    public class UserMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                #region profiles

                // user mapping profile
                cfg.AddProfile<UserMappingProfile>();

                #endregion
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
}