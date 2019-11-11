using System.Threading.Tasks;
using API_Posts_Details.ViewModels;
using Xamarin.Forms;

namespace API_Posts_Details.Views
{
    public class PageService : IPageService
    {
        public async Task DisplayAlert(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok);
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        public async Task PushModalAsync(Page page)
        {
            await MainPage.Navigation.PushModalAsync(page);
        }

        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}