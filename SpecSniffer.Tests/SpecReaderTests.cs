using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecSniffer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSniffer.Tests
{
    [TestClass()]
    public class SpecReaderTests
    {
        [TestMethod()]
        public void CurrentSpecValuesTest()
        {
            SpecReader specReader = new SpecReader();
            Spec expectedSpec;
            Spec actualSpec;

            expectedSpec = new Spec() {
                Manufacturer = "LENOVO",
                Model = "ThinkPad P50",
                Serial= "S120en0005mbPC0C3D4N",
                Processor= "Intel(R) Core(TM) i7-6700HQ CPU @ 2.60GHz",
                Ram="16GB (8+8)",
                Optical="",
                DiskName= "INTEL SSDPEKKF256G7H",
                DiskSize="256SSD",
                DiskSerial= "0000_0000_0100_0000_E4D2_5C1E_9BE4_4E01."
            };
            actualSpec = specReader.CurrentSpec;

            Assert.AreEqual(expectedSpec.Manufacturer, actualSpec.Manufacturer, "Manufacturer value not equal.");
            Assert.AreEqual(expectedSpec.Model, actualSpec.Model,"Model value not equal.");
            Assert.AreEqual(expectedSpec.Serial, actualSpec.Serial, "Serial value not equal.");
            Assert.AreEqual(expectedSpec.Processor, actualSpec.Processor, "Processor value not equal.");
            Assert.AreEqual(expectedSpec.Ram, actualSpec.Ram, "Ram value not equal.");
            Assert.AreEqual(expectedSpec.Optical, actualSpec.Optical, "Optical value not equal.");

            Assert.AreEqual(expectedSpec.DiskName, actualSpec.DiskName, "DiskName value not equal.");
            Assert.AreEqual(expectedSpec.DiskSize, actualSpec.DiskSize, "DiskSize value not equal.");
            Assert.AreEqual(expectedSpec.DiskSerial, actualSpec.DiskSerial, "DiskSerial value not equal.");
        }



    }
}