using System;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class Main : TabbedPage
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_OnCurrentPageChanged(object sender, EventArgs e)
        {
            Title = CurrentPage.Title;
        }
    }
}
