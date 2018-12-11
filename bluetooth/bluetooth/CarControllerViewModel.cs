using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Bluetooth.Abstractions;

namespace bluetooth
{
    public class CarControllerViewModel : BaseViewModel
    {
        private int _angle;
        public Dictionary<string, byte[]> Commands = new Dictionary<string, byte[]>()
        {
            {"Forward", Encoding.ASCII.GetBytes("1")},
            {"Backward", Encoding.ASCII.GetBytes("4")},
            {"Stop", Encoding.ASCII.GetBytes("0")},
            {"Right", Encoding.ASCII.GetBytes("2")},
            {"Left", Encoding.ASCII.GetBytes("3")},
            {"Open Salter", Encoding.ASCII.GetBytes("5")},
            {"Close Salter", Encoding.ASCII.GetBytes("6")}
        };
        public int Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                OnPropertyChanged(nameof(Angle));
            }
        }

        public CarControllerViewModel()
        {
        }

        public async Task SendCommand(IBluetoothDevice car, string text)
        {
            Commands.TryGetValue(text, out var cmd);
            if (cmd != null)
                await car.Write(cmd);
            else
                await car.Write(Encoding.ASCII.GetBytes(text));
        }
    }
}
