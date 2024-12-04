using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class Permission
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PermissionId { get; set; }
        [StringLength(150)]
        public string PermissionName { get; set; } = null!;
        public ICollection<RolePermission> RolePermissions { get; set; } = null!;
    }

