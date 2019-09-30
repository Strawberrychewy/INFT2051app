using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051app {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupFoodShop : PopupPage {

        FoodList foodList { get; set; }

        List<FoodItem> foodShop;
        FoodItem current;
        public int credits { get; set;}

        public event EventHandler PurchaseSucceeded;
        public event EventHandler PurchaseFailed;

        public PopupFoodShop() {
            InitializeComponent();

            foodList = new FoodList();
            foodShop = foodList.resetShop();

            GenerateButtons();
        }

        public void GenerateButtons() {
            /*
             * This function generates buttons in the front end
             */
            food_grid.Children.Clear();
            int index = 0;
            for (int row = 0; row < 4; row += 2) {
                for (int column = 0; column < 4; column++) {
                    //Make new Button
                    Button button = new Button();
                    button.Text = foodShop.ElementAt(index).Name;//Text (Remove for button image)
                    button.CornerRadius = 10;//Corner Radius
                    button.Clicked += ButtonClicked;
                    Grid.SetRow(button, row);//Sets the ROW
                    Grid.SetColumn(button, column);//Sets the COLUMN
                    food_grid.Children.Add(button);


                    //Make new Label for said button
                    Label label = new Label();
                    label.Text = "[" + (row + 1) + ", " + column + "]";//Text
                    Grid.SetRow(label, row + 1);//Sets the ROW (Keep in mind that this is a row below the button)
                    Grid.SetColumn(label, column);//Sets the COLUMN
                    food_grid.Children.Add(label);

                    index++;
                }


            }

        }

        public void ClearCurrent() {
            current = null;
        }

        public string getCurrent() {
            if (current == null) {
                return "";
            } else {
                return current.Name;
            }
        }

        private async void ButtonClicked(object sender, EventArgs e) {
            /*
             * Pulls the item from the list and checks
             * 
             * 
             * 
             */
            Button button = (Button)sender;//Identify sender as button
            current = foodList.findFoodItem(button.Text);//set current as the item whose button is pressed
            if (credits >= current.Cost) {
                credits -= current.Cost;
                PurchaseSucceeded(this, EventArgs.Empty);
            } else {
                PurchaseFailed(this, EventArgs.Empty);
            }
            await PopupNavigation.Instance.PopAsync(true);
        }

    }
}