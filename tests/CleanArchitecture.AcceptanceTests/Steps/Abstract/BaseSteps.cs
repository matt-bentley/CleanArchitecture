
namespace CleanArchitecture.AcceptanceTests.Steps.Abstract
{
    [Binding]
    public abstract class BaseSteps
    {
        protected readonly TestHarness TestHarness;

        protected BaseSteps(TestHarness testHarness)
        {
            TestHarness = testHarness;
        }
    }
}
