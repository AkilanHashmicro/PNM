using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using SalesApp.wizard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CrmLeadPage : ContentPage
    {
        List<CRMLead> crmLeadAll;
              
        public CrmLeadPage()
        {
            Title = "CRM Leads";
            BackgroundColor = Color.White;
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                searchBar.HeightRequest = 40.0;
            }

            crmLeadListView.ItemsSource = App.crmList;
            crmLeadListView.Refreshing += this.RefreshRequested;

            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += (s, e) => {
               // Navigation.PushPopupAsync(new OppurtunityDetailPage());
              //  App.cpstring = "lead";
              //  Navigation.PushPopupAsync(new CRMOpportunityCreationPage1());
                Navigation.PushPopupAsync(new LeadCreationPage()); 

            };
            plus.GestureRecognizers.Add(plusRecognizer);
        }

        private async void RefreshRequested(object sender, object e)
        {
            await Task.Delay(2000);
          List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
          //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmFilterData();

            crmLeadListView.ItemsSource = App.crmList;
            crmLeadListView.EndRefresh();
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
            Navigation.PushPopupAsync(new FilterPopupPage());
        }


        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    MessagingCenter.Subscribe<Dictionary<string,dynamic>, string>(vals, "NotifyMsg", (sender, arg) =>
        //    {
        //        string retarg = arg;
        //        List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
        //    });
        //}

        async void phoneClicked(object sender, EventArgs e1)
        {
            var args = (TappedEventArgs)e1;

            CRMLead myobj = args.Parameter as CRMLead;
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
                crmLeadListView.ItemsSource = App.crmList;
            }
            else
            {
                crmLeadListView.ItemsSource = App.crmList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void crmLeadListView_ItemTapped(object sender, ItemTappedEventArgs ea)
        {
            CRMLead masterItemObj = (CRMLead)ea.Item;

            Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj,"Lead"));
        }
    }
}
