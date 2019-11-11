using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using API_Posts_Details.Models;
using API_Posts_Details.Views;
using Xamarin.Forms;

namespace API_Posts_Details.ViewModels
{
    public class PostsPageViewModel : BaseViewModel
    {
        private PostViewModel _selectedContact;
        private IPageService _pageService;
        private bool _isDataLoaded;
        private string alertMessage;
        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ObservableCollection<PostViewModel> Posts { get; private set; }
            = new ObservableCollection<PostViewModel>();

        public PostViewModel SelectedPost
        {
            get { return _selectedContact; }
            set { SetValue(ref _selectedContact, value); }
        }

        public ICommand LoadPostsCommand { get; private set; }
        public ICommand AddPostCommand { get; private set; }
        public ICommand SelectPostCommand { get; private set; }
        public ICommand DeletePostCommand { get; private set; }
        public ICommand RefreshPostsCommand { get; private set; }

        public PostsPageViewModel(IPageService pageService)
        {
            _pageService = pageService;

            LoadPostsCommand = new Command(async () => await LoadPosts());
            AddPostCommand = new Command(async () => await AddPost());
            RefreshPostsCommand = new Command(async () => await RefreshPosts());
            SelectPostCommand = new Command<PostViewModel>(async c => await SelectPost(c));
            DeletePostCommand = new Command<PostViewModel>(async c => await DeletePost(c));

            MessagingCenter.Subscribe<PostDetailViewModel, Post>
                (this, Events.PostAdded, OnPostAdded);

            MessagingCenter.Subscribe<PostDetailViewModel, Post>
            (this, Events.PostUpdated, OnPostUpdated);
        }

        private async Task RefreshPosts()
        {
            IsRefreshing = true;
            await LoadPosts();
            IsRefreshing = false;
        }

        //Function calls this method When New Post Added
        private async void OnPostAdded(PostDetailViewModel source, Post post)
        {
            var isSuccessResponse = await App.PostsManager.SaveTaskAsync(post, isNewPost: true);
            if (isSuccessResponse)
            {
                alertMessage = "New Post Added Successfully";
            }

            var okAction = await _pageService.DisplayAlert("Success", alertMessage, "OK", "Cancel");
            if (okAction == true)
            {
                await _pageService.PopAsync();
            }
        }

        //Function calls this method When Post Edited
        private async void OnPostUpdated(PostDetailViewModel source, Post _post)
        {
            var post = new Post { UserId = _post.UserId, Id = _post.Id, Title = _post.Title, Body = _post.Body };
            var isSuccessResponse = await App.PostsManager.SaveTaskAsync(post, isNewPost: false);
            if (isSuccessResponse)
            {
                alertMessage = "Updated Successfully";
            }else
            {
                alertMessage = "Oops! Something went Wrong";
            }

            var okAction = await _pageService.DisplayAlert("Success", alertMessage, "OK", "Cancel");
            if (okAction == true)
            {
                await _pageService.PopAsync();
            }
        }

        //Fetch and Load List of Posts
        private async Task LoadPosts()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;
            var posts = await App.PostsManager.GetPostTasksAsync();

            foreach (var post in posts)
                Posts.Add(new PostViewModel(post));
        }

        private async Task AddPost()
        {
            await _pageService.PushAsync(new PostDetailPage(new PostViewModel()));
        }

        //Function calls this method when Post is selected
        private async Task SelectPost(PostViewModel post)
        {
            if (post == null)
                return;

            SelectedPost = null;
            await _pageService.PushAsync(new PostDetailPage(post));
        }

        //Function calls this method When Post is Deleted
        private async Task DeletePost(PostViewModel postViewModel)
        {
            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {postViewModel.Title}?", "Yes", "No"))
            {
                var isSuccessResponse = await App.PostsManager.DeleteTaskAsync(postViewModel);
                if (isSuccessResponse)
                {
                    alertMessage = "Deleted Successfully";
                } else
                {
                    alertMessage = "Oops! Something went Wrong";
                }

                var okAction = await _pageService.DisplayAlert("Success", alertMessage, "OK", "Cancel");
                if (okAction == true)
                {
                    Posts.Remove(postViewModel);
                }
            }
        }
    }
}
