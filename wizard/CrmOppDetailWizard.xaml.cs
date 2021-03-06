﻿using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SalesApp.models;
using SalesApp.Pages;
using SalesApp.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SalesApp.models.CRMModel;

namespace SalesApp.wizard
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrmOppDetailWizard : PopupPage
    {
        CRMOpportunities obj = new CRMOpportunities();

        public CrmOppDetailWizard (CRMOpportunities modelObj)
		{
			InitializeComponent ();

            obj = modelObj;

            CustomerNameValue.Text = modelObj.name;
            CrmNameValue.Text = modelObj.customer;
            ExpectedRevenueValue.Text = modelObj.expected_revenue + "$";
            ProbabilityValue.Text = modelObj.probability + "%";
            EmailValue.Text = modelObj.email;
            PhoneValue.Text = modelObj.phone;
            TeamValue.Text = modelObj.team_name;
            NextActivityValue.Text = modelObj.next_activity;
            NextActivityDateValue.Text = modelObj.next_activity;
            ActivityDescritionValue.Text = modelObj.title_action;
            expectedClosingValue.Text = "";
            StreetValue.Text = modelObj.street;
            Street2Value.Text = modelObj.street2;
            CityValue.Text = modelObj.city;
            CountryValue.Text = modelObj.country;
            ContactNameValue.Text = modelObj.contact_name;
            ContactMobileValue.Text = modelObj.mobile;
            int priorityCnt = Convert.ToInt32(modelObj.priority);

            if (priorityCnt == 3)
            { RatingLayout3.IsVisible = true; }
            else if (priorityCnt == 2)
            { RatingLayout2.IsVisible = true; }
            else if (priorityCnt == 1)
            {
                RatingLayout1.IsVisible = true;
            }
            else { }
        }

        private async Task ButtonMarkWon_ClickedAsync(object sender, EventArgs e)
        {
            var currentpage = new LoadingAlert();
            await PopupNavigation.PushAsync(currentpage);

            bool updated = Controller.InstanceCreation().UpdateMarkWon(obj.id);

            if (updated)
            {
                await DisplayAlert("Alert", "State successfully moved", "Ok");

                List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

                App.Current.MainPage = new MasterPage(new CrmTabbedPage());
                Loadingalertcall();
            }

            else
            {
                await DisplayAlert("Alert", "Please try again", "Ok");
                Loadingalertcall();
            }

        }

        async void Loadingalertcall()
        {
            await PopupNavigation.PopAllAsync();
        }

        private void ButtonMarkLost_Clicked(object sender, EventArgs e)
        {
            // Navigation.PushPopupAsync(new MarkLostPopupPage());
            List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

            PopupNavigation.PushAsync(new MarkLostPopupPage(obj.id));
        }

        private void ButtonNewQuotation_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SalesQuotationCreationPage());
            MessagingCenter.Send<string, string>("MyApp", "NotifyMsg", "From CRM Opp");
           
        }

    }
}