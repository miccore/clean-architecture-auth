using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miccore.CleanArchitecture.Auth.Core.Entities
{
    /// <summary>
    /// Auth entity
    /// </summary>
    [Table("Roles")]
    public class Role : BaseEntity
    {
        [Required]
        public string? Name
        {
            get;
            set;
        }
    }
}