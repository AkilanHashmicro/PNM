using Plugin.Messaging;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class ContractDetailWizard : PopupPage
    {
        ContactsList myobj = null;

        public ContractDetailWizard(ContactsList obj)
        {
            InitializeComponent();

            myobj = obj;
            contactImage.Source = obj.CustomerImg;
            name.Text = obj.name;
            email.Text = obj.email;
            mobile.Text = obj.mobile;
            phone.Text = obj.phone;
            position.Text = obj.position;

            if(phone.Text == "")
            {
                phoneimg.IsVisible = false;
            }
            if (mobile.Text == "")
            {
                mobileimg.IsVisible = false;
            }

        }

        async void phoneClicked(object sender, EventArgs e1)
        {
            //var args = (TappedEventArgs)e1;

            //ContactsList myobj = args.Parameter as ContactsList;
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

        async void mobileClicked(object sender, EventArgs e1)
        {
            //var args = (TappedEventArgs)e1;

            //ContactsList myobj = args.Parameter as ContactsList;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall && myobj.mobile != "")
            {
                phoneDialer.MakePhoneCall(myobj.mobile);
            }
            else
            {
               
                await Navigation.PushPopupAsync(new PhoneCallWizard());
            }
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}
