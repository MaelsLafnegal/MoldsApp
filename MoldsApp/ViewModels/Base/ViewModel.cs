using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoldsApp.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string Propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propertyname));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string Propertyname = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(Propertyname);
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (Disposing || _Disposed) return;
            _Disposed = true;
            //Освобождение управляемых ресурсов
        }
    }
}
