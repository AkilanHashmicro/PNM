using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesApp.models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrmTabbedPage : TabbedPage
    {
        public CrmTabbedPage()
        {            
            NavigationPage.SetHasNavigationBar(this, false);

            BarBackgroundColor = Color.FromHex("#414141");
            var crmLeadPage = new NavigationPage(new CrmLeadPage()) { BarBackgroundColor = Color.FromHex("#414141") };
            crmLeadPage.Icon = "lead.png";              
            Children.Add(crmLeadPage);

            var crmOppurtunityPage = new NavigationPage(new OppurtunityPage()) { BarBackgroundColor = Color.FromHex("#414141") };
            crmOppurtunityPage.Icon = "oppurtunity.png";
            //crmOppurtunityPage.Title = "Opportunity";
            Children.Add(crmOppurtunityPage);

            var quotationPage = new NavigationPage(new QuotationPage()) { BarBackgroundColor = Color.FromHex("#414141") };
            quotationPage.Icon = "quotations.png";
            Children.Add(quotationPage);

            var salesOrderPage = new NavigationPage(new SalesOrderPage()) { BarBackgroundColor = Color.FromHex("#414141") };
            salesOrderPage.Icon = "salesorder.png";
            //salesOrderPage.Title = "Sales Order";
            Children.Add(salesOrderPage);
        }
    }
}