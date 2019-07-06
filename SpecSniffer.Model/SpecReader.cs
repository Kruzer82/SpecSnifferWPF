using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SpecSniffer.Model
{
    public class SpecReader
    {
        public Spec CurrentSpec { get; set; } = new Spec();

        public SpecReader()
        {
            SetManufacturerModelSerial();


        }





        private void SetManufacturerModelSerial()
        {
            CurrentSpec.Manufacturer = GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Manufacturer");
            string serial= GetFromWmi("root\\CIMV2", "Win32_SystemEnclosure", "SerialNumber");

            if (CurrentSpec.Manufacturer== "LENOVO")
            {
                CurrentSpec.Model= GetFromWmi("root\\CIMV2", "Win32_ComputerSystemProduct", "Version");

                //format serial
                string pn = GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Model").ToLower();
                CurrentSpec.Serial = $"S1{pn}{serial}";
            }
            else
            {
                CurrentSpec.Model = GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Model");
                CurrentSpec.Serial = serial;
            }
        }


        private string GetFromWmi(string scopeNamespace, string scopeClass, string scopeProperty)
        {
            List<string> propertyValue = new List<string>();

            try
            {
                foreach (var queryObj in new ManagementObjectSearcher(scopeNamespace,
                    $"SELECT {scopeProperty} " +
                    $"FROM {scopeClass}").Get())
                {
                    propertyValue.Add(queryObj[scopeProperty].ToString());
                }
            }
            catch (Exception)
            {
                //ignored
            }

            return string.Join("/", propertyValue);
        }
    }
}
