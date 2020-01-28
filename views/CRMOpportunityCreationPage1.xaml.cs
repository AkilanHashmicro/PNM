using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SalesApp.models;
using SalesApp.wizard;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class CRMOpportunityCreationPage1 : PopupPage
    {
        string count = "0";
        Dictionary<string, dynamic> vals = new Dictionary<string, dynamic>();
        Dictionary<string, dynamic> vals1 = new Dictionary<string, dynamic>();
        List<AttendeesList> attList = new List<AttendeesList>();
        List<AttendeesList> attList1 = new List<AttendeesList>();

        List<int> atnIdsList = new List<int>();

        List<MeetingLinesList> meetingLineList = new List<MeetingLinesList>();

        double duration = 0;

        public CRMOpportunityCreationPage1()
        {
            InitializeComponent();

            //if(App.cpstring == "lead")
            //{
            //    HideLbl.Text = "Lead Creation";
            //    cusGrid.IsVisible = false;
            //}

            //else if(App.cpstring == "activity")
            //{
            //    HideLbl.Text = "Activity Creation";
            //}

            cuspicker1.ItemsSource = App.cusdict.Select(d => d.Value).ToList();
            cuspicker1.SelectedIndex = 0;

            stagePicker.ItemsSource = App.stageList.Select(x => x.name).ToList();
            stagePicker.SelectedIndex = 0;

            nextActPicker.ItemsSource = App.nextActivityList.Select(x => x.name).ToList();
            nextActPicker.SelectedIndex = 0;

            attnListView.HeightRequest = 0;
            meetingsListview.HeightRequest = 0;

            attnPicker.ItemsSource = App.cusdict.Select(d => d.Value).ToList();
            attnPicker.SelectedIndex = -1;
            //var backRecognizer = new TapGestureRecognizer();
            //backRecognizer.Tapped += (s, e) => {

            //    //  Navigation.PushModalAsync(new MasterPage(new OppurtunityPage()));
            //  //  App.Current.MainPage = new MasterPage(new CrmTabbedPage());
            //};
            //backImg.GestureRecognizers.Add(backRecognizer);


            atnIdsList.Add(App.partner_id);
            var keyValuePair = App.cusdict.Single(x => x.Key == App.partner_id);
            string value = keyValuePair.Value;
            attList.Add(new AttendeesList(value));
            attnListView.ItemsSource = attList;

            attnListView.ItemsSource = attList;
            attnListView.RowHeight = 30;
            attnListView.HeightRequest = 30 * attList.Count;


            var empstar1Recognizer = new TapGestureRecognizer();
            empstar1Recognizer.Tapped += (s, e) => {

                str1.IsVisible = true;
                em1.IsVisible = false;
                str2.IsVisible = false;
                str3.IsVisible = false;
                em2.IsVisible = true;
                em3.IsVisible = true;
                //  RatingLayout1.IsVisible = true;
                //RatingLayout2.IsVisible = false;
                //RatingLayout3.IsVisible = false;
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
                count ="2";
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

            var AirConImgRecognizer = new TapGestureRecognizer();
            AirConImgRecognizer.Tapped += (s, e) => {
                // handle the tap              
                addMeetings.IsVisible = true;
                attnListView.IsVisible = true;
                AddAirCon.IsVisible = false;
                addbtnstack.IsVisible = true;
                atnstack.IsVisible = true;
                AddAirCon1.IsVisible = true;
             //   Navigation.PushPopupAsync(new AddCrmOppMeetingWizard());
               // Navigation.PushPopupAsync(new AddOppMeetingWizard());
            };
            AddAirCon.GestureRecognizers.Add(AirConImgRecognizer);


            var AirConImgRecognizer1 = new TapGestureRecognizer();
            AirConImgRecognizer1.Tapped += (s, e) => {
                attnPicker.IsVisible = true;
                attnListView.ItemsSource = attList;
                attnListView.RowHeight = 30;
                attnListView.HeightRequest = 30 * attList.Count;
            };
            AddAirCon1.GestureRecognizers.Add(AirConImgRecognizer1);

        }
               
        private void Tab1Clicked(object sender, EventArgs ea)
        {
            tab1stack.BackgroundColor = Color.Silver;
            tab1.BackgroundColor = Color.Silver;
            tab2stack.BackgroundColor = Color.White;
            tab2.BackgroundColor = Color.White;
            tab2frame.BackgroundColor = Color.Gray;
            tab2borderstack.BackgroundColor = Color.White;
            notesList.IsVisible = true;
            meetingsList.IsVisible = false;
            // OtherInfoStack2.IsVisible = false;
            tab1frame.BackgroundColor = Color.Silver;
            tab1borderstack.BackgroundColor = Color.Silver;
            //OrderLineList1.IsVisible = true;
            atnstack.IsVisible = false;
            addMeetings.IsVisible = false;
            addbtnstack.IsVisible = false;
        }

        private void Tab2Clicked(object sender, EventArgs ea)
        {
            tab2stack.BackgroundColor = Color.Silver;
            tab2.BackgroundColor = Color.Silver;
            tab1stack.BackgroundColor = Color.White;
            tab1.BackgroundColor = Color.White;
            tab2borderstack.BackgroundColor = Color.Silver;
            tab2frame.BackgroundColor = Color.Silver;
            notesList.IsVisible = false;
            meetingsList.IsVisible = true;
            tab1frame.BackgroundColor = Color.Gray;
            tab1borderstack.BackgroundColor = Color.White;
            AddAirCon.IsVisible = true;

            atnstack.IsVisible = false;
            addMeetings.IsVisible = false;
            addbtnstack.IsVisible = false;

        }

        public void myPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            attnListView.ItemsSource = attList1;
            attnPicker.IsVisible = false;
            attList.Add(new AttendeesList(attnPicker.SelectedItem.ToString()));

            int cusid = App.cusdict.FirstOrDefault(x => x.Value == attnPicker.SelectedItem.ToString()).Key;
            atnIdsList.Add(cusid);

            atnIdsList = atnIdsList.GroupBy(i => i).Select(g => g.First()).ToList();
            attList = attList.GroupBy(i => i.name).Select(g => g.First()).ToList();

            attnListView.ItemsSource = attList;
            attnListView.RowHeight = 30;
            attnListView.HeightRequest = 30 * attList.Count;

            return;         
        }

        private void Button_Add_Clicked1(object sender, EventArgs e)
        {
            meetingsListview.ItemsSource = null;
            AddAirCon.IsVisible = true;
            addbtnstack.IsVisible = false;
            addMeetings.IsVisible = false;

            atnstack.IsVisible = false;
            AddAirCon1.IsVisible = false;

            try
            {
                 duration = Convert.ToDouble(Dur1.Text);
            }

            catch
            {
                 duration = 0;
            }


            DateTime startdateTime;
            DateTime stopdateTime;;

            try
            {
                startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            }

            catch (Exception exe)
            {
                try
                {
                    startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "MM/dd/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                }

                catch (Exception exa)
                {
                    try
                    {
                        startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    }
                    catch (Exception ea1)
                    {

                        try
                        {
                            startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        }
                        catch
                        {

                            try
                            {
                                startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                                try
                                {
                                    startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                }
                                catch
                                {

                                    try
                                    {
                                        startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                    }
                                    catch
                                    {

                                        try
                                        {
                                            startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                                        }

                                        catch
                                        {

                                            startdateTime = DateTime.ParseExact(sDate.Date.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            string start_date = startdateTime.ToString("yyyy-MM-dd HH:mm:ss");


            try
            {
                stopdateTime = DateTime.ParseExact(sDate.Date.ToString(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            }

            catch (Exception exe)
            {
                try
                {
                    stopdateTime = DateTime.ParseExact(eDate.Date.ToString(), "MM/dd/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                }

                catch (Exception exa)
                {
                    try
                    {
                        stopdateTime = DateTime.ParseExact(eDate.Date.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    }
                    catch (Exception ea1)
                    {

                        try
                        {
                            stopdateTime = DateTime.ParseExact(eDate.Date.ToString(), "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        }
                        catch
                        {

                            try
                            {
                                stopdateTime = DateTime.ParseExact(eDate.Date.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                                try
                                {
                                    stopdateTime = DateTime.ParseExact(eDate.Date.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                }
                                catch
                                {

                                    try
                                    {
                                        stopdateTime = DateTime.ParseExact(eDate.Date.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                    }
                                    catch
                                    {

                                        try
                                        {
                                            stopdateTime = DateTime.ParseExact(eDate.Date.ToString(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                                        }

                                        catch
                                        {

                                            stopdateTime = DateTime.ParseExact(eDate.Date.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }



            string end_date = stopdateTime.ToString("yyyy-MM-dd HH:mm:ss");


            meetingLineList.Add(new MeetingLinesList(sub1.Text, start_date, end_date, loc1.Text, duration, atnIdsList));

            meetingsListview.ItemsSource = meetingLineList;
            meetingsListview.RowHeight = 40;
            meetingsListview.HeightRequest = 40 * meetingLineList.Count;

            if(meetingLineList.Count > 0)
            {
                createStackLayout.IsVisible = true;
            }
                                        
        }

        private void createClicked(object sender, EventArgs ea)
        {            
            var cusid = App.cusdict.FirstOrDefault(x => x.Value == cuspicker1.SelectedItem.ToString()).Key;
            vals["customer"] = cusid;
            vals["oppurtunity_title"] = oppTitle.Text;
            vals["email"] = eMail.Text;
            vals["phone"] = phone.Text;

          //  String picker_date = nextActDatePicker.Date.ToString();
            DateTime startdateTime;


            try
            {
                startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            }

            catch (Exception exe)
            {
                try
                {
                    startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "MM/dd/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                }

                catch (Exception exa)
                {
                    try
                    {
                        startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    }
                    catch (Exception ea1)
                    {

                        try
                        {
                            startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        }
                        catch
                        {

                            try
                            {
                                startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                                try
                                {
                                    startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                }
                                catch
                                {

                                    try
                                    {
                                        startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                    }
                                    catch
                                    {

                                        try
                                        {
                                            startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                                        }

                                        catch
                                        {

                                            startdateTime = DateTime.ParseExact(nextActDatePicker.Date.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            string nextactdatepicker_date = startdateTime.ToString("yyyy-MM-dd");

           // vals["next_activity_date"] = nextActDatePicker.Date.ToString();

            vals["next_activity_date"] = nextactdatepicker_date;
            vals["next_activity"] = nextActPicker.SelectedItem.ToString();
            vals["next_activity_summary"] = nextActSum.Text;
            vals["expected_revenue"] = Convert.ToDouble( exRev.Text);
            vals["rating"] = count;
            vals["stage"] = stagePicker.SelectedItem;
            vals["internal_notes"] = comments.Text;
            vals["meetings"] = meetingLineList;
            vals["state"] = "new";

            if(cuspicker1.SelectedItem.ToString() == "")           
            {
                var alert = DisplayAlert("Alert", "Please select the customer", "Ok");
            }

            //bool updated = Controller.InstanceCreation().UpdateCRMOpporData("sale.crm", "create_crm_quotations", vals);
            string updated = Controller.InstanceCreation().UpdateCRMOpporData("sale.crm", "create_crm_quotations", vals);
           
            if (updated == "true") 
            {
                // App.Current.MainPage = new MasterPage(new CrmTabbedPage());
                // Navigation.PushPopupAsync(new MasterPage(  );
                DisplayAlert("Alert", "Created Successfull", "Ok");
                Navigation.PopAllPopupAsync();
            }
            else
            {
                DisplayAlert("Alert", "Please try again", "Ok");
            }
                      
        }

        async void ListviewcloseClicked(object sender, EventArgs e1)
        {
            var args = (TappedEventArgs)e1;
            AttendeesList t2 = args.Parameter as AttendeesList;

            var itemToRemove = attList.Single(r => r.name == t2.name);

            attList.Remove(itemToRemove);
            attnListView.ItemsSource = attList;
            attnListView.RowHeight = 30;
            attnListView.HeightRequest = 30 * attList.Count;
            // taxes obj = (taxes)args;
        }

    }
}
