using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_Posts_Details.Models;

namespace API_Posts_Details.Service
{
    public interface IRestService
    {
        Task<List<Post>> RefreshPostDataAsync();

        Task<List<Comment>> RefreshCommentDataAsync(Post post);

        Task<bool> SavePostAsync(Post post, bool isNewPost);

        Task<bool> DeletePostAsync(int id);
    }
}
