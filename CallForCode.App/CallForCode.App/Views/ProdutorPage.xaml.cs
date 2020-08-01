﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallForCode.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdutorPage : ContentPage
    {
        public ProdutorPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.ProdutorViewModel();

            this.ListViewProdutor.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                if (e.SelectedItem == null) return;
                Task.Delay(500);
                if (sender is ListView lv) lv.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Models.AppException>(this, "GetProdutorFail", async (x) =>
            {
                await DisplayAlert("Atenção", x.Message, "OK");
            });

            MessagingCenter.Subscribe<Models.Produtor>(this, "ProdutorSelected", async (x) =>
            {
                await Navigation.PushModalAsync(new ProdutorDetailsPage(x));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Models.AppException>(this, "GetProdutorFail");
        }
    }
}