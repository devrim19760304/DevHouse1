using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevHouse1.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        // Foreign Key for Role
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        //one to many 
        public Role Role { get; set; }

        // Foreign Key for Team
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        //many to one 
        public Team Team { get; set; }
    }
}
