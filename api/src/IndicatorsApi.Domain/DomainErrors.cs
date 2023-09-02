using IndicatorsApi.Domain.Features.Roles;
using IndicatorsApi.Domain.Features.Sections;
using IndicatorsApi.Domain.Features.Users;
using IndicatorsApi.Domain.Primitives;

namespace IndicatorsApi.Domain;

/// <summary>
/// Domain errors.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Only for the exceptions.")]
public static class DomainErrors
{
    /// <summary>
    /// General domain errors.
    /// </summary>
    public static class General
    {
        /// <summary>
        /// Gets the undefined error.
        /// </summary>
        /// <param name="exception">Inner exception.</param>
        /// <returns>Result error.</returns>
        public static Error UndefinedError(BaseException exception) => new(
                       code: HttpStatusCode.InternalServerError.ToString(),
                       message: "An error occurred while processing the request. Try to contact the support team.",
                       exception: exception);

        /// <summary>
        /// Gets the cancelled operation error.
        /// </summary>
        /// <param name="exception">Inner exception.</param>
        /// <returns>Result error.</returns>
        public static Error CancelledOperation(OperationCanceledException exception) => new(
            code: HttpStatusCode.InternalServerError.ToString(),
            message: "The operation was cancelled.",
            exception: new BaseException(
                message: "The operation was cancelled.",
                innerException: exception));
    }

    /// <summary>
    /// Auth domain errors.
    /// </summary>
    public static class Auth
    {
        /// <summary>
        /// Gets the invalid credentials error.
        /// </summary>
        /// <returns>Result error.</returns>
        public static Error InvalidCredentials() => new(
            code: HttpStatusCode.Unauthorized.ToString(),
            message: "The username or password are not valid. Try again later.",
            exception: new InvalidUserCredentialsException());

        /// <summary>
        /// Gets the null password salt error.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Result error.</returns>
        public static Error NullPasswordSalt(string email) => new(
            code: HttpStatusCode.Unauthorized.ToString(),
            message: "It's necessary to restore the password.",
            exception: new NullUserPasswordSaltException(email));
    }

    /// <summary>
    /// User domain errors.
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Gets the not found error.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Result error.</returns>
        public static Error NotFound(string email) => new(
            code: HttpStatusCode.NotFound.ToString(),
            message: $"The user was not found.",
            exception: new UserByEmailCannotBeFoundException(email));
    }

    /// <summary>
    /// Role domain errors.
    /// </summary>
    public static class Role
    {
        /// <summary>
        /// Gets the not found error.
        /// </summary>
        /// <param name="ids">Role ids.</param>
        /// <returns>Result error.</returns>
        public static Error NotFound(int[] ids) => new(
            code: HttpStatusCode.NotFound.ToString(),
            message: $"Something was wrong. Try again later.",
            exception: new RoleOrRolesByIdCannotBeFoundException(
                    ids: ids));

        /// <summary>
        /// Gets the not found error.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Result error.</returns>
        public static Error NotFound(string userId) => new(
            code: HttpStatusCode.NotFound.ToString(),
            message: $"Something was wrong. Try again later.",
            exception: new RoleOrRolesByUserIdCannotBeFoundException(
                    userId: userId));
    }

    /// <summary>
    /// Sections domain errors.
    /// </summary>
    public static class Section
    {
        /// <summary>
        /// Gets the NotFound error.
        /// </summary>
        /// <param name="id">Section id.</param>
        /// <returns>Result error.</returns>
        public static Error NotFound(int id) => new(
            code: HttpStatusCode.NotFound.ToString(),
            message: "The section was not found.",
            exception: new SectionByIdCannotBeFoundException(id));
    }
}
