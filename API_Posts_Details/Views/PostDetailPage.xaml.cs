using API_Posts_Details.ViewModels;
using Xamarin.Forms;

namespace API_Posts_Details.Views
{
    public partial class PostDetailPage : ContentPage
    { 
        public PostDetailPage(PostViewModel viewModel)
        {
            InitializeComponent();

            var pageService = new PageService();
            BindingContext = new PostDetailViewModel(viewModel ?? new PostViewModel(), pageService);
        }
    }
}
