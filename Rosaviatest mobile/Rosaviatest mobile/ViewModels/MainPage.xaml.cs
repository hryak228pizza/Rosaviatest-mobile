using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using System.IO;
using System.Diagnostics;
using System.Windows.Input;

namespace Rosaviatest_mobile.Views
{
    public partial class MainPage : ContentPage
    {
        private bool _bankCreated = false;
        private Bank _bank;

        public MainPage()
        {
            InitializeComponent();

            paperPlane.Source = ImageSource.FromResource("Rosaviatest_mobile.Resources.paper-plane.png");
            BGpic.Source = ImageSource.FromResource("Rosaviatest_mobile.Resources.Launch.png");

            //correctMark.Source = ImageSource.FromResource("Rosaviatest_mobile.Resources.check.png");

            NavigationPage.SetHasNavigationBar(this, false);

            
        }

        private async void TestClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Test());
        }

        private async void BankClick(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new Bank());
            if (!_bankCreated)
            {
                _bank = new Bank();
                _bankCreated = true;
                await Navigation.PushModalAsync(_bank);
            }
            else await Navigation.PushModalAsync(_bank);
        }
    }
}
