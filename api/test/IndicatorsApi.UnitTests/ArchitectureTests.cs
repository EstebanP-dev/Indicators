using NetArchTest.Rules;

namespace IndicatorsApi.UnitTests;

/// <summary>
/// Architecture unit tests.
/// </summary>
public class ArchitectureTests
{
    private const string BaseNamespace = "IndicatorsApi.";
    private const string DomainNamespace = "Domain";
    private const string ContractsNamespace = "Application";
    private const string ApplicationNamespace = "Application";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string PersistenceNamespace = "Persistence";
    private const string PresentationNamespace = "Presentation";
    private const string WebApiNamespace = "WebApi";

    /// <summary>
    /// Test: If the domain layer not have dependencies on other solution projects.
    /// </summary>
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Domain.AssemblyReference.Assembly;
        string[] otherProjects = new[]
        {
            ContractsNamespace,
            ApplicationNamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            WebApiNamespace,
        }
        .Select(project => BaseNamespace + project)
        .ToArray();

        // Act
        TestResult result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Test: If the contracts layer not have dependencies on other solution projects.
    /// </summary>
    [Fact]
    public void Contracts_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Contracts.AssemblyReference.Assembly;
        string[] otherProjects = new[]
        {
            DomainNamespace,
            ApplicationNamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            WebApiNamespace,
        }
        .Select(project => BaseNamespace + project)
        .ToArray();

        // Act
        TestResult result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Test: If the application layer not have dependencies on other solution projects.
    /// </summary>
    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Application.AssemblyReference.Assembly;
        string[] otherProjects = new[]
        {
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            WebApiNamespace,
        }
        .Select(project => BaseNamespace + project)
        .ToArray();

        // Act
        TestResult result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Test: If the infrastructure layer not have dependencies on other solution projects.
    /// </summary>
    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Infrastructure.AssemblyReference.Assembly;
        string[] otherProjects = new[]
        {
            PersistenceNamespace,
            PresentationNamespace,
            WebApiNamespace,
        }
        .Select(project => BaseNamespace + project)
        .ToArray();

        // Act
        TestResult result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Test: If the persistence layer not have dependencies on other solution projects.
    /// </summary>
    [Fact]
    public void Persistence_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Persistence.AssemblyReference.Assembly;
        string[] otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            WebApiNamespace,
        }
        .Select(project => BaseNamespace + project)
        .ToArray();

        // Act
        TestResult result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Test: If the presentation layer not have dependencies on other solution projects.
    /// </summary>
    [Fact]
    public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Presentation.AssemblyReference.Assembly;
        string[] otherProjects = new[]
        {
            PersistenceNamespace,
            InfrastructureNamespace,
            WebApiNamespace,
        }
        .Select(project => BaseNamespace + project)
        .ToArray();

        // Act
        TestResult result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Test: If the webapi layer not have dependencies on other solution projects.
    /// </summary>
    [Fact]
    public void WebApi_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = WebApi.AssemblyReference.Assembly;
        string[] otherProjects = new[]
        {
            PersistenceNamespace,
            InfrastructureNamespace,
        }
        .Select(project => BaseNamespace + project)
        .ToArray();

        // Act
        TestResult result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}