using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using API_Posts_Details.Models;
using Xamarin.Forms;

namespace API_Posts_Details.ViewModels
{
    public class PostCommentsViewModel: BaseViewModel
    {
        private IPageService _pageService;
        private Post _selectedPost;

        private bool _isDataLoaded;

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

        public ObservableCollection<CommentViewModel> Comments { get; private set; }
            = new ObservableCollection<CommentViewModel>();

        public ICommand LoadCommentsCommand { get; private set; }
        public ICommand RefreshCommentsCommand { get; private set; }
        public ICommand ReturnButtonCommand { get; private set; }

        public PostCommentsViewModel(IPageService pageService, Post post)
        {
            _pageService = pageService;
            _selectedPost = post;

            LoadCommentsCommand = new Command(async () => await LoadComments(_selectedPost));
            RefreshCommentsCommand = new Command(async () => await RefreshComments());
            ReturnButtonCommand = new Command(async () => await GoToPosts());
        }

        private async Task RefreshComments()
        {
            IsRefreshing = true;
            await LoadComments(_selectedPost);
            IsRefreshing = false;
        }

        //Fetch Comments From Post API
        private async Task LoadComments(Post post)
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;
            var comments = await App.PostsManager.GetCommentsTasksAsync(post);

            foreach (var comment in comments)
                Comments.Add(new CommentViewModel(comment));
        }

        //Navigation to PostsDetailPage
        async Task GoToPosts()
        {
            await _pageService.PopAsync();
        }
    }
}
