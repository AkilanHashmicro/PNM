using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rg.Plugins.Popup.Services;
using SalesApp.models;
using SalesApp.Pages;
using SalesApp.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SalesApp.models.CRMModel;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {

        //  public static var calattnList = "";

        public static List<next_activity> nextActivityList = new List<next_activity>();
        public static List<ProductsList> productList = new List<ProductsList>();
        public static List<taxes> taxList = new List<taxes>();

        public static List<ContactsCreationList> concreationList = new List<ContactsCreationList>();

        public static List<paytermList> paytermList = new List<paytermList>();

        public static List<taxes> taxListRemove = new List<taxes>();
        public static Dictionary<int, string> cusdict = new Dictionary<int, string>();
        public static Dictionary<int, string> reasondict = new Dictionary<int, string>();
        public static Dictionary<int, string> tagsDict = new Dictionary<int, string>();
        public static List<Customers> cusList = new List<Customers>();
        public static List<stages> stageList = new List<stages>();

        public static List<CRMLead> crmList = new List<CRMLead>();
        public static List<CRMOpportunities> crmOpprList = new List<CRMOpportunities>();
        public static List<SalesQuotation> salesQuotList = new List<SalesQuotation>();
        public static List<SalesOrder> salesOrderList = new List<SalesOrder>();

        public static List<OrderLine> orderLineList = new List<OrderLine>();

        public static int userid = 0;
        public static int partner_id = 0;
        public static string partner_name = "";
        public static string partner_image = "";
        public static string partner_email = "";

        public static string cpstring = "";
        public static string calselecteddate = "";
        public static string filterstring = "Month";
        public static Dictionary<string, dynamic> filterdict = new Dictionary<string, dynamic>();

        public static Dictionary<int, string> salesteam = new Dictionary<int, string>();
        public static Dictionary<int, string> salespersons = new Dictionary<int, string>();
        public static Dictionary<int, string> crmleadtags = new Dictionary<int, string>();
        public static Dictionary<int, string> countrydict = new Dictionary<int, string>();
        public static Dictionary<int, string> statedict = new Dictionary<int, string>();

        public App()
        {
            InitializeComponent();

            //  Settings.UserLoginId = "";

            //  MainPage = new SalesApp.views.MapPage();

            if (Settings.UserName.Length > 0 && Settings.UserPassword.Length > 0)
            {
                App.Current.MainPage = new MasterPage(new LogoutPage());

                //  var currentpage = new LoadingAlert();

                //   Controller.InstanceCreation().login("http://beta-dev1.hashmicro.com", "LBS", Settings.UserName, Settings.UserPassword);

              //  Controller.InstanceCreation().login("http://beta-dev1.hashmicro.com", "telering_dev", Settings.UserName, Settings.UserPassword);

              //  Controller.InstanceCreation().login("http://telering.equip-sapphire.com", "telering", Settings.UserName, Settings.UserPassword);

             //   Controller.InstanceCreation().login("http://bevananda.equip-sapphire.com", "bevananda", Settings.UserName, Settings.UserPassword);

                Controller.InstanceCreation().login("http://beta-dev2.hashmicro.com", "PNM", Settings.UserName, Settings.UserPassword);

                //if (Settings.UserLoginId == "0")
                //{
                //    MainPage = new SignInPage();
                //}
                //else
                //{
                //    Application.Current.MainPage = new MasterPage(new DashBoardPage());
                //}

                List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

                //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmFilterData();

                App.Current.MainPage = new MasterPage(new CrmTabbedPage());

                //   Loadingalertcall();

            }

            else
            {
                //  MainPage = new MainPage();
                MainPage = new SalesApp.views.LoginPage();
                //  MainPage = new SalesApp.views.GoogleMapScreen();
            }

            //      MainPage = new SalesApp.views.CRMLeadEditAddressPage();

        }

        //async void Loadingalertcall()
        //{
        //    await PopupNavigation.PopAllAsync();
        //}


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


    }

}
