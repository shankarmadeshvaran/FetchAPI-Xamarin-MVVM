using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_Posts_Details.Models;
using API_Posts_Details.ViewModels;

namespace API_Posts_Details.Service
{
    public class PostManager
    {
        IRestService restService;

        public PostManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<Post>> GetPostTasksAsync()
        {
            return restService.RefreshPostDataAsync();
        }

        public Task<List<Comment>> GetCommentsTasksAsync(Post post)
        {
            return restService.RefreshCommentDataAsync(post);
        }

        public Task<bool> SaveTaskAsync(Post post, bool isNewPost)
        {
            return restService.SavePostAsync(post, isNewPost);
        }

        public Task<bool> DeleteTaskAsync(PostViewModel post)
        {
            return restService.DeletePostAsync(post.UserId);
        }
    }
}
