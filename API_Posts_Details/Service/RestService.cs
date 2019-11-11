using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API_Posts_Details.Models;
using Newtonsoft.Json;

namespace API_Posts_Details.Service
{
    public class RestService : IRestService
    {
        HttpClient _client;

        public List<Post> Posts { get; private set; }

        public List<Comment> Comments { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Post>> RefreshPostDataAsync()
        {
            Posts = new List<Post>();

            var uri = new Uri(string.Format(Constants.PostsUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                Debug.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Posts = JsonConvert.DeserializeObject<List<Post>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Posts;
        }

        public async Task<List<Comment>> RefreshCommentDataAsync(Post post)
        {
            Comments = new List<Comment>();

            var urlGetRequest = Constants.CommentsUrl + $"{post.UserId}";
            try
            {
                var response = await _client.GetAsync(urlGetRequest);
                Debug.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Comments = JsonConvert.DeserializeObject<List<Comment>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Comments;
        }

        public async Task<bool> SavePostAsync(Post post, bool isNewPost)
        {
            var uri = new Uri(string.Format(Constants.PostsUrl, string.Empty));

            try
            {
                var content = JsonConvert.SerializeObject(post);
                var urlPutRequest = uri + $"{ post.Id}";

                HttpResponseMessage response = null;

                if (isNewPost)
                {
                    response = await _client.PostAsync(uri, new StringContent(content));
                }
                else
                {
                    response = await _client.PutAsync(urlPutRequest, new StringContent(content));
                }
                Debug.WriteLine(response);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var urlPutRequest = Constants.PostsUrl + $"{id}";
            try
            {
                var response = await _client.DeleteAsync(urlPutRequest);
                Debug.WriteLine(response);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }
    }
}
