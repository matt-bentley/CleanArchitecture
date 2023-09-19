using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Arch.Tests
{
    [Collection("Sequential")]
    public class ApiLayerTests : BaseTests
    {
        [Fact]
        public void Api_Controllers_ShouldOnlyResideInApi()
        {
            AllTypes.That().Inherit(typeof(ControllerBase))
                .Should().ResideInNamespaceStartingWith("CleanArchitecture.Api")
                .AssertIsSuccessful();
        }

        [Fact]
        public void Api_Controllers_ShouldInheritFromControllerBase()
        {
            Types.InAssembly(ApiAssembly)
                .That().HaveNameEndingWith("Controller")
                .Should().Inherit(typeof(ControllerBase))
                .AssertIsSuccessful();
        }

        [Fact]
        public void Api_Controllers_ShouldEndWithController()
        {
            AllTypes.That().Inherit(typeof(ControllerBase))
                .Should().HaveNameEndingWith("Controller")
                .AssertIsSuccessful();
        }
    }
}