using System.ComponentModel.DataAnnotations;

namespace Freedom.Auth.Web.Models;

public class AddUserView
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Compare(nameof(Password))]
    public string ConfirmedPassword { get; set; } = null!;
}
