using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenPage : ContentPage
    {
        
        private readonly MainPage GamePage;

        public OpenPage(MainPage gamePage) {
            InitializeComponent();

            GamePage = gamePage;

            Background bg = new Background();
            this.FindByName<Image>("backgroundPic").Source = bg.Source;




        }

        private async void MoveToApp(object sender, EventArgs e) {

            await Navigation.PushAsync(GamePage);
        }

    }
}