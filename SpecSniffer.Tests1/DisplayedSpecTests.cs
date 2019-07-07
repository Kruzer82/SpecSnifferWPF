using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecSniffer.Model;
using SpecSniffer.ViewModel;
using System;

namespace SpecSniffer.Tests
{
    [TestClass()]
    public class DisplayedSpecTests
    {
        [TestMethod()]
        public void ViewSpecValuesTest()
        {

            Spec expectedSpec;
            Spec actualSpec;
            DisplayedSpec displayedSpec = new DisplayedSpec();
            expectedSpec = new Spec()
            {
                Manufacturer = "LENOVO",
                Model = "P50",
                Serial = "S120en0005mbPC0C3D4N",
                Processor = "i7-6700HQ CPU @ 2.60GHz",
                Ram = "16GB (8+8)",
                Optical = "",
                DiskName = "INTEL SSDPEKKF256G7H",
                DiskSize = "256SSD",
                DiskSerial = "0000_0000_0100_0000_E4D2_5C1E_9BE4_4E01.",
                Diagonal = $@"23""",
                Resolution = "FHD",
                Gpu = $"NVIDIA Quadro M1000M{Environment.NewLine}Intel(R) HD Graphics 530",
                InstalledOS = "Win10 Pro",
                VerOS = "18362",
                LangOS = "PL",
                OsKey= "2NG2R-HRCYB-3J9YB-CG7MP-HH7CP"
            };
            actualSpec = displayedSpec.ViewSpec;


            Assert.AreEqual(expectedSpec.Manufacturer, actualSpec.Manufacturer, "Manufacturer value not equal.");
            Assert.AreEqual(expectedSpec.Model, actualSpec.Model, "Model value not equal.");
            Assert.AreEqual(expectedSpec.Serial, actualSpec.Serial, "Serial value not equal.");
            Assert.AreEqual(expectedSpec.Processor, actualSpec.Processor, "Processor value not equal.");
            Assert.AreEqual(expectedSpec.Ram, actualSpec.Ram, "Ram value not equal.");
            Assert.AreEqual(expectedSpec.Optical, actualSpec.Optical, "Optical value not equal.");

            Assert.AreEqual(expectedSpec.DiskName, actualSpec.DiskName, "DiskName value not equal.");
            Assert.AreEqual(expectedSpec.DiskSize, actualSpec.DiskSize, "DiskSize value not equal.");
            Assert.AreEqual(expectedSpec.DiskSerial, actualSpec.DiskSerial, "DiskSerial value not equal.");
            Assert.AreEqual(expectedSpec.Diagonal, actualSpec.Diagonal, "Diagonal value not equal.");
            Assert.AreEqual(expectedSpec.Resolution, actualSpec.Resolution, "Resolution value not equal.");
            Assert.AreEqual(expectedSpec.Gpu, actualSpec.Gpu, "GPU value not equal.");

            Assert.AreEqual(expectedSpec.InstalledOS, actualSpec.InstalledOS, "InstalledOS value not equal.");
            Assert.AreEqual(expectedSpec.VerOS, actualSpec.VerOS, "VerOS value not equal.");
            Assert.AreEqual(expectedSpec.LangOS, actualSpec.LangOS, "LangOS value not equal.");
            Assert.AreEqual(expectedSpec.OsKey, actualSpec.OsKey, "OsKey value not equal.");
        }
    }
}