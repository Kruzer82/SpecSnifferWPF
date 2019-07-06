using SpecSniffer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSniffer.ViewModel
{
    public class SpecViewModel
    {
        public Spec ViewModelSpec { get; set; }


        public SpecViewModel()
        {
            SpecReader specReader = new SpecReader();
            ViewModelSpec = specReader.CurrentSpec;
        }
    }
}
