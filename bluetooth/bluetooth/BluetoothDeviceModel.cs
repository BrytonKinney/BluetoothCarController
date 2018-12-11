using Plugin.Bluetooth;
using Plugin.Bluetooth.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bluetooth
{
    public class BluetoothDeviceModel : BaseViewModel
    {
        async Task ConnectToDevice(IBluetoothDevice dev)
        {
            IsBusy = true;
            await dev.Connect().ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("Successfully connected",
                            $"Connected to {dev.Name}", "OK");
                    });
                    IsBusy = false;
                }
            });
        }

        public ObservableCollection<IBluetoothDevice> Devices { get; set; }
        public Command LoadItemsCommand { get; set; }
        public BluetoothDeviceModel()
        {
            Devices = new ObservableCollection<IBluetoothDevice>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            LoadItemsCommand.Execute(null);
            MessagingCenter.Subscribe<MainPage, IBluetoothDevice>(this, "DeviceSelected",
                async (page, dev) =>
                {
                    await ConnectToDevice(dev);
                    await page.Navigation.PushAsync(new NavigationPage(new CarControllerPage(dev)));
                });
        }
        async Task LoadItems()
        {
            if(IsBusy)
                return;
            IsBusy = true;
            try
            {
                Devices.Clear();
                using(var bt = CrossBluetooth.Current)
                {
                    var bts = await bt.GetPairedDevices();
                    foreach(var device in bts)
                    {
                        Devices.Add(device);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
