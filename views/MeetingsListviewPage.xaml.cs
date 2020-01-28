using System;
using System.Collections.Generic;
using SalesApp.models;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;


namespace SalesApp.views
{
    public partial class MeetingsListviewPage : ContentPage
    {
        List<all_events> meetingresult = new List<all_events>();

        public MeetingsListviewPage(int cus_id)
        {
            InitializeComponent();

            meetingresult = Controller.InstanceCreation().GetMeetingsData(cus_id);
            meetingsListView.ItemsSource = meetingresult;

        }

        private void ViewCellPending_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            obj.View.BackgroundColor = Color.White;
        }

        private void meetingListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            all_events modelObj = e.Item as all_events;
            Navigation.PushAsync(new CalendarDetailPage(modelObj));
        }

       

    }
}
