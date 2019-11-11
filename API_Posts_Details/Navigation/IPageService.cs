using System.Threading.Tasks;
using Xamarin.Forms;

namespace API_Posts_Details.ViewModels
{
    public interface IPageService
    {
        Task PushAsync(Page page);

        Task PushModalAsync(Page page);

        Task<Page> PopAsync();

        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);

        Task DisplayAlert(string title, string message, string ok);
    }
}
