using AliveBlog.Models;

namespace AliveBlog.ViewModels
{
    public class HomeViewModel
    {
        public List<Post>? RecentPosts { get; set; }

        public List<Post>? BannerPosts { get; set; }
    }
}