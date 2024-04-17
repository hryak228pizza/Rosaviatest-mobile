using Rosaviatest_mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;
using Rosaviatest_mobile.Models;

namespace Rosaviatest_mobile
{
    public partial class App : Application
    {
        //public List<QuestionData> Questions { get; private set; }
        //public Bank bankPage = new Bank();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new TestSlide(false, true, false, false, false)); 
            NavigationPage.SetHasNavigationBar(this, false);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected void OnStop()
        {
        }
    }
}
