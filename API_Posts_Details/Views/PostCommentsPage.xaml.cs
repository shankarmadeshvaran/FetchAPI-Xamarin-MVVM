using API_Posts_Details.Models;
using API_Posts_Details.ViewModels;
using Xamarin.Forms;

namespace API_Posts_Details.Views
{
    public partial class PostCommentsPage : ContentPage
    {
        public PostCommentsPage(Post post)
        {
            var pageService = new PageService();
            ViewModel = new PostCommentsViewModel(pageService, post);

            InitializeComponent();
        }

        public PostCommentsViewModel ViewModel
        {
            get { return BindingContext as PostCommentsViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadCommentsCommand.Execute(null);
            base.OnAppearing();
        }
    }
}
