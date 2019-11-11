using System;
using API_Posts_Details.Models;

namespace API_Posts_Details.ViewModels
{
    public class PostViewModel: BaseViewModel
    {
        public int Id { get; set; }

        public PostViewModel() { }

        public PostViewModel(Post post)
        {
            Id = post.Id;
            _userId = post.UserId;
            _title = post.Title;
            _body = post.Body;
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                SetValue(ref _userId, value);
                OnPropertyChanged(nameof(UserId));
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                SetValue(ref _title, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _body;
        public string Body
        {
            get { return _body; }
            set
            {
                SetValue(ref _body, value);
                OnPropertyChanged(nameof(Body));
            }
        }
    }
}
