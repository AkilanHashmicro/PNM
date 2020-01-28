using Rg.Plugins.Popup.Extensions;
using SalesApp.wizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class CustomerListviewDetailPage : ContentPage
    {
        CustomersModel cusobj = new CustomersModel();

        public CustomerListviewDetailPage(CustomersModel obj)
        {
            InitializeComponent();

            cusobj = obj;

            List<string> tagsList = new List<string>();

            List<TagsList> custagsList = new List<TagsList>();

            List<ContactsList> cusnewList = new List<ContactsList>();

            byte[] imageAsBytes = Encoding.UTF8.GetBytes(obj.image_medium);

            byte[] decodedByteArray = System.Convert.FromBase64String(Encoding.UTF8.GetString(imageAsBytes, 0, imageAsBytes.Length));
            var stream = new MemoryStream(decodedByteArray);
            cus_image.Source = ImageSource.FromStream(() => stream);

            name.Text = obj.name;
            email.Text = obj.email;

            mobile_No.Text = obj.phone;
            street.Text = obj.street;
            city.Text = obj.city;
            zip.Text = obj.zip;
            web_text.Text = obj.website;
            oppo_text.Text = obj.crm_count.ToString();
            sales_text.Text = obj.sales_count.ToString();
            meeting_text.Text = obj.meeting_count.ToString();

            cusnewList = obj.contacts;

            CusListView.ItemsSource = cusnewList;

            //  CusListView.ItemsSource = cusmodel;
            foreach (var cusname in obj.tags)
            {
                tagsList.Add(cusname.Value);
                custagsList.Add(new TagsList(cusname.Value));
            }

            //tags strted 

            try
            {
                int cnt1 = 0;
                int cnt = 0;

                for (int row = 0; row < tagsList.Count; row++)
                {
                    tags_grid.IsVisible = true;
                    if (cnt == tagsList.Count) { break; }

                    if (tagsList.Count == 1)
                    {
                        tags_grid.VerticalOptions = LayoutOptions.Center;
                        tags_grid.VerticalOptions = LayoutOptions.Center;
                        tags_grid.HorizontalOptions = LayoutOptions.Center;
                        tags_grid.HorizontalOptions = LayoutOptions.Center;
                    }

                    for (int col = 0; col < 2; col++)
                    {
                        if (cnt == tagsList.Count) { break; }

                        TagsList modelobj = custagsList.ElementAt(cnt1++);
                        //  cnt = cnt + 1;

                        StackLayout nestedStackLayout = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            //  Padding = 10
                            // HeightRequest = 20
                        };

                        Label tagsLabel = new Label
                        {
                            //  Margin = new Thickness(5, 5, 5, 5),
                            Text = modelobj.name,
                            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Start,
                            HorizontalTextAlignment = TextAlignment.Center,
                            //  TextColor = Color.Black,
                            VerticalOptions = LayoutOptions.Start,
                            VerticalTextAlignment = TextAlignment.Center,
                            //HeightRequest =10,
                        };

                        Frame lblframe = new Frame()
                        {
                            CornerRadius = 6,
                            BackgroundColor = Color.Gray,
                            Padding = new Thickness(5),
                            HeightRequest = 15,
                            Margin = new Thickness(3, 3, 3, 3),
                        };

                        lblframe.Content = tagsLabel;

                        nestedStackLayout.Children.Add(lblframe);

                        tags_grid.Children.Add(nestedStackLayout, col, row);
                    }
                }

            }

            catch (Exception ea)
            { System.Diagnostics.Debug.WriteLine("Warning Message : " + ea.Message); }

        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            // obj.View.BackgroundColor = Color.FromHex("#414141");
            obj.View.BackgroundColor = Color.White;
        }

        private void meetingStackClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MeetingsListviewPage(cusobj.id));
        }

        private void oppoClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OppurtunityListviewPage(cusobj.id));
        }

        private void saleClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SaleListviewPage(cusobj.id));
        }

        private void CusListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ContactsList obj = e.Item as ContactsList;
             Navigation.PushPopupAsync(new ContractDetailWizard(obj));

        }
    }
}
