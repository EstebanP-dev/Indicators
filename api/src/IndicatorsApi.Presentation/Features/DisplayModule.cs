using IndicatorsApi.Application.Features.Displays.CreateDisplay;
using IndicatorsApi.Application.Features.Displays.DeleteDisplay;
using IndicatorsApi.Application.Features.Displays.GetDisplayById;
using IndicatorsApi.Application.Features.Displays.GetDisplaysPagination;
using IndicatorsApi.Application.Features.Displays.UpdateSection;
using IndicatorsApi.Contracts.Displays;
using IndicatorsApi.Contracts.Features.Displays.GetDisplayById;
using IndicatorsApi.Contracts.Features.Displays.GetDisplaysPagination;
using IndicatorsApi.Domain.Errors;
using IndicatorsApi.Domain.Features.Displays;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Presentation.Features;

/// <summary>
/// Display endpoints.
/// </summary>
public sealed class DisplayModule
    : BaseModule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayModule"/> class.
    /// </summary>
    public DisplayModule()
        : base("displays")
    {
    }

    /// <inheritdoc/>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] CreateDisplayRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            CreateDisplayCommand command = request.Adapt<CreateDisplayCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: command, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapGet("/", async ([AsParameters] PaginationQueryParameters parameters, ISender sender, CancellationToken cancellationToken) =>
        {
            int[] ids = GetIntsFromExcludeParameter(exclude: parameters.Exclude);

            GetDisplaysPaginationQuery query = new(Page: parameters.Page, Rows: parameters.Rows, Excludes: ids);

            ErrorOr<Pagination<Display>> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return Result<Pagination<Display>, Pagination<DisplayPaginationResponse>>(value: result);
        });

        app.MapGet("/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            GetDisplayByIdQuery query = new(Id: id);

            ErrorOr<Display> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result<Display, DisplayByIdResponse>(value: result);
        });

        app.MapPut("/{id}", async (int id, [FromBody] UpdateDisplayRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            if (id != request.Id)
            {
                return Problem(error: DomainErrors.NoCoincidence(left: id, right: request.Id));
            }

            UpdateDisplayCommand query = request.Adapt<UpdateDisplayCommand>();

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });

        app.MapDelete("/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            DeleteDisplayCommand query = new(id);

            ErrorOr<Success> result = await sender
                .Send(request: query, cancellationToken: cancellationToken)
                .ConfigureAwait(true);

            return Result(value: result);
        });
    }
}
