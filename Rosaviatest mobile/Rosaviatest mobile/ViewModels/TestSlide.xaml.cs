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
    public partial class TestSlide : ContentPage
    {
        private int maxQ, counterQ, percentage;
        private bool _CHECK1, _CHECK2, _CHECK3, _CHECK4, _isTraining;
        private List<byte> inputAns = new List<byte>();
        private string correctAnsText = "";

        private Dictionary<int, string> translator = new Dictionary<int, string>()
        {
            { 0, "основ полета"},
            { 1, "аэронавигации (самолетовождения)"},
            { 2, "по получению и применению метеорологических сводок, карт и прогнозов, кодов и сокращений"},
            { 3, "о возможностях человека, включая принципы контроля факторов угрозы и ошибок"},
            { 4, "правил обеспечения безопасности при полетах в визуальных метеорологических условиях"},
            { 5, "правил эксплуатации и ограничений воздушных судов"},
            { 6, "основных принципов устройства силовых установок, газотурбинных и/или поршневых двигателей; характеристик топлива"},
            { 7, "опасных для полетов метеорологических явлений, особых условий погоды"},
            { 8, "правил полетов"},
            { 9, "авиационной метеорологии; климатологии и ее влияния на авиацию"},
            { 10, "компасов, гироскопических приборов, правил и порядка действий при неисправностях различных пилотажных приборов"},
            { 11, "правил ведения радиосвязи и фразеологии"},
            { 12, "использования авиационного электронного и приборного оборудования"},
            { 13, "аварийных ситуаций и выживаемости"},
            { 14, "назначений аварийно-спасательного снаряжения воздушных судов, правил его эксплуатации"},
            { 15, "воздушного законодательства"},
            { 16, "планера, органов управления, колесных шасси, тормозов, систем"},
            { 17, "правил технического обслуживания воздушных судов"},
            { 18, "использования аэронавигационной документации, авиационных кодов и сокращений"},
            { 19, "подготовки и представления планов полета"},
            { 20, "процедур, связанных с актами незаконного вмешательства в деятельность гражданской авиации"},
            { 21, "оборудования воздушных судов"}
        };
        private int questionNum = 0;
        private int titleIndex = 0;

        private List<int> history = new List<int>();
        private static readonly Random random = new Random();

        //_CHECK1 - случайный порядок вопросов
        //_CHECK2 - случайный порядок ответов
        //_CHECK3 - показывать кнопку подсказки
        //_CHECK4 - показывать номер вопроса по приказу
        //1..1520+1

        // Метод для получения случайного ключа из коллекции
        private static T GetRandomKey<T>(ICollection<T> collection)
        {
            int randomIndex = random.Next(collection.Count);
            //return collection.ElementAt(randomIndex);
            return collection.Skip(randomIndex).Take(1).First();
        }

        private void LoadQuestion(int counterQ, int maxQ, bool _CHECK1, bool _CHECK2, bool _CHECK3, bool _CHECK4, bool _isTraining)
        {
            var random = new Random();
            int randQNum = -1;
            string question = "";
            string blockName = "";            

            questionNumber.Text = "Вопрос №" + (counterQ+1).ToString();

            Ans1.IsChecked = false;
            Ans2.IsChecked = false;
            Ans3.IsChecked = false;
            NextQBtn.IsEnabled = false;

            Ans1.TextColor = Color.FromHex("#363642");
            Ans1.BackgroundColor = Color.Transparent;
            Ans2.TextColor = Color.FromHex("#363642");
            Ans2.BackgroundColor = Color.Transparent;
            Ans3.TextColor = Color.FromHex("#363642");
            Ans3.BackgroundColor = Color.Transparent;

            var percentage = Math.Truncate(((double)counterQ / (double)maxQ)*100);
            progressLabel.Text = $"{percentage}% ({counterQ} из {maxQ})";
            progressBar.Progress = percentage/100;

            if (_CHECK1)
            {
                // Получение случайного блока вопросов
                //blockName = GetRandomKey(Models.QuestionData.QuestionBank.Keys);

                // Получение случайного вопроса из выбранного блока
                //questionNum = GetRandomKey(Models.QuestionData.QuestionBank[blockName].Keys);

                bool flag = true;
                do
                {
                    questionNum = random.Next(1520 + 1);
                    if (!history.Contains(questionNum)) // Если еще не было такого вопроса
                    {
                        // Проверяем если он существует
                        foreach (var item in Models.QuestionData.QuestionBank)
                        {
                            if (item.Value.ContainsKey(questionNum))
                            {
                                blockName = item.Key;
                                flag = false;
                            }
                        }
                    }
                    
                } while (flag);

                history.Add(questionNum);                
                question = Models.QuestionData.QuestionBank[blockName][questionNum];
            }
            else
            {
                questionNum++;

                foreach (var item in Models.QuestionData.QuestionBank)
                {
                    if (item.Value.ContainsKey(questionNum))
                    {
                        question = Models.QuestionData.QuestionBank[item.Key][questionNum];
                        break;
                    }
                }
            }

            // Номер правильного ответа из Answers
            string number = questionNum.ToString();
            int correctAns = -1;
            if (Models.QuestionData.Keys[number].Values.Count() == 1)
            {
                correctAns = Models.QuestionData.Keys[number].First().Key;
            }
            else
            {
                foreach (var pair in Models.QuestionData.Keys[number])
                {
                    if (pair.Value == "list-group-item list-group-item-pseudo-success")
                    {
                        correctAns = pair.Key;
                        break;
                    }
                }
            }
            correctAnsText = Models.QuestionData.Answers[questionNum][correctAns - 1];
            if (_CHECK4) stateQuestionNumber.Text = "Номер вопроса по Приказу: № " + number;
            else stateQuestionNumber.IsVisible = false;


            questionText.Text = question;

            if (_CHECK2)
            {
                List<int> list = new List<int>() { 0, 1, 2 };
                int randomIndex = list[random.Next(list.Count)];
                Ans1.Content = Models.QuestionData.Answers[questionNum][randomIndex];
                list.Remove(randomIndex);

                randomIndex = list[random.Next(list.Count)];
                Ans2.Content = Models.QuestionData.Answers[questionNum][randomIndex];
                list.Remove(randomIndex);

                Ans3.Content = Models.QuestionData.Answers[questionNum][list[0]];                               
            }
            else
            {
                Ans1.Content = Models.QuestionData.Answers[questionNum][0];
                Ans2.Content = Models.QuestionData.Answers[questionNum][1];
                Ans3.Content = Models.QuestionData.Answers[questionNum][2];
            }

            Ans1.HorizontalOptions = default;
            Ans2.HorizontalOptions = default;
            Ans3.HorizontalOptions = default;
            Ans1.VerticalOptions = default;
            Ans2.VerticalOptions = default;
            Ans3.VerticalOptions = default;

            Ans1.HorizontalOptions = LayoutOptions.FillAndExpand;
            Ans1.VerticalOptions = LayoutOptions.Center;
            Ans2.HorizontalOptions = LayoutOptions.FillAndExpand;
            Ans2.VerticalOptions = LayoutOptions.Center;
            Ans3.HorizontalOptions = LayoutOptions.FillAndExpand;
            Ans3.VerticalOptions = LayoutOptions.Center;

            if (!_CHECK3) ShowAnsBtn.IsVisible = false;
        }

        private void NextQuestion(object sender, EventArgs e)
        {
            if (Ans1.IsChecked)
            {
                if (Ans1.Content == correctAnsText) inputAns.Add(1);
                else inputAns.Add(0);

                NextQBtn.IsEnabled = true;
            }
            if (Ans2.IsChecked)
            {
                if (Ans2.Content == correctAnsText) inputAns.Add(1);
                else inputAns.Add(0);

                NextQBtn.IsEnabled = true;
            }
            if (Ans3.IsChecked)
            {
                if (Ans3.Content == correctAnsText) inputAns.Add(1);
                else inputAns.Add(0);

                NextQBtn.IsEnabled = true;
            }

            counterQ++;

            if (counterQ < maxQ) LoadQuestion(counterQ, maxQ, _CHECK1, _CHECK2, _CHECK3, _CHECK4, _isTraining);
            else Navigation.PushModalAsync(new ResultSlide(inputAns));
        }
        private void ShowAnswer(object sender, EventArgs e)
        {
            if (Ans1.Content == correctAnsText)
            {
                Ans1.IsChecked = true;
                Ans1.TextColor = Color.FromHex("#00A28A");
                Ans1.BackgroundColor = Color.FromHex("#DFF0D8");
            }
            if (Ans2.Content == correctAnsText)
            {
                Ans2.IsChecked = true;
                Ans2.TextColor = Color.FromHex("#00A28A");
                Ans2.BackgroundColor = Color.FromHex("#DFF0D8");
            }
            if (Ans3.Content == correctAnsText)
            {      
                Ans3.IsChecked = true;
                Ans3.TextColor = Color.FromHex("#00A28A");
                Ans3.BackgroundColor = Color.FromHex("#DFF0D8");
            }
        }


        private void RadioButton_CheckedChanged1(object sender, CheckedChangedEventArgs e)
        {
            if (_isTraining)
            {
                if (Ans1.Content == correctAnsText) // Если верный ответ
                {
                    Ans1.TextColor = Color.FromHex("#00A28A");
                    Ans1.BackgroundColor = Color.FromHex("#DFF0D8");

                    // Вернуть остальные варианты в обычное состояние
                    Ans2.TextColor = Color.FromHex("#363642");
                    Ans2.BackgroundColor = Color.Transparent;
                    Ans3.TextColor = Color.FromHex("#363642");
                    Ans3.BackgroundColor = Color.Transparent;
                }
                else
                {
                    // Иначе красный
                    Ans1.TextColor = Color.White;
                    Ans1.BackgroundColor = Color.FromHex("#CD5C5C");

                    // Выделение верного
                    if(Ans2.Content == correctAnsText)
                    {
                        Ans2.TextColor = Color.FromHex("#00A28A");
                        Ans2.BackgroundColor = Color.FromHex("#DFF0D8");

                        Ans3.TextColor = Color.FromHex("#363642");
                        Ans3.BackgroundColor = Color.Transparent;
                    }
                    else
                    {
                        Ans3.TextColor = Color.FromHex("#00A28A");
                        Ans3.BackgroundColor = Color.FromHex("#DFF0D8");

                        Ans2.TextColor = Color.FromHex("#363642");
                        Ans2.BackgroundColor = Color.Transparent;
                    }
                }
            }
            NextQBtn.IsEnabled = true;
        }
        private void RadioButton_CheckedChanged2(object sender, CheckedChangedEventArgs e)
        {
            if (_isTraining)
            {
                if (Ans2.Content == correctAnsText)
                {
                    Ans2.TextColor = Color.FromHex("#00A28A");
                    Ans2.BackgroundColor = Color.FromHex("#DFF0D8");

                    // Вернуть остальные варианты в обычное состояние
                    Ans1.TextColor = Color.FromHex("#363642");
                    Ans1.BackgroundColor = Color.Transparent;
                    Ans3.TextColor = Color.FromHex("#363642");
                    Ans3.BackgroundColor = Color.Transparent;
                }
                else
                {
                    // Иначе красный
                    Ans2.TextColor = Color.White;
                    Ans2.BackgroundColor = Color.FromHex("#CD5C5C");

                    // Выделение верного
                    if (Ans1.Content == correctAnsText)
                    {
                        Ans1.TextColor = Color.FromHex("#00A28A");
                        Ans1.BackgroundColor = Color.FromHex("#DFF0D8");

                        Ans3.TextColor = Color.FromHex("#363642");
                        Ans3.BackgroundColor = Color.Transparent;
                    }
                    else
                    {
                        Ans3.TextColor = Color.FromHex("#00A28A");
                        Ans3.BackgroundColor = Color.FromHex("#DFF0D8");

                        Ans1.TextColor = Color.FromHex("#363642");
                        Ans1.BackgroundColor = Color.Transparent;
                    }
                }
            }
            NextQBtn.IsEnabled = true;
        }
        private void RadioButton_CheckedChanged3(object sender, CheckedChangedEventArgs e)
        {
            if (_isTraining)
            {
                if (Ans3.Content == correctAnsText)
                {
                    Ans3.TextColor = Color.FromHex("#00A28A");
                    Ans3.BackgroundColor = Color.FromHex("#DFF0D8");

                    // Вернуть остальные варианты в обычное состояние
                    Ans1.TextColor = Color.FromHex("#363642");
                    Ans1.BackgroundColor = Color.Transparent;
                    Ans2.TextColor = Color.FromHex("#363642");
                    Ans2.BackgroundColor = Color.Transparent;
                }
                else
                {
                    // Иначе красный
                    Ans3.TextColor = Color.White;
                    Ans3.BackgroundColor = Color.FromHex("#CD5C5C");

                    // Выделение верного
                    if (Ans1.Content == correctAnsText)
                    {
                        Ans1.TextColor = Color.FromHex("#00A28A");
                        Ans1.BackgroundColor = Color.FromHex("#DFF0D8");

                        Ans2.TextColor = Color.FromHex("#363642");
                        Ans2.BackgroundColor = Color.Transparent;
                    }
                    else
                    {
                        Ans2.TextColor = Color.FromHex("#00A28A");
                        Ans2.BackgroundColor = Color.FromHex("#DFF0D8");

                        Ans1.TextColor = Color.FromHex("#363642");
                        Ans1.BackgroundColor = Color.Transparent;
                    }
                }
            }
            NextQBtn.IsEnabled = true;
        }


        public TestSlide(bool CHECK1, bool CHECK2, bool CHECK3, bool CHECK4, bool isTraining)
        {
            _CHECK1 = CHECK1;
            _CHECK2 = CHECK2;
            _CHECK3 = CHECK3;
            _CHECK4 = CHECK4;
            _isTraining = isTraining;
            //_CHECK4 = true;
            //_isTraining = true;

            if (_isTraining) _CHECK3 = false;

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


            counterQ = 0;
            // Тест, без номера по приказу, вопросы в перемешку, ответы по порядку
            if (!_isTraining)
            {
                maxQ = 6;                

                LoadQuestion(counterQ, maxQ, _CHECK1, _CHECK2, _CHECK3, _CHECK4, _isTraining);                
            }
            else // Тренировка
            {
                maxQ = 693; //693

                LoadQuestion(counterQ, maxQ, _CHECK1, _CHECK2, _CHECK3, _CHECK4, _isTraining);
            }
        }
    }
}