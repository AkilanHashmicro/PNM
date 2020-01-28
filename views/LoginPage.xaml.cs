using SalesApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using SalesApp.Pages;
using static SalesApp.models.CRMModel;
using Syncfusion.SfBusyIndicator.XForms;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private Controller controllerObj = Controller.InstanceCreation();

        public LoginPage()
        {
            InitializeComponent();
            loginEntry.Text = "admin";
            passwordEntry.Text = "admin";

            NavigationPage.SetHasNavigationBar(this, false);

        }

        //public void URLTextChanged(object sender, EventArgs e)
        //{
        //    //string urlText = ((Entry)sender).Text;
        //    //dbPicker.Items.Clear();
        //    //dbPicker.IsVisible = false;

        //    //if (Util.Net.ValidateURL(urlText) && urlText.Length > 10)
        //    //{
        //    //    try
        //    //    {
        //    //        String[] dbData = controllerObj.getDatabases(urlText);
        //    //        foreach (String db in dbData)
        //    //        {
        //    //            dbPicker.Items.Add(db);
        //    //        }
        //    //        dbPicker.SelectedIndex = 0;
        //    //        if (dbData.Length >=1)
        //    //        {
        //    //            urlAlert.Text = "Invalid URL";
        //    //            dbPicker.IsVisible = true;
        //    //            urlAlert.IsVisible = false;
        //    //        }

        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        System.Diagnostics.Debug.WriteLine(ex.StackTrace);
        //    //        urlAlert.IsVisible = true;
        //    //    }
        //    //}
        //}

        async void Loadingalertcall()
        {
            await PopupNavigation.PopAllAsync();
        }

        public async void SignInActionAsync(object sender, EventArgs ea)
        {
            // string dbName = "";
            loginAlert.IsVisible = false;
            passwordAlert.IsVisible = false;
            dbPickerAlert.IsVisible = false;

            try
            {
                // UserDialogs.Instance.ShowLoading();
                // await Task.Delay(TimeSpan.FromSeconds(1));

                var currentpage = new LoadingAlert();

                await PopupNavigation.PushAsync(currentpage);

                Settings.UserName = loginEntry.Text;
                Settings.UserPassword = passwordEntry.Text;

                //controllerObj.login("http://salesapp.hashmicro.com", "salesapp", "admin", "admin");

                await Task.Run(() => controllerObj.login("http://beta-dev2.hashmicro.com", "PNM", Settings.UserName, Settings.UserPassword));
                //   List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

                List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

                //await Task.Run(() =>
                //{
                //    List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                //});

                Page pageRef = new CrmTabbedPage();
                App.Current.MainPage = new MasterPage(pageRef);


                //await Task.Run(() =>
                //{

                //    Device.BeginInvokeOnMainThread(() =>
                //    {
                //        Page pageRef = new CrmTabbedPage();
                //        App.Current.MainPage = new MasterPage(pageRef);
                //    });
                //});


                System.Diagnostics.Debug.WriteLine(" WWWWWWWWWWWWWWWWWWWWWWwwwwwwwwwwww ", DateTime.Now.ToLocalTime().ToString());

                Loadingalertcall();
                //UserDialogs.Instance.HideLoading();

            }
            catch
            {
                loginfailedAlert.Text = "Invalid Username or Password.";
                loginfailedAlert.IsVisible = true;

                Loadingalertcall();
            }


        }


    }
}
