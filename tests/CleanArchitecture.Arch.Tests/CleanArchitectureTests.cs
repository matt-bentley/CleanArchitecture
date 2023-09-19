using CleanArchitecture.Application.Abstractions.Repositories;

namespace CleanArchitecture.Arch.Tests
{
    [Collection("Sequential")]
    public class CleanArchitectureTests : BaseTests
    {
        [Fact]
        public void CleanArchitecture_Layers_ApplicationDoesNotReferenceInfrastructure()
        {
            AllTypes.That().ResideInNamespace("CleanArchitecture.Application")
            .ShouldNot().HaveDependencyOn("CleanArchitecture.Infrastructure")
            .AssertIsSuccessful();
        }

        [Fact]
        public void CleanArchitecture_Layers_CoreDoesNotReferenceOuter()
        {
            var coreTypes = AllTypes.That().ResideInNamespace("CleanArchitecture.Core");

            coreTypes.ShouldNot().HaveDependencyOn("CleanArchitecture.Infrastructure")
                .AssertIsSuccessful();

            coreTypes.ShouldNot().HaveDependencyOn("CleanArchitecture.Application")
                .AssertIsSuccessful();
        }

        [Fact]
        public void CleanArchitecture_Repositories_OnlyInInfrastructure()
        {
            AllTypes.That().HaveNameEndingWith("Repository")
            .Should().ResideInNamespaceStartingWith("CleanArchitecture.Infrastructure")
            .AssertIsSuccessful();

            AllTypes.That().HaveNameEndingWith("Repository")
                .And().AreClasses()
                .Should().ImplementInterface(typeof(IRepository<>))
                .AssertIsSuccessful();
        }

        [Fact]
        public void CleanArchitecture_Repositories_ShouldEndWithRepository()
        {
            AllTypes.That().Inherit(typeof(IRepository<>))
                .Should().HaveNameEndingWith("Repository")
                .AssertIsSuccessful();
        }
    }
}