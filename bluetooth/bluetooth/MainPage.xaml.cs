using Plugin.Bluetooth;
using Plugin.Bluetooth.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bluetooth
{
    public partial class MainPage : ContentPage
    {
        public BluetoothDeviceModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BluetoothDeviceModel();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Plugin.Bluetooth.Abstractions.IBluetoothDevice;
            if(item == null)
                return;
            MessagingCenter.Send(this, "DeviceSelected", item);
           // new Command(async () => await ConnectToDevice(item)).Execute(null);
            ItemList.SelectedItem = null;
        }
    }
}
