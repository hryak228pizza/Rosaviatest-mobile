using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rosaviatest_mobile.Models;

namespace Rosaviatest_mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionForm : ContentPage

    {
        public QuestionForm(int questionNum, string title, string statusColor)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            check_P.Source = ImageSource.FromResource("Rosaviatest_mobile.Resources.check_P.png");
            check_G.Source = ImageSource.FromResource("Rosaviatest_mobile.Resources.check_G.png");

            int n = questionNum;

            questionLabel.Text = $"Обзор вопроса № {n}";
            theme.Text = $"Тема вопроса: {title}";

            //status frame
            Frame typeFrame = new Frame()
            {
                Padding = new Thickness(4, 0),
                BackgroundColor = Color.FromHex(statusColor)
            };
            //status text
            Label questionTextLabelCeil = new Label
            {
                Text = QuestionData.Status[n],
                FontFamily = "FiraSansBold",
                FontSize = 12.8,
                TextColor = Color.White
            };
            typeFrame.Content = questionTextLabelCeil;
            typeContainer.Children.Add(typeFrame);

            //Текст вопроса
            //////////////////////////////////////////////////////////////////////
            Frame questionTextFrame = new Frame
            {
                Padding = 16
            };
            Label questionTextLabel = new Label
            {
                Text = QuestionData.QuestionBank[title][n],
                FontFamily = "FiraSans",
                FontSize = 14,
                TextColor = Color.FromHex("#363642")
            };
            FlexLayout flexLayout = new FlexLayout
            {
                Direction = FlexDirection.Row,
                Wrap = FlexWrap.Wrap,
                JustifyContent = FlexJustify.Start,
                AlignItems = FlexAlignItems.Start,
                AlignContent = FlexAlignContent.SpaceAround
            };

            flexLayout.Children.Add(questionTextLabel);
            questionTextFrame.Content = flexLayout;
            questionTextContainer.Children.Add(questionTextFrame);

            //Варианты ответов
            //////////////////////////////////////////////////////////////////////


            // Создаем Grid для размещения элементов
            var grid = new Grid
            {
                RowSpacing = 0,
            };

            for (int i = 0; i < 3; i++)
            {
                string t = "";
                try { t = QuestionData.Keys[n.ToString()][i + 1]; }
                catch { };

                string BGanswerColor;
                string answerTextColor = "#363642";

                if (t == "list-group-item list-group-item-success") BGanswerColor = "#DFF0D8";
                else if (t == "list-group-item list-group-item-pseudo-success") { BGanswerColor = "#E6E6FA"; answerTextColor = "#6A5ACD"; }
                else BGanswerColor = "#FFFFFF";

                // Создаем строку для текста
                RowDefinition textRow = new RowDefinition { Height = GridLength.Auto };
                grid.RowDefinitions.Add(textRow);

                Frame frame = new Frame();

                // Создаем разделительную полосу (кроме последней строки)
                if (i < 2)
                {
                    RowDefinition separatorRow = new RowDefinition { Height = 1 };
                    grid.RowDefinitions.Add(separatorRow);

                    BoxView separator = new BoxView
                    {
                        HeightRequest = 1,
                        //BackgroundColor = Color.LightGray, // Цвет разделительной полосы
                        BackgroundColor = Color.FromHex("#f8f8f8")
                    };
                    grid.Children.Add(separator, 0, i * 2 + 1);

                    // Создаем Frame для текстового блока
                    frame = new Frame
                    {
                        HasShadow = false, // Добавляем тень
                        CornerRadius = 0,
                        BackgroundColor = Color.FromHex(BGanswerColor), // Устанавливаем цвет фона
                        Padding = new Thickness(0), // Убираем внутренний отступ у Frame
                        VerticalOptions = LayoutOptions.Start, // Выравнивание по верхнему краю
                        HorizontalOptions = LayoutOptions.FillAndExpand, // Заполняем ширину ячейки
                    };
                }
                else
                {
                    // Создаем Frame для текстового блока
                    frame = new Frame
                    {
                        HasShadow = true, // Добавляем тень
                        BackgroundColor = Color.FromHex(BGanswerColor), // Устанавливаем цвет фона
                        Padding = new Thickness(0), // Убираем внутренний отступ у Frame
                        VerticalOptions = LayoutOptions.Start, // Выравнивание по верхнему краю
                        HorizontalOptions = LayoutOptions.FillAndExpand, // Заполняем ширину ячейки
                    };
                }
                

                StackLayout rowLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                // Добавляем вертикальную полоску для выделения правильного ответа
                if (t == "list-group-item list-group-item-success")
                {
                    BoxView answerIndicator = new BoxView
                    {
                        WidthRequest = 3,
                        BackgroundColor = Color.FromHex("#00A28A"),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.Start,
                    };
                    Image img = new Image
                    {
                        Source = ImageSource.FromResource("Rosaviatest_mobile.Resources.check_G.png"),
                        WidthRequest = 16,
                        HeightRequest = 16,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(4, 0, 0, 0),
                    };
                    Label label = new Label
                    {
                        Text = QuestionData.Answers[n][i],
                        Margin = new Thickness(0, 0, 0, 0),
                        FontFamily = "FiraSans",
                        FontSize = 14,
                        TextColor = Color.FromHex(answerTextColor),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Padding = new Thickness(4, 12),
                    };
                    rowLayout.Children.Add(answerIndicator);
                    rowLayout.Children.Add(img);
                    rowLayout.Children.Add(label);

                    frame.Content = rowLayout;
                }
                else if (t == "list-group-item list-group-item-pseudo-success")
                {
                    BoxView answerIndicator = new BoxView
                    {
                        WidthRequest = 3,
                        BackgroundColor = Color.FromHex("#6A5ACD"),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.Start,
                    };
                    Image img = new Image
                    {
                        Source = ImageSource.FromResource("Rosaviatest_mobile.Resources.check_P.png"),
                        WidthRequest = 16,
                        HeightRequest = 16,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(4, 0, 0, 0),
                    };
                    Label label = new Label
                    {
                        Text = QuestionData.Answers[n][i],
                        Margin = new Thickness(0, 0, 0, 0),
                        FontFamily = "FiraSans",
                        FontSize = 14,
                        TextColor = Color.FromHex(answerTextColor),
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Padding = new Thickness(4, 12),
                    };
                    rowLayout.Children.Add(answerIndicator);
                    rowLayout.Children.Add(img);
                    rowLayout.Children.Add(label);

                    frame.Content = rowLayout;
                }
                else
                {
                    // Создаем текстовый элемент
                    Label label = new Label
                    {
                        Text = QuestionData.Answers[n][i],
                        FontFamily = "FiraSans",
                        FontSize = 14,
                        TextColor = Color.FromHex(answerTextColor),
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Padding = new Thickness(8, 12),
                        Margin = new Thickness(4, 0, 0, 0),
                    };
                    rowLayout.Children.Add(label);

                    frame.Content = rowLayout;
                }

                // Добавляем каждый фрейм в Grid
                grid.Children.Add(frame, 0, i * 2);
            }

            answersContainer.Children.Add(grid);


            //Описание
            //////////////////////////////////////////////////////////////////////
            Frame descriptionTextFrame = new Frame
            {
                Padding = 16
            };
            Label descriptionTextLabel = new Label
            {
                Text = QuestionData.Descriptions[n],
                FontFamily = "FiraSans",
                FontSize = 14,
                TextColor = Color.FromHex("#363642")
            };
            FlexLayout descriptionLayout = new FlexLayout
            {
                Direction = FlexDirection.Row,
                Wrap = FlexWrap.Wrap,
                JustifyContent = FlexJustify.Start,
                AlignItems = FlexAlignItems.Start,
                AlignContent = FlexAlignContent.SpaceAround
            };

            if (QuestionData.Descriptions[questionNum] != "") 
            {
                var descriptionContainer = new StackLayout();

                var descrTitle = new Label
                {
                    Text = "Обоснование",
                    FontSize = 24,
                    FontFamily = "FiraSans",
                    TextColor = Color.FromHex("#363642")
                };

                descriptionContainer.Children.Add(descrTitle);

                descriptionLayout.Children.Add(descriptionTextLabel);
                descriptionTextFrame.Content = descriptionLayout;
                descriptionContainer.Children.Add(descriptionTextFrame);

                mainStack.Children.Add(descriptionContainer);
                mainStack.Children.Add(new Label());
            }
        }
    }
}