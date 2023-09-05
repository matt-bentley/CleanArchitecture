using CleanArchitecture.AcceptanceTests.Pages.Abstract;

namespace CleanArchitecture.AcceptanceTests.Pages
{
    public class HomePage : PageObject
    {
        public readonly NavBar NavBar;

        public HomePage(IPage page) : base(page)
        {
            NavBar = new NavBar(page);
        }
    }
}
