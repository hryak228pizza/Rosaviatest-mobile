using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rosaviatest_mobile.Views
{
    
    public partial class Test : ContentPage
    {
        private bool isButton1Pressed = true;
        private bool _CHECK1 = true;
        private bool _CHECK2 = false;
        private bool _CHECK3 = false;
        private bool _CHECK4 = false;
        private bool _isTraining = false;


        public Test()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


            Start_Button_Statement();

        }

        public void Start_Button_Statement()
        {
            isButton1Pressed = true;
            Button1.BackgroundColor = Color.FromHex("#d1cfcf");
            Button2.BackgroundColor = Color.FromHex("#ebebeb"); // Сбросить состояние кнопки 2
            UpdateLabel(); 
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            isButton1Pressed = true;
            _isTraining = false;
            checkBox3.IsEnabled = true;
            checkBox3.Opacity = 1;
            Label3.Opacity = 1;
            Button1.BackgroundColor = Color.FromHex("#d1cfcf");
            Button2.BackgroundColor = Color.FromHex("#ebebeb"); // Сбросить состояние кнопки 2
            UpdateLabel();
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            isButton1Pressed = false;
            _isTraining = true;
            checkBox3.IsEnabled = false;
            checkBox3.Opacity = 0.5;
            Label3.Opacity = 0.5;
            Button2.BackgroundColor = Color.FromHex("#d1cfcf");
            Button1.BackgroundColor = Color.FromHex("#ebebeb"); // Сбросить состояние кнопки 1
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            if (isButton1Pressed)
            {
                labelTest.Text = "Простой режим тестирования со случайным выбором вопросов.\n\n";
            }
            else
            {
                labelTest.Text = "Режим тренировки позволяет пройти весь набор вопросов в отмеченных темах. В этом режиме правильность выбранного ответа отображается сразу.";
            }
        }

        private async void testStart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TestSlide(_CHECK1, _CHECK2, _CHECK3, _CHECK4, _isTraining));
        }

        void OnCheckBoxCheckedChanged1(object sender, CheckedChangedEventArgs e)
        {
            if(_CHECK1) _CHECK1 = false;
            else _CHECK1 = true;
        }
        void OnCheckBoxCheckedChanged2(object sender, CheckedChangedEventArgs e)
        {
            if (_CHECK2) _CHECK2 = false;
            else _CHECK2 = true;
        }
        void OnCheckBoxCheckedChanged3(object sender, CheckedChangedEventArgs e)
        {
            if (_CHECK3) _CHECK3 = false;
            else _CHECK3 = true;
        }
        void OnCheckBoxCheckedChanged4(object sender, CheckedChangedEventArgs e)
        {
            if (_CHECK4) _CHECK4 = false;
            else _CHECK4 = true;
        }
    }
}