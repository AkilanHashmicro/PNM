using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using SalesApp.wizard;
using SalesApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuotationPage : ContentPage
    {
        //public List<SalesModel> getSalesQuotationDetails()
        //{
        //    List<SalesModel> quotationData = Controller.InstanceCreation().salesOrderData("draft");
        //    return quotationData;
        //}

        public QuotationPage()
        {
            Title = "Sales Quotations";

            BackgroundColor = Color.White;
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                searchBar.HeightRequest = 40.0;
            }
          //  salesQuotationListView.ItemsSource = getSalesQuotationDetails();

            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += (s, e) => {
                
                Navigation.PushPopupAsync(new SalesQuotationCreationPage());

            };
            plus.GestureRecognizers.Add(plusRecognizer);

            salesQuotationListView.ItemsSource = App.salesQuotList;

            salesQuotationListView.Refreshing += this.RefreshRequested;
        }

        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {

            App.Current.MainPage = new MasterPage(new SalesQuotationsListviewDetail(ea.Item as SalesQuotation));

        }

        private async void RefreshRequested(object sender, object e)
        {
            await Task.Delay(2000);
            List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

            salesQuotationListView.ItemsSource = App.salesQuotList;
            salesQuotationListView.EndRefresh();
        }

        private void Toolbar_Search_Activated(object sender, EventArgs e)
        {
            if (searchBar.IsVisible)
            {
                searchBar.IsVisible = false;
            }   
            else { searchBar.IsVisible = true; }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                salesQuotationListView.ItemsSource = App.salesQuotList; ;
            }

            else
            {
              //  salesQuotationListView.ItemsSource = App.salesQuotList.Where(x => x.name.ToLower().StartsWith(e.NewTextValue.ToLower()));
                salesQuotationListView.ItemsSource = App.salesQuotList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void Toolbar_Filter_Activated(object sender, EventArgs e)
        {            
                Navigation.PushPopupAsync(new FilterPopupPage());
               //Navigation.PushPopupAsync(new CrmFilterWizard());
        }
    }
}