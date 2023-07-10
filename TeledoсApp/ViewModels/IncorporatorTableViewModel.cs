using Teledoc.Domain.Models;

namespace TeledocApp.ViewModels
{
    public class IncorporatorTableViewModel
    {
        public IEnumerable<Incorporator> Incorporators { get; set; }

        public IncorporatorTableViewModel(IEnumerable<Incorporator> incorporators)
        {
            Incorporators = incorporators;
        }
    }
}
