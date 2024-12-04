using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RoleId { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; } = null!;
        public ICollection<RolePermission> RolePermissions { get; set; } = null!;
        public ICollection<User> Users { get; set; } = null!;
    }

