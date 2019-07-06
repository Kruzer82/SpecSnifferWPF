using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSnifferWPF
{
    public class ForTestOnly
    {
        public string Gpu { get; set; }
        public string Storage { get; set; }
        public string Name { get; set; }


        public ForTestOnly()
        {
            Gpu = $"Intel(R) HD Graphics 530\nNVIDIA Quadro M1000M";
            Storage = "512SSD\n240SSD";
            Name = "INTEL SSDPEKKF256G7H\nINTEL SSDPEKKF256G7H";
        }
    }
}
