using API_Posts_Details.ViewModels;
using Xamarin.Forms;

namespace API_Posts_Details.Views
{
    public partial class PostsPage : ContentPage
    {
        public PostsPage()
        {
            var pageService = new PageService();
            ViewModel = new PostsPageViewModel(pageService);

            InitializeComponent();
        }

        public PostsPageViewModel ViewModel
        {
            get { return BindingContext as PostsPageViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadPostsCommand.Execute(null);
            base.OnAppearing();
        }

        void OnPostSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectPostCommand.Execute(e.SelectedItem);
        }
    }
}
