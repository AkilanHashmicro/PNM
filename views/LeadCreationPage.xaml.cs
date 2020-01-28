using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SalesApp.models;
using SalesApp.Pages;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class LeadCreationPage : PopupPage
    {
        string count = "0";
        Dictionary<string, dynamic> vals = new Dictionary<string, dynamic>();
        Dictionary<string, dynamic> vals1 = new Dictionary<string, dynamic>();
        List<AttendeesList> attList = new List<AttendeesList>();
        List<AttendeesList> attList1 = new List<AttendeesList>();

        List<int> atnIdsList = new List<int>();

        List<MeetingLinesList> meetingLineList = new List<MeetingLinesList>();

        double duration = 0;


        public LeadCreationPage()
        {
            InitializeComponent();

            //customer_Picker.ItemsSource = App.cusdict.Select(x => x.Value).ToList();
            //customer_Picker.SelectedIndex = 0;


            salesperson_Picker.ItemsSource = App.salespersons.Select(x => x.Value).ToList();
            salesperson_Picker.SelectedIndex = 0;

            salesteam_Picker.ItemsSource = App.salesteam.Select(x => x.Value).ToList();
            salesteam_Picker.SelectedIndex = 0;

            tagsPicker.ItemsSource = App.crmleadtags.Select(x => x.Value).ToList();
            tagsPicker.SelectedIndex = 0;

            state_picker.ItemsSource = App.statedict.Select(x => x.Value).ToList();
            // state_picker.SelectedIndex = -1;

            country_picker.ItemsSource = App.countrydict.Select(x => x.Value).ToList();
            // country_picker.SelectedIndex = -1;


            var keyValuePair1 = App.salespersons.Single(x => x.Key == App.userid);
            string value1 = keyValuePair1.Value;

            salesperson_Picker.SelectedItem = value1;


            atnIdsList.Add(App.partner_id);
            var keyValuePair = App.cusdict.Single(x => x.Key == App.partner_id);
            string value = keyValuePair.Value;
            attList.Add(new AttendeesList(value));



            var empstar1Recognizer = new TapGestureRecognizer();
            empstar1Recognizer.Tapped += (s, e) => {

                str1.IsVisible = true;
                em1.IsVisible = false;
                str2.IsVisible = false;
                str3.IsVisible = false;
                em2.IsVisible = true;
                em3.IsVisible = true;

                count = "1";
            };
            em1.GestureRecognizers.Add(empstar1Recognizer);

            var empstar2Recognizer = new TapGestureRecognizer();
            empstar2Recognizer.Tapped += (s, e) => {
                str1.IsVisible = true;
                str2.IsVisible = true;
                em2.IsVisible = false;
                em1.IsVisible = false;
                str3.IsVisible = false;
                em3.IsVisible = true;
                count = "2";
            };
            em2.GestureRecognizers.Add(empstar2Recognizer);

            var empstar3Recognizer = new TapGestureRecognizer();
            empstar3Recognizer.Tapped += (s, e) => {
                str1.IsVisible = true;
                str2.IsVisible = true;
                str3.IsVisible = true;
                em3.IsVisible = false;
                em2.IsVisible = false;
                em1.IsVisible = false;
                count = "3";
            };
            em3.GestureRecognizers.Add(empstar3Recognizer);

            var str1Recognizer = new TapGestureRecognizer();
            str1Recognizer.Tapped += (s, e) => {

                str1.IsVisible = true;
                em2.IsVisible = true;
                em3.IsVisible = true;
                str2.IsVisible = false;
                str3.IsVisible = false;

                count = "1";

            };
            str1.GestureRecognizers.Add(str1Recognizer);

            var str2Recognizer = new TapGestureRecognizer();
            str2Recognizer.Tapped += (s, e) => {

                str1.IsVisible = true;
                str2.IsVisible = true;
                str3.IsVisible = false;
                em1.IsVisible = false;
                em2.IsVisible = false;
                em3.IsVisible = true;

                count = "2";

            };
            str2.GestureRecognizers.Add(str2Recognizer);

            var str3Recognizer = new TapGestureRecognizer();
            str3Recognizer.Tapped += (s, e) => {

                str1.IsVisible = true;
                str2.IsVisible = true;
                str3.IsVisible = false;
                em1.IsVisible = false;
                em2.IsVisible = false;
                em3.IsVisible = true;

                count = "3";

            };
            str3.GestureRecognizers.Add(str3Recognizer);

        }

        async void Loadingalertcall()
        {
            await PopupNavigation.PopAllAsync();
        }

        private async void createClickedAsync(object sender, EventArgs ea)
        {
            if (leadTitle.Text == "")
            {
                leadtitle_alert.IsVisible = true;
            }

            else
            {
                //var cusid = App.cusdict.FirstOrDefault(x => x.Value == customer_Picker.SelectedItem.ToString()).Key;
                //vals["partner_id"] = cusid;
                vals["contact_name"] = compName.Text;

                vals["name"] = leadTitle.Text;

                vals["email_from"] = eMail.Text;
                vals["phone"] = phone.Text;
                vals["function"] = jobPos.Text;
                vals["street"] = street.Text;
                vals["street2"] = street2.Text;
                vals["zip"] = zip.Text;

                var salespersonid = App.salespersons.FirstOrDefault(x => x.Value == salesperson_Picker.SelectedItem.ToString()).Key;
                vals["user_id"] = salespersonid;

                var salesteamid = App.salesteam.FirstOrDefault(x => x.Value == salesteam_Picker.SelectedItem.ToString()).Key;
                vals["team_id"] = salesteamid;

                if (state_picker.SelectedItem == null)
                {
                    vals["state_id"] = false;
                }
                else
                {
                    var stateid = App.statedict.FirstOrDefault(x => x.Value == state_picker.SelectedItem.ToString()).Key;
                    vals["state_id"] = stateid;
                }

                if (country_picker.SelectedItem == null)
                {
                    vals["country_id"] = false;
                }
                else
                {
                    var countryid = App.countrydict.FirstOrDefault(x => x.Value == country_picker.SelectedItem.ToString()).Key;
                    vals["country_id"] = countryid;
                }

                vals["description"] = comments.Text;

                vals["type"] = "lead";

                vals["priority"] = count;

                //bool updated = Controller.InstanceCreation().UpdateCRMOpporData("sale.crm", "create_crm_quotations", vals);
                string updated = Controller.InstanceCreation().UpdateLeadCreationData("crm.lead", "create", vals);

                if (updated == "Odoo Error")
                {
                    // App.Current.MainPage = new MasterPage(new CrmTabbedPage());
                    // Navigation.PushPopupAsync(new MasterPage(  );
                    DisplayAlert("Alert", "Please try again", "Ok");
                }
                else
                {
                    var currentpage = new LoadingAlert();
                    await PopupNavigation.PushAsync(currentpage);

                 await   DisplayAlert("Alert", "Created Successfull", "Ok");

                    List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

                    await Navigation.PopAllPopupAsync();

                    Loadingalertcall();

                    App.Current.MainPage = new MasterPage(new CrmTabbedPage());

                    salesteam_Picker.Unfocus();

                }

            }

        }

    }
}
