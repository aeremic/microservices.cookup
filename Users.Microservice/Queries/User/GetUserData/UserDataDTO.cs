namespace Users.Microservice.Queries.User.GetUserData;

public class UserDataDto
{
    public long Id { get; set; }
    public required Guid Guid { get; set; }

    public required string Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Image { get; set; }
}