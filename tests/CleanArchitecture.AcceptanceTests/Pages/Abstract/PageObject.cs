
namespace CleanArchitecture.AcceptanceTests.Pages.Abstract
{
    public abstract class PageObject
    {
        protected PageObject(IPage page)
        {
            Page = page;
        }

        public readonly IPage Page;
        public TPage As<TPage>() where TPage : PageObject
        {
            return (TPage)this;
        }

        public async Task RefreshAsync()
        {
            await Page.ReloadAsync();
        }       

        public async Task<bool> WaitForConditionAsync(Func<Task<bool>> condition, bool waitForValue = true, int checkDelayMs = 100, int numberOfChecks = 300)
        {
            var value = !waitForValue;
            for (int i = 0; i < numberOfChecks; i++)
            {
                value = await condition();
                if (value == waitForValue)
                {
                    break;
                }

                await Task.Delay(checkDelayMs);
            }
            return value;
        }
    }
}
