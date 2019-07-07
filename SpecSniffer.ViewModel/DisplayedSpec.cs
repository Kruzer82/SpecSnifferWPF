using SpecSniffer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSniffer.ViewModel
{
    public class DisplayedSpec
    {
        public Spec ViewSpec { get; set; }


        public DisplayedSpec()
        {
            SpecReader specReader = new SpecReader();
            ViewSpec = specReader.CurrentSpec;
        }
    }
}
