using System.ComponentModel.DataAnnotations;

namespace apirest.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(32)]
        public string AuthKey { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        public string PasswordResetToken { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public short Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public string VerificationToken { get; set; }

        public string AccessToken { get; set; }
    }
}
