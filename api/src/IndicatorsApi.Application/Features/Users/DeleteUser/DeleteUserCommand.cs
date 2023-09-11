namespace IndicatorsApi.Application.Features.Users.DeleteUser;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">User id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteUserCommand(string Id)
    : IDeleteCommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
