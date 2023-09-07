namespace IndicatorsApi.Application.Features.ActorTypes.DeleteActorType;

/// <summary>
/// Delete command.
/// </summary>
/// <param name="Id">ActorType id.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public sealed record class DeleteActorTypeCommand(int Id)
    : ICommand;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
