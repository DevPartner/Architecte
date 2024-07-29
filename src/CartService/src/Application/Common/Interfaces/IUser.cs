namespace CartService.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; init; }
    string? Role { get; init; }
}
