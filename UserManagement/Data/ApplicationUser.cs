using Microsoft.AspNetCore.Identity;
namespace UserManagement.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int UsernameChangeLimit { get; set; } = 10;

    public byte[]? ProfilePicture { get; set; }
}
