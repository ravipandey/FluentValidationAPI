public class UserDto {
    [Required, MaxLength(100)]
    public string Name { get; set; }
    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; }
}
