using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
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
    public partial class SalesOrderPage : ContentPage
    {
        //public List<SalesModel> getSalesOrderDetails()
        //{
        //    //List<SalesModel> quotationData = Controller.InstanceCreation().salesOrderData("sales");
        //    //return quotationData;

        //    List<SalesModel> quotationData1 = Controller.InstanceCreation().salesOrderData1();
        //    return quotationData1;

        //}

        public SalesOrderPage()
        {
            Title = "Sales Order";

            BackgroundColor = Color.White;
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                searchBar.HeightRequest = 40.0;
            }

            // salesOrderListView.ItemsSource = getSalesOrderDetails();
            salesOrderListView.ItemsSource = App.salesOrderList;
            salesOrderListView.Refreshing += this.RefreshRequested;
        }


        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {

            App.Current.MainPage = new MasterPage(new SalesOrderListviewDetail(ea.Item as SalesOrder));
          // App.Current.MainPage = new MasterPage(new SalesOrderDetailPage(ea.Item as SalesModel));
        }

        private async void RefreshRequested(object sender, object e)
        {
            await Task.Delay(2000);
            List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
            salesOrderListView.ItemsSource = App.salesOrderList;
            salesOrderListView.EndRefresh();
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
                salesOrderListView.ItemsSource = App.salesOrderList; ;
            }

            else
            {
                salesOrderListView.ItemsSource = App.salesOrderList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void Toolbar_Filter_Activated(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new FilterPopupPage());
            //Navigation.PushPopupAsync(new CrmFilterWizard());
        }

        private void Handle_FabClicked(object sender, EventArgs e)
        {

        }
    }
}