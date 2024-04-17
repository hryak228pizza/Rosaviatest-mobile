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
    public partial class ResultSlide : ContentPage
    {
        public ResultSlide(List<byte> inputAns)
        {
            InitializeComponent();

            int sm = 0;
            foreach (byte a in inputAns) sm += a;

            if ((double)sm / (double)inputAns.Count * 100 < 75) Result.Text = "менее 75% ответов верно.";
            else Result.Text = ((int)((double)sm / (double)inputAns.Count * 100)).ToString() + "% верных ответов.";


        }
    }
}