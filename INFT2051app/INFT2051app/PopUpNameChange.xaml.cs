using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpNameChange : PopupPage
    {
        // readonly Entry petName;
        readonly Pet pet;

        public PopUpNameChange()
        {
            InitializeComponent();

            //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/entry
            // petName = new Entry { Placeholder="Charles"};
            //var petName = new Entry { Placeholder = "Charles" };
        }

        private async void ChangeName(object sender, EventArgs e)
        {
            //pet.NickName = this.FindByName<Entry>("NewPetName").Text;
            await PopupNavigation.Instance.PopAsync();
        }
        

    }


}