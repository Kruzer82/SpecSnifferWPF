using SpecSniffer.Model;

namespace SpecSniffer.ViewModel
{
    public class DisplayedSpec
    {
        public SpecLog ViewSpec { get; set; }


        public DisplayedSpec()
        {
            SpecReader specReader = new SpecReader();
            ViewSpec = specReader.CurrentSpec;
        }
    }
}
