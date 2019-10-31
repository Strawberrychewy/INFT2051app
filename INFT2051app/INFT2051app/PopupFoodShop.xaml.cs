using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
/*
 * A6
* NAME: RG.PLUGIN.POPUP
* PURPOSE: To allow for popup menus and UI to be made as a main interface navigational tool
* DATE: 30/9/19
* SOURCE OF CODE AND ASSISTANCE: https://github.com/rotorgames/Rg.Plugins.Popup
* AUTHOR: Kirill Lyubimov
* DESCRIPTION OF ASSISTANCE: Directions on how to setup and create a popup page  
*/
namespace INFT2051app {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupFoodShop : PopupPage {

        public FoodList FoodList { get; set; }
        public List<FoodItem> foodShop;
        public FoodItem current;
  
        public int Credits { get; set;}
        public event EventHandler PurchaseSucceeded;

        public PopupFoodShop(PlayerData playerData) {
            InitializeComponent();

            Credits = playerData.Credits;
            FoodList = new FoodList();
            Reset();
        }

        public void Reset() {
            foodShop = FoodList.ResetShop();

            GenerateButtons();
        }

        public void GenerateButtons() {
            /*
             * This function generates buttons in the front end
             */

            food_grid.Children.Clear();
            int index = 0;
            for (int row = 0; row < 4; row += 2)
            {
                for (int column = 0; column < 3; column++)
                {
                    //Make new Button
                    ImageButton button = new FoodItem(foodShop.ElementAt(index).Name, foodShop.ElementAt(index).Type, foodShop.ElementAt(index).Cost);

                    button.Clicked += ButtonClicked;
                    button.Aspect = Aspect.AspectFit;
                    Grid.SetRow(button, row);//Sets the ROW
                    Grid.SetColumn(button, column);//Sets the COLUMN
                    food_grid.Children.Add(button);


                    //Make new Label for said button
                    Label label = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "" + foodShop.ElementAt(index).Name + "\n[$" + foodShop.ElementAt(index).Cost + "]"//Text
                    };
                    Grid.SetRow(label, row + 1);//Sets the ROW
                    Grid.SetColumn(label, column);//Sets the COLUMN
                    food_grid.Children.Add(label);

                    index++;
                }


            }

        }

        public void ClearCurrent() {
            current = null;
        }

        public string GetCurrent() {
            if (current == null) {
                return "";
            } else {
                return current.Name;
            }
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            foodShop = FoodList.ResetShop();
            shop_label.Text = "Welcome to the Shop! [Credits: " + Credits + "]";
        }

        private async void ButtonClicked(object sender, EventArgs e) {
            /*
             * This Event is triggered upon the cooresponding button pressed when inside the popup
             * 
             */
            var button = sender as FoodItem;//Identify sender as button
            button.IsEnabled = false;//Disable button press, this stops the user from double tapping and losing out on credits
            current = FoodList.FindFoodItem(button.Name);

            //Compare the number of credits with the cost of the item
            if (Credits >= current.Cost) {
                //Pops the shop off the UI stack
                Credits -= current.Cost;
                PurchaseSucceeded(this, EventArgs.Empty);
                GenerateButtons();
                await PopupNavigation.Instance.PopAsync(true);
            } else {
                shop_label.Text = "Insuffient Funds";
            }
            button.IsEnabled = true;//Enable button press so that button may be used again
        }

    }
}