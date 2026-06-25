namespace UserManagement.Models;

public class UserRolesViewModel
{
    public string UserId { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Roles { get; set; } = string.Empty;
}