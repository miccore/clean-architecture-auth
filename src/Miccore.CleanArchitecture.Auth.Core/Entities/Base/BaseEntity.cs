using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Miccore.CleanArchitecture.Auth.Core.Utils;

namespace Miccore.CleanArchitecture.Auth.Core.Entities
{
    /// <summary>
    /// Base entity
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }

        [DefaultValue(0)]
        public int CreatedAt
        {
            get;
            set;
        } = DateUtils.GetCurrentTimeStamp();

        [DefaultValue(0)]
        public int? UpdatedAt
        {
            get;
            set;
        }

        [DefaultValue(0)]
        public int? DeletedAt
        {
            get;
            set;
        }
    }
}