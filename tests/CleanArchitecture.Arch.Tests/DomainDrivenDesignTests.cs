using CSharpFunctionalExtensions;
using CleanArchitecture.Application.Abstractions.DomainEventHandlers;
using CleanArchitecture.Core.Abstractions.DomainEvents;
using CleanArchitecture.Core.Abstractions.Entities;

namespace CleanArchitecture.Arch.Tests
{
    public class DomainDrivenDesignTests : BaseTests
    {
        [Fact]
        public void DomainDrivenDesign_ValueObjects_ShouldBeImmutable()
        {
            Types.InAssembly(CoreAssembly)
                .That().Inherit(typeof(ValueObject))
                .Should().BeImmutable()
                .AssertIsSuccessful();
        }

        [Fact]
        public void DomainDrivenDesign_Aggregates_ShouldBeHavePrivateSettings()
        {
            Types.InAssembly(CoreAssembly)
                .That().Inherit(typeof(AggregateRoot))
                .Should().BeImmutable()
                .AssertIsSuccessful();
        }

        [Fact]
        public void DomainDrivenDesign_Entities_ShouldBeHavePrivateSettings()
        {
            Types.InAssembly(CoreAssembly).That().Inherit(typeof(EntityBase))
                .Should().BeImmutable()
                .AssertIsSuccessful();
        }

        [Fact]
        public void DomainDrivenDesign_Aggregates_ShouldOnlyResideInCore()
        {
            AllTypes.That().Inherit(typeof(AggregateRoot))
                .Should().ResideInNamespaceStartingWith("CleanArchitecture.Core")
                .AssertIsSuccessful();
        }

        [Fact]
        public void DomainDrivenDesign_DomainEvents_ShouldOnlyResideInCore()
        {
            AllTypes.That().Inherit(typeof(DomainEvent))
                .Should().ResideInNamespaceStartingWith("CleanArchitecture.Core")
                .AssertIsSuccessful();
        }

        [Fact]
        public void DomainDrivenDesign_DomainEvents_ShouldEndWithDomainEvent()
        {
            AllTypes.That().Inherit(typeof(DomainEvent))
                .Should().HaveNameEndingWith("DomainEvent")
                .AssertIsSuccessful();
        }

        [Fact]
        public void DomainDrivenDesign_DomainEventHandlers_ShouldOnlyResideInApplication()
        {
            AllTypes.That().Inherit(typeof(DomainEventHandler<>))
                .Should().ResideInNamespaceStartingWith("CleanArchitecture.Application")
                .AssertIsSuccessful();
        }

        [Fact]
        public void DomainDrivenDesign_DomainEventHandlers_ShouldEndWithDomainEventHandler()
        {
            AllTypes.That().Inherit(typeof(DomainEventHandler<>))
                .Should().HaveNameEndingWith("DomainEventHandler")
                .AssertIsSuccessful();
        }
    }
}