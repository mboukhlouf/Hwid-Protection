using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Get_Hwid.Annotations;

namespace Get_Hwid.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string hwid;

        public string Hwid
        {
            get => hwid;
            set
            {
                hwid = value;
                OnPropertyChanged(nameof(Hwid));
            }
        }

        public MainViewModel()
        {
            Hwid = HwidProtection.HwidHelper.GetHwid();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
