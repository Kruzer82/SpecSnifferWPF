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
            Spec specLenovo = new Spec();
            Spec specDell = new Spec();
            Spec specHp = new Spec();
            Spec specFujitsu = new Spec();
            Spec specOther = new Spec();


            specLenovo.Manufacturer = "LENOVO";
            specLenovo.Model = "ThinkPad W540  ";
            specDell.Manufacturer = "DELL INC.";
            specDell.Model = "OptiPlex7010 Tower ";
            specHp.Manufacturer = "HEWLETT-PACKARD";
            specHp.Model = "HP COMPAQ   8300 Pro";
            specFujitsu.Manufacturer = "FUJITSU";
            specFujitsu.Model = "LIFEBOOK S710 ";
            specOther.Manufacturer = "";
            specOther.Model = "Alienware G25";

            string lenovoModel = "W540";
            string dellModel = "7010 TWR";
            string hpModel = "8300";
            string fujitsuModel = "S710";
            string otherModel = "Alienware G25";


            Assert.AreEqual(lenovoModel, specLenovo.Model);
            Assert.AreEqual(dellModel, specDell.Model);
            Assert.AreEqual(hpModel, specHp.Model);
            Assert.AreEqual(fujitsuModel, specFujitsu.Model);
            Assert.AreEqual(otherModel, specOther.Model);
        }
    }
}