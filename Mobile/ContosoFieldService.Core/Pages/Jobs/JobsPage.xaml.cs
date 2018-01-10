using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ContosoFieldService.Pages
{
    public partial class JobsPage : ContentPage
    {
        public JobsPage()
        {
            InitializeComponent();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            // Reset selection
            (sender as ListView).SelectedItem = null;
        }
    }
}
