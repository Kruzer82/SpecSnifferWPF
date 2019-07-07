using System.Text;
using System.Text.RegularExpressions;

namespace SpecSniffer.Model
{
    public class Spec
    {
        private string model;

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


                if(Manufacturer.Contains("LENOVO"))
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

        public string Processor { get; set; }

        public string Ram { get; set; }

        public string Optical { get; set; }

        public string DiskSize { get; set; }

        public string DiskName { get; set; }

        public string DiskSerial { get; set; }

        public string Diagonal { get; set; }

        public string Resolution { get; set; }

        public string GPU { get; set; }

        public string InstalledOS { get; set; }

        public string VerOS { get; set; }

        public string LangOS { get; set; }

        public Spec()
        {

        }

    }
}
