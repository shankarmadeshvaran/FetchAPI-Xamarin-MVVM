using System;
using API_Posts_Details.Service;
using API_Posts_Details.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace API_Posts_Details
{
    public partial class App : Application
    {
        public static PostManager PostsManager { get; private set; }
        public App()
        {
            InitializeComponent();
            PostsManager = new PostManager(new RestService());
            MainPage = new NavigationPage(new PostsPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
