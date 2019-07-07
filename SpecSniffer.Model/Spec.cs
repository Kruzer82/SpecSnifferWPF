using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace SpecSniffer.Model
{
    public class Spec : INotifyPropertyChanged
    {
        private string model;

        private string deviceType;
        private string processor;
        private string installedOS;
        private string manufacturer;
        private string serial;
        private string ram;
        private string optical;
        private string diskSize;
        private string diskName;
        private string diskSerial;
        private string diagonal;
        private string resolution;
        private string gpu;
        private string verOS;
        private string langOS;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DeviceType
        {
            get { return deviceType; }
            set
            {

                var deviceTypeSB = new StringBuilder(value);

                deviceTypeSB.Replace("24", "Sealed-Case PC");
                deviceTypeSB.Replace("23", "Rack Mount Chassis");
                deviceTypeSB.Replace("22", "Storage Chassis");
                deviceTypeSB.Replace("21", "Peripheral Chassis");
                deviceTypeSB.Replace("20", "Bus Expansion Chassis");
                deviceTypeSB.Replace("19", "SubChassis");
                deviceTypeSB.Replace("18", "Expansion Chassis");
                deviceTypeSB.Replace("17", "Main System Chassis");
                deviceTypeSB.Replace("16", "Lunch Box");
                deviceTypeSB.Replace("15", "Space-Saving");
                deviceTypeSB.Replace("14", "Sub Notebook");
                deviceTypeSB.Replace("13", "All in One");
                deviceTypeSB.Replace("12", "Docking Station");
                deviceTypeSB.Replace("11", "Hand Held");
                deviceTypeSB.Replace("10", "Notebook");
                deviceTypeSB.Replace("9", "Laptop");
                deviceTypeSB.Replace("8", "Portable");
                deviceTypeSB.Replace("7", "Tower");
                deviceTypeSB.Replace("6", "Mini Tower");
                deviceTypeSB.Replace("5", "Pizza Box");
                deviceTypeSB.Replace("4", "Low Profile Desktop");
                deviceTypeSB.Replace("3", "Desktop");
                deviceTypeSB.Replace("2", "Unknown");
                deviceTypeSB.Replace("1", "Other");

                deviceType = deviceTypeSB.ToString();

                RaisePropertyChanged("DeviceType");
            }
        }

        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                manufacturer = value;
                RaisePropertyChanged("Manufacturer");
            }
        }

        public string Model
        {
            get
            {
                return model;
            }

            set
            {
                var modelSB = new StringBuilder(value);


                if (Manufacturer.Contains("LENOVO"))
                {
                    modelSB.Replace("ThinkPad", "");
                }
                else if (Manufacturer.Contains("DELL"))
                {
                    modelSB.Replace("Workstation", "");
                    modelSB.Replace("Precision", "");
                    modelSB.Replace("Latitude", "");
                    modelSB.Replace("OptiPlex", "");
                    modelSB.Replace("non-vPro", "");
                    modelSB.Replace("Inspiron", "");
                    modelSB.Replace("Vostro", "");
                    modelSB.Replace("AIO", "");
                    modelSB.Replace("Tower", "TWR");
                }
                else if (Manufacturer.Contains("HEWLETT-PACKARD"))
                {
                    modelSB.Replace("All-in-One", " AiO ");
                    modelSB.Replace("Workstation", "");
                    modelSB.Replace("EliteBook", "");
                    modelSB.Replace("Precision", "");
                    modelSB.Replace("EliteDesk", "");
                    modelSB.Replace("Notebook", "");
                    modelSB.Replace("ProBook", "");
                    modelSB.Replace("Spectre", "");
                    modelSB.Replace("Compaq", "");
                    modelSB.Replace("COMPAQ", "");
                    modelSB.Replace("Elite", "");
                    modelSB.Replace("Folio", "");
                    modelSB.Replace("Pro", "");
                    modelSB.Replace("HP", "");
                    modelSB.Replace("PC", "");
                }
                else if (Manufacturer.Contains("FUJITSU"))
                {
                    modelSB.Replace("LIFEBOOK", "");
                    modelSB.Replace("ESPRIMO", "");
                }

                //converts many spaces into one.
                model = Regex.Replace(modelSB.ToString(), @"\s+", " ").Trim();
                RaisePropertyChanged("Model");
            }
        }

        public string Serial
        {
            get
            {
                return serial;
            }
            set
            {
                serial = value;
                RaisePropertyChanged("Serial");
            }
        }

        public string Processor
        {
            get
            {
                return processor;
            }
            set
            {
                var cpuSB = new StringBuilder(value);

                cpuSB.Replace("Intel(R)", "");
                cpuSB.Replace("Core(TM)", "");
                cpuSB.Replace("CPU", "");

                processor = Regex.Replace(cpuSB.ToString(), @"\s+", " ").Trim();
                RaisePropertyChanged("Processor");
            }
        }

        public string Ram
        {
            get
            {
                return ram;
            }
            set
            {
                ram = value;
                RaisePropertyChanged("Ram");
            }
        }

        public string Optical
        {
            get
            {
                return optical;
            }
            set
            {
                optical = value;
                RaisePropertyChanged("Optical");
            }
        }

        public string DiskSize
        {
            get
            {
                return diskSize;
            }
            set
            {
                diskSize = value;
                RaisePropertyChanged("DiskSize");
            }
        }

        public string DiskName
        {
            get
            {
                return diskName;
            }
            set
            {
                diskName = value;
                RaisePropertyChanged("DiskName");
            }
        }

        public string DiskSerial
        {
            get
            {
                return diskSerial;
            }
            set
            {
                diskSerial = value;
                RaisePropertyChanged("DiskSerial");
            }
        }

        public string Diagonal
        {
            get
            {
                return diagonal;
            }
            set
            {
                diagonal = value;
                RaisePropertyChanged("Diagonal");
            }
        }

        public string Resolution
        {
            get
            {
                return resolution;
            }
            set
            {
                resolution = value;
                RaisePropertyChanged("Resolution");
            }
        }

        public string Gpu
        {
            get
            {
                return gpu;
            }
            set
            {
                gpu = value;
                RaisePropertyChanged("Gpu");
            }
        }

        public string InstalledOS
        {
            get
            {
                return installedOS;
            }
            set
            {
                var osNameSB = new StringBuilder(value);

                osNameSB.Replace("Microsoft", "");
                osNameSB.Replace("Windows 10", "Win10");

                installedOS = Regex.Replace(osNameSB.ToString(), @"\s+", " ").Trim();
                RaisePropertyChanged("InstalledOS");
            }
        }

        public string VerOS
        {
            get
            {
                return verOS;
            }
            set
            {
                verOS = value;
                RaisePropertyChanged("VerOS");
            }
        }

        public string LangOS
        {
            get
            {
                return langOS;
            }
            set
            {
                langOS = value;
                RaisePropertyChanged("LangOS");
            }
        }

        public string SummarySpec
        {
            get
            {
                return $"{Model}  " +
                    $"{Processor.Remove(Processor.IndexOf("@")).Trim()}/" +
                    $"{Ram.Remove(Ram.IndexOf("(")).Trim()}/" +
                    $"{DiskSize.Replace(Environment.NewLine, "/")}/" +
                    $"{Optical.Replace(Environment.NewLine, "/")}/" +
                    $"{Diagonal}{Resolution}";
            }
        }

        public string OsKey { get; set; }

        public Spec()
        {

        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
