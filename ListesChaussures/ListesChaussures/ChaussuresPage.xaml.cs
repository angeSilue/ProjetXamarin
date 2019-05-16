using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListesChaussures.UWP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChaussuresPage : ContentPage
    {
        private ChaussuresDataAccess dataAccess;

        public ChaussuresPage()
        {
            InitializeComponent();

            this.dataAccess = new ChaussuresDataAccess();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // The instance of CustomersDataAccess
            // is the data binding source
            this.BindingContext = this.dataAccess;
        }

        // Save any pending changes
        private void OnSaveClick(object sender, EventArgs e)
        {
            this.dataAccess.SaveAllChaussures();
        }

        // Add a new customer to the Customers collection
        private void OnAddClick(object sender, EventArgs e)
        {
            this.dataAccess.AddNewChaussures();
        }        

        // Remove the current customer
        // If it exist in the database, it will be removed
        // from there too
        private void OnRemoveClick(object sender, EventArgs e)
        {
            var currentChaussures = this.ChaussuresView.SelectedItem as itemChaussures;

            if (currentChaussures != null)
            {
                this.dataAccess.DeleteChaussures(currentChaussures);
            }
        }

        // Remove all customers
        // Use a DisplayAlert object to ask the user's confirmation
        private async void OnRemoveAllClick(object sender, EventArgs e)
        {
            if (this.dataAccess.Chaussures.Any())
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "ete vous certain de vouloir continuer? cette Procedure ne peut etre annulé",
                  "OK", "Cancel");

                if (result == true)
                {
                    this.dataAccess.DeleteAllChaussuress();
                    this.BindingContext = this.dataAccess;
                }
            }
        }

    }
}


        