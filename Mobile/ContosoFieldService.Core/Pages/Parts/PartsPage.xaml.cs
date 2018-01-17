using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ContosoFieldService.Pages
{
    public partial class PartsPage : ContentPage
    {
        public PartsPage()
        {
            InitializeComponent();
        }

        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Reset selection
            (sender as ListView).SelectedItem = null;
        }
    }
}
