using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SpecSniffer.Model
{
    public class Spec
    {
        private string model;

        private string deviceType;
        private string processor;
        private string installedOS;


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
            }
        }

        public string Manufacturer { get; set; }

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

            }
        }

        public string Serial { get; set; }

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
            }
        }

        public string Ram { get; set; }

        public string Optical { get; set; }

        public string DiskSize { get; set; }

        public string DiskName { get; set; }

        public string DiskSerial { get; set; }

        public string Diagonal { get; set; }

        public string Resolution { get; set; }

        public string GPU { get; set; }

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
            }
        }

        public string VerOS { get; set; }

        public string LangOS { get; set; }


        public string SummarySpec
        {
            get
            {
                return $"{Model}  " +
                    $"{Processor.Remove(Processor.IndexOf("@")).Trim()}/" +
                    $"{Ram.Remove(Ram.IndexOf("(")).Trim()}/" +
                    $"{DiskSize.Replace("/", Environment.NewLine)}/" +
                    $"{Optical}/" +
                    $"{Diagonal}{Resolution}";
            }
        }

        public Spec()
        {

        }

    }
}
