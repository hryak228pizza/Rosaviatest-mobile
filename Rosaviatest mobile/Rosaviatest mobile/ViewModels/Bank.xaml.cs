using Rosaviatest_mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rosaviatest_mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bank : ContentPage
    {
        public Bank()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            Style buttonStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.WidthRequestProperty, Value = 40 },
                    new Setter { Property = Button.HeightRequestProperty, Value = 40 },
                    new Setter { Property = Button.CornerRadiusProperty, Value = 0 },
                    new Setter { Property = Button.MarginProperty, Value = new Thickness(1.6) }
                }
            };


            foreach (KeyValuePair<string, Dictionary<int, string>> item in QuestionData.QuestionBank)
            {
                //StackLayout stackLayout = new StackLayout(); // Создаем контейнер для заголовка и кнопок

                Label titleLabel = new Label
                {
                    Text = item.Key,
                    FontFamily = "FiraSans",
                    FontSize = 24,
                    TextColor = Color.FromHex("#363642")
                };

                //titleContainer.Children.Add(titleLabel);        // Добавляем заголовок в контейнер
                buttonContainer.Children.Add(titleLabel);

                Frame frame = new Frame
                {
                    Padding = 8
                };

                FlexLayout flexLayout = new FlexLayout
                {
                    Direction = FlexDirection.Row,
                    Wrap = FlexWrap.Wrap,
                    JustifyContent = FlexJustify.Start,
                    AlignItems = FlexAlignItems.Start,
                    AlignContent = FlexAlignContent.SpaceAround
                };

                foreach (KeyValuePair<int, string> val in item.Value)
                {
                    string Bgcolor = QuestionData.Status[val.Key];
                    if (Bgcolor == "Подтверждена") Bgcolor = "#00A26B";
                    else if (Bgcolor == "Не подтверждена") Bgcolor = "#EFB858";
                    else if (Bgcolor == "Имеются расхождения") Bgcolor = "#CD5C5C";
                    else if (Bgcolor == "Неоднозначна") Bgcolor = "#808080";
                    else if (Bgcolor == "В процессе подтверждения") Bgcolor = "#428BCA";

                    Button button = new Button
                    {
                        Text = val.Key.ToString(),
                        TextColor = Color.White,
                        BackgroundColor = Color.FromHex(Bgcolor),
                        WidthRequest = 56,
                        HeightRequest = 40,
                        CornerRadius = 0,
                        Margin = new Thickness(1.6),
                        //Style = (Style)Application.Current.Resources["ButtonStyle"]
                    };

                    button.Clicked += (sender, args) =>
                    {
                        // Создаем новую страницу с вопросом и передаем необходимые данные
                        Navigation.PushModalAsync(new QuestionForm(val.Key, item.Key, Bgcolor)); // val.Key содержит номер вопроса
                    };

                    flexLayout.Children.Add(button);
                }

                frame.Content = flexLayout; // Задаем содержимое фрейма
                //buttonContainer.Children.Add(titleContainer); // Добавляем заголовок
                buttonContainer.Children.Add(frame); // Добавляем контейнер с заголовком и кнопками в основной контейнер
            }
        }



    }

}