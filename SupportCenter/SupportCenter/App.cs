using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportCenter.Domain;

using Xamarin.Forms;

namespace SupportCenter
{
    public class App : Application
    {

        public App()
        {
            // The root page of your application
            var np = new NavigationPage(new Dashboard());

            MainPage = np;
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
