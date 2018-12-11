using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Bluetooth.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bluetooth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarControllerPage : ContentPage
	{
	    public IBluetoothDevice Car { get; set; }
        public CarControllerViewModel ViewModel { get; set; }
        public CarControllerPage() {}
		public CarControllerPage (IBluetoothDevice device)
		{
            BindingContext = ViewModel = new CarControllerViewModel();
			InitializeComponent ();
		    Car = device;
		}

	    private async void Button_OnPressed(object sender, EventArgs e)
	    {
	        Button pressed = (Button) sender;
	        await ViewModel.SendCommand(Car, pressed.Text);
	    }

	    private async void Stepper_OnValueChanged(object sender, ValueChangedEventArgs e)
	    {
	        ViewModel.Angle = (int) e.NewValue;
	        await ViewModel.SendCommand(Car, $"S {ViewModel.Angle}");
	    }
	}
}