﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace SpecSniffer.Model
{
    public class SpecReader
    {
        public  SpecLog CurrentSpec { get; set; } = new SpecLog();

        public SpecReader()
        {
            GetDeviceType();
            GetManufacturerModelSerial();
            CurrentSpec.MotherboardSerial = GetFromWmi("root\\CIMV2", "Win32_BaseBoard", "SerialNumber");
            CurrentSpec.Cpu= GetFromWmi("root\\CIMV2", "Win32_Processor", "Name");
            GetRam();
            CurrentSpec.Optical = GetFromWmi("root\\CIMV2", "Win32_CDROMDrive", "MediaType");
            GetDisk();
            GetDiagonal();
            GetResolution();
            CurrentSpec.Gpu = GetFromWmi("root\\CIMV2", "Win32_VideoController", "Caption");
            GetOS();
            CurrentSpec.OsKey = GetFromWmi("root\\CIMV2", "SoftwareLicensingService", "OA3xOriginalProductKey");
            GetDeviceManagerItems();
        }



        private void GetDeviceType()
        {
            try
            {
                foreach (var query in new ManagementObjectSearcher("root\\CIMV2",
                                 $"SELECT ChassisTypes " +
                                 $"FROM Win32_SystemEnclosure").Get())
                {
                    UInt16[] chassis = ((UInt16[])query["ChassisTypes"]);
                    CurrentSpec.DeviceType = string.Join("/", chassis);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GetManufacturerModelSerial()
        {
            CurrentSpec.Manufacturer = GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Manufacturer");
            string serial= GetFromWmi("root\\CIMV2", "Win32_SystemEnclosure", "SerialNumber");

            if (CurrentSpec.Manufacturer== "LENOVO")
            {
                CurrentSpec.ModelName= GetFromWmi("root\\CIMV2", "Win32_ComputerSystemProduct", "Version");

                //format serial
                string pn = GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Model").ToLower();
                CurrentSpec.ModelSerial = $"S1{pn}{serial}";
            }
            else
            {
                CurrentSpec.ModelName = GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Model");
                CurrentSpec.ModelSerial = serial;
            }
        }

        private void GetRam()
        {
            List<object> ramSizeObj = new List<object>();
            List<string> ramPartNumber = new List<string>();
            List<string> ramSerialNumber = new List<string>();
            try
            {
                //get ram as object list
                foreach (var queryObj in new ManagementObjectSearcher("root\\CIMV2",
                   $"SELECT Capacity,PartNumber,SerialNumber " +
                   $"FROM Win32_PhysicalMemory").Get())
                {
                    ramSizeObj.Add(queryObj["Capacity"]);
                    ramPartNumber.Add(queryObj["PartNumber"].ToString().Trim());
                    ramSerialNumber.Add(queryObj["SerialNumber"].ToString().Trim());
                }
                //converts to intlist
                var ramSizeInt = ramSizeObj
                .OfType<ulong>()
                .Select(s => s / (1024 * 1024 * 1024))
                .Select(Convert.ToInt32)
                .ToList();

                //return full RAM and RAM per bank
                CurrentSpec.RamSize= string.Format($"{ramSizeInt.Sum().ToString()}GB ({string.Join("+", ramSizeInt)})");
                CurrentSpec.RamPartNumber = string.Join(Environment.NewLine, ramPartNumber).Trim();
                CurrentSpec.RamSerial = string.Join(Environment.NewLine, ramSerialNumber);
            }
            catch (Exception)
            {
                //ignore
            }
        }

        /// <summary>
        /// This method cant check disk type(HDD or SSD) but can better filter external drives.
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
                throw;
            }

            CurrentSpec.DiskSize = string.Join(Environment.NewLine, hddSize);
            CurrentSpec.DiskName = string.Join(Environment.NewLine, hddModel);
            CurrentSpec.DiskSerial = string.Join(Environment.NewLine, hddSerial);
        }

        private void GetDiagonal()
        {
            double verticalSize = 0;
            double horizontalSize = 0;
            List<double> diagonalMap = new List<double>()
            {10.1,11.6,12,12.5,14,15.6,17.3,18,19,20,20.1,21,21.3,22,22.2,23,24,26,27};


            try
            {
                ManagementObjectSearcher searcher =
                   new ManagementObjectSearcher("root\\WMI",
                   "SELECT MaxHorizontalImageSize, MaxVerticalImageSize  FROM WmiMonitorBasicDisplayParams");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["MaxHorizontalImageSize"] != null)
                    {
                        verticalSize = Convert.ToDouble(queryObj["MaxVerticalImageSize"]) / 2.54;
                        horizontalSize = Convert.ToDouble(queryObj["MaxHorizontalImageSize"]) / 2.54;
                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;

            }

            var diagonal = Math.Sqrt(verticalSize * verticalSize + horizontalSize * horizontalSize);
            var roundedDiagonal = diagonalMap.Select(n => new { n, distance = Math.Abs(n - diagonal) })
            .OrderBy(p => p.distance)
            .First()
            .n;

            CurrentSpec.Diagonal= $@"{roundedDiagonal.ToString()}""";
        }

        private void GetResolution()
        {
            var resolution = "";
        Dictionary<string, string> _resolutionMap = new Dictionary<string, string>()
        {
            {"1280x1024", "SXGA"},
            {"1360x768", "HD"},
            {"1366x768", "HD"},
            {"1600x900", "HD+"},
            {"1920x1080", "FHD"},
            {"1280x800", "WXGA"},
            {"1280x768", "WXGA"},
            {"1280x720", "WXGA"},
            {"1440x900", "WXGA"},
            {"1680x1050", "WSXGA"},
            {"1920x1200", "WUXGA"},
            {"1152x864", "XGA+"},
            {"1024x768", "XGA"},
            {"1024x600", "WSVGA"},
            {"800x600", "SVGA"},
            {"2560x1440", "WQHD"},
            {"3840x2160", "UHD"},
            {"4096x2160", "UHD"},
            {"2560×1600", "WQXGA"}
        };

            try
            {
                ManagementObjectSearcher searcher =
                   new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT CurrentHorizontalResolution, CurrentVerticalResolution FROM Win32_VideoController");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["CurrentHorizontalResolution"] != null)
                    {
                        resolution = queryObj[$"CurrentHorizontalResolution"] + "x" + queryObj[$"CurrentVerticalResolution"];
                        break;
                    }
                    break;

                }

            }
            catch (Exception)
            {
                throw;

            }

            foreach (var resName in _resolutionMap)
                if (resName.Key == resolution)
                {
                    resolution = resName.Value;
                    break;
                }

            CurrentSpec.Resolution = resolution;
        }

        public void GetOS()
        {
            try
            {
                foreach (var query in new ManagementObjectSearcher("root\\CIMV2",
                                  $"SELECT Caption,BuildNumber,MUILanguages " +
                                  $"FROM Win32_OperatingSystem").Get())
                {
                    CurrentSpec.InstalledOS = query["Caption"].ToString();
                    CurrentSpec.VerOS = query["BuildNumber"].ToString();
                    string[] langs = ((string[])query["MUILanguages"]).Select(s => s = s.Remove(0, 3)).ToArray();
                    CurrentSpec.LangOS = string.Join(" ", langs);
                }
            }
            catch (Exception)
            {
                throw;

            }
        }

        public void GetDeviceManagerItems()
        {
            List<string> DeviceList = new List<string>();
            try
            {
                foreach (var queryObj in new ManagementObjectSearcher("root\\CIMV2",
                    $"SELECT Status,Caption " +
                    $"FROM Win32_PnPEntity").Get())
                {
                    DeviceList.Add($"{(string)queryObj["Caption"]} [{(string)queryObj["Status"]}]");
                }


            }
            catch (Exception)
            {
                throw;
            }
            CurrentSpec.DeviceManager = string.Join("/", DeviceList);
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
                    propertyValue.Add(queryObj[scopeProperty].ToString().Trim());
                }
            }
            catch (Exception)
            {
                throw;

            }

            return string.Join(Environment.NewLine, propertyValue);
        }
    }
}
