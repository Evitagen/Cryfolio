using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cryfolio.Models;
using Cryfolio.Services;

namespace Cryfolio.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public IDataStore<Portfolio> DataStore => App.Repository;
        public IDataStore<CoinsHodle> DataStore_CoinsHodle => (Cryfolio.Services.IDataStore<Cryfolio.Models.CoinsHodle>)App.CoinsHodle_Repository;

        public event PropertyChangedEventHandler PropertyChanged;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T storage, T value,
                                [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
