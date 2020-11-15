using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SimplyWeather2.Controls
{
    public partial class BasePage : ContentPage
    {
        public IList<View> BasePageContent => BaseContentGrid.Children;
        public static readonly BindableProperty HasLocationMenuItemProperty = BindableProperty.Create("HasLocationMenuItem", typeof(bool), typeof(BasePage), true);


        public bool HasLocationMenuItem
        {
            get { return (bool)GetValue(HasLocationMenuItemProperty); }
            set {
                SetValue(HasLocationMenuItemProperty, value);
                if (value)
                {
                    ShowLocationMenuItem();
                }
                else
                {
                    RemoveLocationMenuItem();
                }
                
            }
        }

        private ToolbarItem LocationItem;

        public BasePage()
        {
            InitializeComponent();

            if(HasLocationMenuItem)
            {
                ShowLocationMenuItem();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(LocationItem != null)
            {
                LocationItem.Clicked += OnNavigateToUpdateLocation;
            }
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (LocationItem != null)
            {
                LocationItem.Clicked -= OnNavigateToUpdateLocation;
            }
        }

        private void OnNavigateToUpdateLocation(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("UpdateLocation", true);
        }

        private void ShowLocationMenuItem()
        {

            LocationItem = new ToolbarItem
            {
                Text = "Location",
                IconImageSource = ImageSource.FromFile("location_icon"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            ToolbarItems.Add(LocationItem);
            LocationItem.Clicked += OnNavigateToUpdateLocation;
        }
                
        private void RemoveLocationMenuItem()
        {
            if(LocationItem != null)
            {
                LocationItem.Clicked -= OnNavigateToUpdateLocation;
                ToolbarItems.Remove(LocationItem);
                LocationItem = null;
            }
        }
    }
}
