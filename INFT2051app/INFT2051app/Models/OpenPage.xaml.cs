using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace INFT2051app.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenPage : ContentPage
    {
        public OpenPage()
        {
            InitializeComponent();
        }

        
        //private async Task TaskAsync<async>(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new MainPage());
        //}

        //public void OpenMainPage(object sender, EventArgs e)
        //{
        //    Task.Run(async () =>
        //    {
        //        await Navigation.PushAsync(new MainPage());
        //    });
        //}
}
}