using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using SalesApp.models;
using SalesApp.Pages;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class SalesQuotationsListviewDetail : ContentPage
    {
        int saleoder_id = 0;
        public SalesQuotationsListviewDetail(SalesQuotation item)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //    App.orderLineList = item.order_line;

            Cus.Text = item.customer;
            CD.Text = item.DateOrder;
            PT.Text = item.payment_term;
            SP.Text = item.sales_person;
            ST.Text = item.sales_team;
            CR.Text = item.customer_reference;
            FP.Text = item.fiscal_position;
            saleoder_id = item.id;


            orderListview.ItemsSource = item.order_line;

            var backImgRecognizer = new TapGestureRecognizer();
            backImgRecognizer.Tapped += async (s, e) =>
            {
                // handle the tap              
                var currentpage = new LoadingAlert();
                await PopupNavigation.PushAsync(currentpage);
                Application.Current.MainPage = new MasterPage(new CrmTabbedPage());
                //  orderListview.ItemsSource = null;
                Loadingalertcall();

            };
            backImg.GestureRecognizers.Add(backImgRecognizer);
        }

        async void Loadingalertcall()
        {
            await PopupNavigation.PopAllAsync();
        }

        private void ConfirmSOClicked(object sender, EventArgs ea)
        {
            String res = Controller.InstanceCreation().SaleOrderConfirm("sale.order", "confirm_sale_quotation", saleoder_id);

          //  String res = "Stock not available for this products : \n Down payment";

            //  var data = await DisplayAlert("Logout Alert", "Are you sure you want to log out?", "OK", "Cancel");
            if (res.Contains("\n"))
            {
                string[] stringSeparators = new string[] { "\n" };
                string[] results = res.Split(stringSeparators, StringSplitOptions.None);

                String first = results[0];
                String second = results[1];
               
                DisplayAlert(first, second, "Ok");
            }

            else
            {

                if (res == "Success")
                {
                    DisplayAlert("Alert", "Sale Order Confirmed Successfully", "Ok");
                    Application.Current.MainPage = new MasterPage(new CrmTabbedPage());
                }
                else
                {
                    DisplayAlert("Alert", res, "Ok");

                    //  orderListview.ItemsSource = null;
                    // Loadingalertcall();
                }
            }


        }


        private void Tab1Clicked(object sender, EventArgs ea)
        {
            tab1stack.BackgroundColor = Color.Silver;
            tab1.BackgroundColor = Color.Silver;
            tab2stack.BackgroundColor = Color.White;
            tab2.BackgroundColor = Color.White;
            tab2frame.BackgroundColor = Color.Gray;
            tab2borderstack.BackgroundColor = Color.White;
            orderLineList.IsVisible = true;
            OtherInfoStack1.IsVisible = false;
            OtherInfoStack2.IsVisible = false;
            tab1frame.BackgroundColor = Color.Silver;
            tab1borderstack.BackgroundColor = Color.Silver;
            OrderLineList1.IsVisible = true;
        }

        private void Tab2Clicked(object sender, EventArgs ea)
        {
            tab2stack.BackgroundColor = Color.Silver;
            tab2.BackgroundColor = Color.Silver;
            tab1stack.BackgroundColor = Color.White;
            tab1.BackgroundColor = Color.White;
            tab2borderstack.BackgroundColor = Color.Silver;
            tab2frame.BackgroundColor = Color.Silver;
            orderLineList.IsVisible = false;
            OtherInfoStack1.IsVisible = true;
            OtherInfoStack2.IsVisible = true;
            tab1frame.BackgroundColor = Color.Gray;
            tab1borderstack.BackgroundColor = Color.White;
            OrderLineList1.IsVisible = false;

        }

    }
}
