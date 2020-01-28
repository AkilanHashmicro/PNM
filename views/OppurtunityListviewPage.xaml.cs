﻿using System;
using System.Collections.Generic;
using Plugin.Messaging;
using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using SalesApp.wizard;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class OppurtunityListviewPage : ContentPage
    {
        List<CRMOpportunities> meetingresult = new List<CRMOpportunities>();
        public OppurtunityListviewPage(int cus_id)
        {
            InitializeComponent();

            meetingresult = Controller.InstanceCreation().GetOppssData(cus_id);
          //  meetingsListView.ItemsSource = meetingresult;
            oppurtunityListView.ItemsSource = meetingresult;
        }

        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {
            CRMOpportunities masterItemObj = (CRMOpportunities)ea.Item;
            //Navigation.PushAsync(new CrmLeadDetailPage());
            Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
            //  App.Current.MainPage = new MasterPage(new OppurtunityDetailPage(masterItemObj));
        }

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
    }
}
