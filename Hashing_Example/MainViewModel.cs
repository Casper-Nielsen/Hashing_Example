using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Hashing_Example
{
    class MainViewModel : INotifyPropertyChanged
    {
        private byte[] hashed;
        private Hashing hashing;
        private string key;
        private string message;
        private string mac;
        private ObservableCollection<HashingType> hashingOptions = new ObservableCollection<HashingType>();
        private HashingType selectedHashing;

        public HashingType SelectedHashing
        {
            get { return selectedHashing; }
            set { selectedHashing = value; }
        }

        public ObservableCollection<HashingType> HashingOptions
        {
            get { return hashingOptions; }
            set { hashingOptions = value; }
        }
        public string Key
        {
            get { return key; }
            set 
            { 
                key = value;
                OnPropertyChanged("Key");
            }
        }
        public string Message
        {
            get { return message; }
            set 
            { 
                message = value;
                OnPropertyChanged("Message");
            }
        }
        public string MAC
        {
            get { return mac; }
            set 
            { 
                mac = value;
                OnPropertyChanged("MAC");
            }
        }

        public MainViewModel()
        {
            hashing = new Hashing();
            hashing.SetHashingType("SHA1");
        }
        public void InputChanges()
        {
            hashing.SetHashingType(selectedHashing.Name);
            hashed = hashing.ComputeMAC(Encoding.ASCII.GetBytes(message), Encoding.ASCII.GetBytes(key));
        }
        public void ChangedOutput()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
