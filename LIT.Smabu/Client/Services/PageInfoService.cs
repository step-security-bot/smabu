namespace LIT.Smabu.Client.Services
{
    public class PageInfoService
    {
        public event Action OnShowBusyOverlay;
        public event Action OnHideBusyOverlay;
        public event Action<string, string> OnShowError;
        
        public bool HandleHttpResponse(HttpResponseMessage? response)
        {
            if (response == null)
            {
                return false;
            }
            else if (response!.IsSuccessStatusCode == false)
            {
                HideBusyOverlay();
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var message = content.ToString().Split(" at ").FirstOrDefault();
                message = message?.Split(":").LastOrDefault();
                message = message?.ReplaceLineEndings().Trim();
                message = string.IsNullOrEmpty(message) ? content : message;
                var title = "Vorgang nicht möglich";
                ShowError(message, title);
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ShowBusyOverlay()
        {
            OnShowBusyOverlay?.Invoke();
        }

        public void HideBusyOverlay()
        {
            OnHideBusyOverlay?.Invoke();
        }

        public void ShowError(string message, string title)
        {
            OnShowError?.Invoke(message, title);
        }
    }
}
