using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using SalesApp.wizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SalesApp.Pages;
using static SalesApp.models.CRMModel;
using Plugin.Messaging;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OppurtunityPage : ContentPage
    {
        //List<CrmLeadModel> crmLeadAll;
        //public List<CrmLeadModel> getOppurtunityDetails()
        //{
        //    List<CrmLeadModel> crmLeadData = Controller.InstanceCreation().crmMyOppurtunitiesData();
        //    crmLeadAll = crmLeadData;
        //    return crmLeadData;
        //}

        public OppurtunityPage()
        {
            Title = "CRM Opportunities";

            BackgroundColor = Color.White;
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                searchBar.HeightRequest = 40.0;
            }

            // oppurtunityListView.ItemsSource = getOppurtunityDetails();
            oppurtunityListView.ItemsSource = App.crmOpprList;
            oppurtunityListView.Refreshing += this.RefreshRequested;

            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += (s, e) => {

                App.cpstring = "activity";
                Navigation.PushPopupAsync(new CRMOpportunityCreationPage1());

              //  Navigation.PushPopupAsync(new CRMOpportunityCreationPage());
            };
            plus.GestureRecognizers.Add(plusRecognizer);
         }

        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {
            CRMOpportunities masterItemObj = (CRMOpportunities)ea.Item;
            //Navigation.PushAsync(new CrmLeadDetailPage());

            Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));

            //  App.Current.MainPage = new MasterPage(new OppurtunityDetailPage(masterItemObj));
        }

        private async void RefreshRequested(object sender, object e)
        {
            await Task.Delay(2000);
            List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
           // oppurtunityListView.ItemsSource = this.getOppurtunityDetails();
            oppurtunityListView.ItemsSource = App.crmOpprList;
            oppurtunityListView.EndRefresh();
        }

        private void Toolbar_Search_Activated(object sender, EventArgs e)
        {
            if (searchBar.IsVisible)
            {
                searchBar.IsVisible = false;
            }
            else { searchBar.IsVisible = true; }
        }

        private void Toolbar_Filter_Activated(object sender, EventArgs e)
        {
            // Navigation.PushPopupAsync(new CrmFilterWizard());
            Navigation.PushPopupAsync(new FilterPopupPage()); 
        }

        //private void MenuItem_Clicked(object sender, EventArgs e)
        //{
        //    var mi = ((MenuItem)sender);

        //    CRMLead obj = (CRMLead)mi.CommandParameter;
        //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(obj));
        //}

        async void phoneClicked(object sender, EventArgs e1)
        {
            // taxes myobj = sender as taxes;
            var args = (TappedEventArgs)e1;

            CRMOpportunities myobj = args.Parameter as CRMOpportunities;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall && myobj.phone != "")
            {
                phoneDialer.MakePhoneCall(myobj.phone);
            }
            else
            {
                await Navigation.PushPopupAsync(new PhoneCallWizard());
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                oppurtunityListView.ItemsSource = App.crmOpprList;;
            }

            else
            {
                oppurtunityListView.ItemsSource = App.crmOpprList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }
    }
}