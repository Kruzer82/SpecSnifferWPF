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
    public class SpecTests
    {
        [TestMethod()]
        public void SetModelValueTest()
        {
            SpecLog specLenovo = new SpecLog();
            SpecLog specDell = new SpecLog();
            SpecLog specHp = new SpecLog();
            SpecLog specFujitsu = new SpecLog();
            SpecLog specOther = new SpecLog();


            specLenovo.Manufacturer = "LENOVO";
            specLenovo.ModelName = "ThinkPad W540  ";
            specDell.Manufacturer = "DELL INC.";
            specDell.ModelName = "OptiPlex7010 Tower ";
            specHp.Manufacturer = "HEWLETT-PACKARD";
            specHp.ModelName = "HP COMPAQ   8300 Pro";
            specFujitsu.Manufacturer = "FUJITSU";
            specFujitsu.ModelName = "LIFEBOOK S710 ";
            specOther.Manufacturer = "";
            specOther.ModelName = "Alienware G25";

            string lenovoModel = "W540";
            string dellModel = "7010 TWR";
            string hpModel = "8300";
            string fujitsuModel = "S710";
            string otherModel = "Alienware G25";


            Assert.AreEqual(lenovoModel, specLenovo.ModelName);
            Assert.AreEqual(dellModel, specDell.ModelName);
            Assert.AreEqual(hpModel, specHp.ModelName);
            Assert.AreEqual(fujitsuModel, specFujitsu.ModelName);
            Assert.AreEqual(otherModel, specOther.ModelName);
        }

        [TestMethod()]
        public void SetDeviceTypeValueTest()
        {
            SpecLog singleType = new SpecLog();
            SpecLog doubleType = new SpecLog();
            SpecLog multiType = new SpecLog();

            singleType.DeviceType = "9";
            doubleType.DeviceType = "1/10";
            multiType.DeviceType = "1/10/24/2/13";

            string singleTypeExpected = "Laptop";
            string doubleTypeExpected = "Other/Notebook";
            string multiTypeExpected = "Other/Notebook/Sealed-Case PC/Unknown/All in One";

            Assert.AreEqual(singleTypeExpected, singleType.DeviceType);
            Assert.AreEqual(doubleTypeExpected, doubleType.DeviceType);
            Assert.AreEqual(multiTypeExpected, multiType.DeviceType);

        }
    }
}