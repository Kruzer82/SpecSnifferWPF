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
            GetManufacturerModelSerial();

            CurrentSpec.Processor= GetFromWmi("root\\CIMV2", "Win32_Processor", "Name");
            GetRam();
            CurrentSpec.Optical = GetFromWmi("root\\CIMV2", "Win32_CDROMDrive", "MediaType");
            GetDisk();
        }





        private void GetManufacturerModelSerial()
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

        private void GetRam()
        {
            List<object> ramList = new List<object>();
            try
            {
                //get ram as object list
                foreach (var queryObj in new ManagementObjectSearcher("root\\CIMV2",
                   $"SELECT Capacity " +
                   $"FROM Win32_PhysicalMemory").Get())
                {
                    ramList.Add(queryObj["Capacity"]);
                }
                //converts to intlist
                var ramInt = ramList
                .OfType<ulong>()
                .Select(s => s / (1024 * 1024 * 1024))
                .Select(Convert.ToInt32)
                .ToList();

                //return full RAM and RAM per bank
                CurrentSpec.Ram= string.Format($"{ramInt.Sum().ToString()}GB ({string.Join("+", ramInt)})");
            }
            catch (Exception)
            {
                //ignore
            }
        }

        /// <summary>
        /// Method cant check disk type(HDD or SSD) but can better filter external drives.
        /// (due some of external USB adapters are not displaying as USB but as SCSI)
        /// </summary>
        //private void GetDisk()
        //{
        //    List<string> hddSize = new List<string>();
        //    List<string> hddModel = new List<string>();
        //    List<string> hddSerial = new List<string>();
        //    //gets size serial and model
        //    try
        //    {
        //        foreach (var query in new ManagementObjectSearcher("root\\CIMV2",
        //                          $"SELECT Size,Model,MediaType,SerialNumber " +
        //                          $"FROM Win32_DiskDrive").Get())
        //        {
        //            bool isInternal = query["MediaType"].ToString() == "Fixed hard disk media" ? true : 
        //                              query["MediaType"].ToString() == "Format is unknown" ? true : false;

        //            if (isInternal)
        //            {
        //                hddSize.Add(((UInt64)query["Size"] / (1000 * 1000 * 1000)).ToString());
        //                hddModel.Add((string)query["Model"]);
        //                hddSerial.Add((string)query["SerialNumber"]);
        //            }
        //        }


        //    }
        //    catch (Exception)
        //    {

        //    }

        //    CurrentSpec.DiskSize = string.Join("/", hddModel);
        //    CurrentSpec.DiskName = string.Join("/", hddModel);
        //    CurrentSpec.DiskName = string.Join("/", hddModel);
        //}

        
        
        private void GetDisk()
        {
            List<string> hddSize = new List<string>();
            List<string> hddModel = new List<string>();
            List<string> hddSerial = new List<string>();
            try
            {
                foreach (var query in new ManagementObjectSearcher("root\\Microsoft\\Windows\\Storage",
                                  $"SELECT BusType,Size,MediaType,Model,SerialNumber " +
                                  $"FROM MSFT_PhysicalDisk").Get())
                {
                    bool isNoUSB = (ushort)query["BusType"] == 7 ? false : true;

                    if (isNoUSB)
                    {
                        string size = ((UInt64)query["Size"] / (1000 * 1000 * 1000)).ToString();
                        string type = (ushort)query["MediaType"] == 4 ? "SSD" : (ushort)query["MediaType"] == 3 ? "HDD" : "GB";

                        hddSize.Add($"{size}{type}");
                        hddModel.Add((string)query["Model"]);
                        hddSerial.Add((string)query["SerialNumber"]);
                    }
                }


            }
            catch (Exception)
            {

            }

            CurrentSpec.DiskSize = string.Join("/", hddSize);
            CurrentSpec.DiskName = string.Join("/", hddModel);
            CurrentSpec.DiskSerial = string.Join("/", hddSerial);
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
