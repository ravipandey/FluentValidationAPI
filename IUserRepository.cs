namespace FluentValidationAPI
{
    public interface IUserRepository
    {
        Task<bool> NameExistsAsync(User user, string name, CancellationToken cancellationToken);
    }
}
