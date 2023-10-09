namespace LIT.Smabu.Client.Services
{
    public class PageInfoService
    {
        public event Action OnShowBusyOverlay;
        public event Action OnHideBusyOverlay;

        public void ShowBusyOverlay()
        {
            OnShowBusyOverlay?.Invoke();
        }

        public void HideBusyOverlay()
        {
            OnHideBusyOverlay?.Invoke();
        }
    }
}
