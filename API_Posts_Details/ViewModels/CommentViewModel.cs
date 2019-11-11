using System;
using API_Posts_Details.Models;

namespace API_Posts_Details.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        public int PostId { get; set; }

        public int Id { get; set; }

        public CommentViewModel() { }

        public CommentViewModel(Comment comment)
        {
            PostId = comment.PostId;
            Id = comment.Id;
            _name = comment.Name;
            _email = comment.Email;
            _body = comment.Body;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                SetValue(ref _email, value);
                OnPropertyChanged(nameof(Email));
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
