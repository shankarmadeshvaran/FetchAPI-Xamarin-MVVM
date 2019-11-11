using System;
using System.Threading.Tasks;
using System.Windows.Input;
using API_Posts_Details.Models;
using API_Posts_Details.Views;
using Xamarin.Forms;

namespace API_Posts_Details.ViewModels
{
    public class PostDetailViewModel
    {
        private readonly IPageService _pageService;

        public Post Post { get; private set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand ShowCommentsCommand { get; private set; }
        public string PageTitle { get; }
        public bool IsCommentsButtonVisible { get; }

        public PostDetailViewModel(PostViewModel viewModel, IPageService pageService)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());
            ShowCommentsCommand = new Command(async () => await ShowComments());
            IsCommentsButtonVisible = (viewModel.Title == null && viewModel.Body == null) ? false : true;
            PageTitle = (viewModel.Title == null) ? "New Post" : "Edit Post";

            Post = new Post
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                Title = viewModel.Title,
                Body = viewModel.Body,
            };
        }
        // Editing and Adding New Post
        async Task Save()
        {
            if (String.IsNullOrWhiteSpace(Post.Title) && String.IsNullOrWhiteSpace(Post.Body))
            {
                await _pageService.DisplayAlert("Error", "Please enter the Title and Body of the Post.", "OK");
                return;
            }

            if (Post.Id == 0)
            {
                //Call this Event When New Post Added
                MessagingCenter.Send(this, Events.PostAdded, Post);
            }
            else
            {
                //Call this Event when Post is Edited
                MessagingCenter.Send(this, Events.PostUpdated, Post);
            }
            await _pageService.PopAsync();
        }

        //Navigate to PostsCommentsPage
        async Task ShowComments()
        {
            await _pageService.PushAsync(new PostCommentsPage(Post));
        }
    }
}
