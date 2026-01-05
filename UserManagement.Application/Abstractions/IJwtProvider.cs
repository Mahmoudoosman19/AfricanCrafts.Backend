namespace UserManagement.Application.Abstractions;

public interface IJwtProvider
{
    Task<string> Generate(User user);
}
