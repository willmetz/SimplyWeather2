using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SimplyWeather2.Controls
{
    public partial class BasePage : ContentPage
    {
        public IList<View> BasePageContent => BaseContentGrid.Children;
        public static readonly BindableProperty HasLocationMenuItemProperty = BindableProperty.Create("HasLocationMenuItem", typeof(bool), typeof(BasePage), true);
        public static readonly BindableProperty PageTitleProperty = BindableProperty.Create("PageTitle", typeof(string), typeof(BasePage), string.Empty, propertyChanged:OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is BasePage basePage)
            {
                basePage.Title.Text = (string)newValue;
            }
        }

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

        private string _pageTitle;
        public string PageTitle
        {
            get => _pageTitle;
            set => _pageTitle = value;
        }

        public BasePage()
        {
            InitializeComponent();

            if(HasLocationMenuItem)
            {
                ShowLocationMenuItem();
            }

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LocationMenuItem.Clicked += OnNavigateToUpdateLocation;            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            LocationMenuItem.Clicked -= OnNavigateToUpdateLocation;
        }

        protected void OnNavigateToUpdateLocation(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("UpdateLocation", true);
        }

        private void ShowLocationMenuItem()
        {

            LocationMenuItem.IsVisible = true;
        }
                
        private void RemoveLocationMenuItem()
        {
            LocationMenuItem.IsVisible = false;
        }
    }
}
