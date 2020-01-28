using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SalesApp.models;
using SalesApp.wizard;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class SalesQuotationCreationPage : PopupPage
    {

        List<KeyValuePair<string, dynamic>> order_lines = new List<KeyValuePair<string, dynamic>>();
        List<int> taxidList = new List<int>();

        List<Dictionary<string, dynamic>> abc = new List<Dictionary<string, dynamic>>();
        List<OrderLinesList> orderLineList1 = new List<OrderLinesList>();
        List<OrderLinesList> orderLineList2 = new List<OrderLinesList>();

        public SalesQuotationCreationPage()
        {
            InitializeComponent();
            orderListview.HeightRequest = 0;

            cuspicker1.ItemsSource = App.cusdict.Select(x => x.Value).ToList();
            cuspicker1.SelectedIndex = 0;

            taxpicker.ItemsSource = App.taxList.Select(x => x.Name).ToList();
            taxpicker.SelectedIndex = 0;

            ptpicker.ItemsSource = App.paytermList.Select(x => x.name).ToList();
            taxpicker.SelectedIndex = 0;

            var AirConImgRecognizer = new TapGestureRecognizer();
            AirConImgRecognizer.Tapped += (s, e) => {
                // handle the tap 
                pd.ItemsSource = App.productList.Select(x => x.Name).ToList();
                pd.SelectedIndex = 0;
                airconImg1.IsVisible = true;
                AddAirCon.IsVisible = false;
                orderLineGrid.IsVisible = true;

                taxlistviewGrid.IsVisible = true;
                addtaxGrid.IsVisible = true;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
                taxpicker.SelectedIndex = 0;
                taxListView.ItemsSource = null;
                orderListview.ItemsSource = orderLineList1;
            };
            AddAirCon.GestureRecognizers.Add(AirConImgRecognizer);

            var AirConImgRecognizer1 = new TapGestureRecognizer();
            AirConImgRecognizer1.Tapped += (s, e) => {
                // handle the tap 

                airconImg1.IsVisible = false;
                taxlistviewGrid.IsVisible = false;
                addtaxGrid.IsVisible = false;

                orderListview.ItemsSource = orderLineList2;
                Dictionary<string, dynamic> xyz = new Dictionary<string, dynamic>();

                if (up.Text == "" || oqty.Text == null)
                {
                    DisplayAlert("Alert", "Please fill all the fields", "Ok");
                    orderLineGrid.IsVisible = false;
                    airconImg.IsVisible = true;
                    AddAirCon.IsVisible = true;
                }
                else
                {
                    xyz.Add("product", pd.SelectedItem.ToString());
                    xyz.Add("ordered_qty", Convert.ToDouble(oqty.Text));
                    xyz.Add("unit_price", Convert.ToDouble(up.Text));

                    orderLineList1.Add(new OrderLinesList(pd.SelectedItem.ToString(), Convert.ToDouble(oqty.Text), Convert.ToDouble(up.Text), taxidList));
                    //  abc.Add(new Dictionary<string, dynamic>(xyz));

                    orderListview.ItemsSource = orderLineList1;
                    orderListview.RowHeight = 40;
                    orderListview.HeightRequest = 40 * orderLineList1.Count;

                    orderLineGrid.IsVisible = false;
                    airconImg.IsVisible = true;
                    AddAirCon.IsVisible = true;

                }

            };
            AddAirCon1.GestureRecognizers.Add(AirConImgRecognizer1);

            var taxImgRecognizer = new TapGestureRecognizer();
            taxImgRecognizer.Tapped += (s, e) => {
                // handle the tap 
                taxListView.ItemsSource = null;

                App.taxListRemove.Add(new taxes(taxpicker.SelectedItem.ToString()));

                App.taxListRemove =   App.taxListRemove.GroupBy(i => i.Name).Select(g => g.First()).ToList();
               // taxpicker.IsVisible = false;
                taxStackLayout.IsVisible = true;
                taxListView.ItemsSource = App.taxListRemove;

                taxListView.RowHeight = 40;
                taxListView.HeightRequest = 40 * App.taxListRemove.Count;

               // taxpickstringList.Add(taxpicker.SelectedItem.ToString());
                var taxesid =
               (
               from i in App.taxList
               where i.Name == taxpicker.SelectedItem.ToString()

               select new
               {
                   i.Id,
               }
               ).ToList();

                foreach (var person in taxesid)
                {
                    int selecttaxid = person.Id;
                    taxidList.Add(selecttaxid);
                    taxidList = taxidList.GroupBy(i => i).Select(g => g.First()).ToList();
                }
            };
            Addtax.GestureRecognizers.Add(taxImgRecognizer);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string, string>("MyApp", "NotifyMsg", (sender, arg) =>
            {
                HideLbl.Text = "New Quotation Creation";

            });
        }


        private void Button_Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAllPopupAsync();
        }

        private void Button_Add_Clicked(object sender, EventArgs e)
        {
            int selectpaytermid = 0;
            Dictionary<string, dynamic> vals = new Dictionary<string, dynamic>();

            if (pd.SelectedIndex == -1 || up.Text == "0" || oqty.Text == "0" || taxpicker.SelectedIndex == -1)

            {                
                DisplayAlert("Alert", "Please select atleast one", "Ok");
            }

            else
            {
               // vals["customer"] = cuspicker1.SelectedItem;

                vals["order_date"] = orDatePicker.Date;
                vals["expiration_date"] = exDatePicker.Date;

                if (ptpicker.SelectedItem == null)
                {
                    vals["payment_terms"] = false;
                }
                else
                {
                    var paytermid =
                    (
                            from i in App.paytermList
                            where i.name == ptpicker.SelectedItem.ToString()
                            select new
                            {
                                i.id,
                            }
               ).ToList();

                    foreach (var person in paytermid)
                    {
                        selectpaytermid = person.id;
                    }

                    vals["payment_terms"] = selectpaytermid;
                }


                vals["user_id"] = App.userid;

                var cusid = App.cusdict.FirstOrDefault(x => x.Value == cuspicker1.SelectedItem.ToString()).Key;
                vals["customer"] = cusid;

                vals["order_lines"] = orderLineList1;
                vals["state"] = "draft";
                string updated = Controller.InstanceCreation().UpdateCRMOpporData("sale.crm", "create_sale_quotation", vals);
                if (updated == "true")
                {
                    // App.Current.MainPage = new MasterPage(new CrmTabbedPage());
                   // Navigation.PushPopupAsync(new MasterPage(  );
                    Navigation.PopAllPopupAsync();
                }

                else
                {
                    DisplayAlert("Alert", "Please try again", "Ok");
                }

            }
        }

        private void Button_Add_Clicked1(object sender, EventArgs e)
        {
            airconImg1.IsVisible = false;
            taxlistviewGrid.IsVisible = false;
            addtaxGrid.IsVisible = false;

            orderListview.ItemsSource = orderLineList2;
            Dictionary<string, dynamic> xyz = new Dictionary<string, dynamic>();

            if (up.Text == "" || oqty.Text == null)
            {

                DisplayAlert("Alert", "Please fill all the fields", "Ok");

                orderLineGrid.IsVisible = false;
                airconImg.IsVisible = true;
                AddAirCon.IsVisible = true;
            }
            else
            {
                xyz.Add("product", pd.SelectedItem.ToString());
                xyz.Add("ordered_qty", Convert.ToDouble(oqty.Text));
                xyz.Add("unit_price", Convert.ToDouble(up.Text));


                 orderLineList1.Add(new OrderLinesList(pd.SelectedItem.ToString(), Convert.ToDouble(oqty.Text), Convert.ToDouble(up.Text), taxidList));
                //  abc.Add(new Dictionary<string, dynamic>(xyz));

                orderListview.ItemsSource = orderLineList1;
                orderListview.RowHeight = 40;

                orderLineGrid.IsVisible = false;
                airconImg.IsVisible = true;
                AddAirCon.IsVisible = true;

                orderListview.HeightRequest = 40 * orderLineList1.Count;
            }
        }

        async void ListviewcloseClicked(object sender, EventArgs e1)
        {
            var args = (TappedEventArgs)e1;
            taxes t2 = args.Parameter as taxes;
              
            var itemToRemove = App.taxListRemove.Single(r => r.Name == t2.Name);

            App.taxListRemove.Remove(itemToRemove);
            taxListView.ItemsSource = App.taxListRemove;
            taxListView.RowHeight = 30;
            taxListView.HeightRequest = 30 * App.taxListRemove.Count;

        }

    }
}
