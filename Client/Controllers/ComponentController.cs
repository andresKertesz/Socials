using System.Reactive;
namespace Socials.Client.Client.Controllers
{
    public class ComponentController
    {

        public bool ShowNavBar { get; set; }
        public string? CurrentPageTitle { get; private set; }

        public event Action OnTitleChanged;


        public void SetCurrentPageTitle(string title)
        {
            CurrentPageTitle = title;
            NotifyTitleChanged();

        }

        private void NotifyTitleChanged() => OnTitleChanged?.Invoke();
    }
}
