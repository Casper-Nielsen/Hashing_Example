using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Hashing_Example
{
    class MainViewModel : INotifyPropertyChanged
    {
        private byte[] hashed;

        private string key = "";
        private string message = "";
        private string mac = "";
        private string vMac = "";

        private ObservableCollection<HashingType> hashingOptions;

        private HashingType selectedHashing;

        private bool ascii = true;
        private bool base64;
        private bool vAscii = true;
        private bool vBase64;
        private bool valid;

        private long time;

        private ICommand outputChangedCommand;
        private ICommand validMACChangedCommand;

        private IHashing hashing;

        public long Time
        {
            get { return time; }
            set { time = value;
                OnPropertyChanged("Time");
            }
        }
        
        public bool Valid
        {
            get { return valid; }
            set
            {
                valid = value;
                OnPropertyChanged("Valid");
            }
        }
        public bool VBase64
        {
            get { return vBase64; }
            set
            {
                vBase64 = value;
            }
        }
        public bool VASCII
        {
            get { return vAscii; }
            set { vAscii = value; }
        }
        public bool Base64
        {
            get { return base64; }
            set { base64 = value;
            }
        }
        public bool ASCII
        {
            get { return ascii; }
            set { ascii = value; }
        }
        
        public string Key
        {
            get { return key; }
            set
            {
                key = value;
                OnPropertyChanged("Key");
                InputChanges();
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
                InputChanges();
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
        public string VMAC
        {
            get { return vMac; }
            set
            {
                vMac = value;
                OnPropertyChanged("VMAC");
                Validate();
            }
        }

        public ICommand ValidMACChangedCommand
        {
            get { return validMACChangedCommand; }
            set { validMACChangedCommand = value; }
        }
        public ICommand OutputChangedCommand
        {
            get { return outputChangedCommand; }
            set { outputChangedCommand = value; }
        }
        
        public HashingType SelectedHashing
        {
            get { return selectedHashing; }
            set 
            { 
                selectedHashing = value;
                InputChanges();
            }
        }
        
        public ObservableCollection<HashingType> HashingOptions
        {
            get { return hashingOptions; }
            set { hashingOptions = value; }
        }
       
        public MainViewModel()
        {
            OutputChangedCommand = new RelayCommand(new Action<object>(UpdateOutput));
            ValidMACChangedCommand = new RelayCommand(new Action<object>(Validate));
            // Fills the option with diffent types of hashing
            hashingOptions = new ObservableCollection<HashingType>() {
                new HashingType() { Managed=false, Type = "SHA1", Name = "SHA1" },
                new HashingType() { Managed=false, Type = "SHA256", Name = "SHA256" },
                new HashingType() { Managed=false, Type = "SHA384", Name = "SHA384" },
                new HashingType() { Managed=false, Type = "SHA512", Name = "SHA512" },
                new HashingType() { Managed=false, Type = "MD5", Name = "MD5" },
                new HashingType() { Managed=true, Type = "SHA1", Name = "SHA1 Managed" },
                new HashingType() { Managed=true, Type = "SHA256", Name = "SHA256 Managed" },
                new HashingType() { Managed=true, Type = "SHA384", Name = "SHA384 Managed" },
                new HashingType() { Managed=true, Type = "SHA512", Name = "SHA512 Managed" },
            };
            selectedHashing = hashingOptions[0];
            hashing = new HmacHashing(selectedHashing.Type);
        }

        /// <summary>
        /// hashes the users input
        /// </summary>
        public void InputChanges(object obj = null)
        {
            // Creates the Hashing object if it is managed or not 
            if (selectedHashing.Managed)
            {
                hashing = new Hashing();
            }
            else
            {
                hashing = new HmacHashing();
            }
            // Sets the hashingtype 
            hashing.SetHashingType(selectedHashing.Name);
            // Converts the message to bytes using ascii
            byte[] message = Encoding.ASCII.GetBytes(this.message);
            // Hashes the message
            Time = hashing.ComputeMAC(ref message, Encoding.ASCII.GetBytes(key));
            hashed = message;
            // Updates the output
            UpdateOutput(obj);
            // Validates the generatede mac with the inputed mac
            Validate(obj);
        }
        /// <summary>
        /// Validates user inputs with the inputed MAC
        /// </summary>
        public void Validate(object obj = null)
        {
            try
            {
                if (VMAC != "")
                {
                    byte[] userMAC = new byte[0];
                    if (vAscii)
                    {
                        // Converts ascii to byte array
                        userMAC = Encoding.ASCII.GetBytes(VMAC);
                    }
                    else if (vBase64)
                    {
                        // Converts base64 to byte array
                        userMAC = Convert.FromBase64String(VMAC);
                    }
                    else
                    {
                        // Converts hex string to byte array
                        string[] hexValuesSplit = VMAC.Split('-');
                        userMAC = new byte[hexValuesSplit.Length];
                        for (int i = 0; i < hexValuesSplit.Length; i++)
                        {
                            userMAC[i] = Convert.ToByte(hexValuesSplit[i], 16);

                        }
                    }
                    // Validates the inputed mac with the generated mac
                    Valid = hashing.Validate(hashed, userMAC);
                }
            }
            catch 
            { 
                Valid = false; 
            }
        }

        /// <summary>
        /// Updates the output to the given string type
        /// </summary>
        public void UpdateOutput(object obj)
        {
            try
            {
                if (ASCII)
                {
                    // Converts byte array to ascii string
                    MAC = Encoding.ASCII.GetString(hashed);
                }
                else if (Base64)
                {
                    // Converts byte array to Base64 string
                    MAC = Convert.ToBase64String(hashed);
                }
                else
                {
                    // Converts byte array to hex string
                    MAC = BitConverter.ToString(hashed);
                }
            }
            catch { }
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
