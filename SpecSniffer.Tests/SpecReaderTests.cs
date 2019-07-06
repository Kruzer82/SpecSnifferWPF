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
                Serial= "S120en0005mbPC0C3D4N"
            };
            actualSpec = specReader.CurrentSpec;

            Assert.AreEqual(expectedSpec.Manufacturer, actualSpec.Manufacturer, "Manufacturer value not equal.");
            Assert.AreEqual(expectedSpec.Model, actualSpec.Model,"Model value not equal.");
            Assert.AreEqual(expectedSpec.Serial, actualSpec.Serial, "Serial value not equal.");
        }



    }
}