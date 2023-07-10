using Teledok.Domain.Models;

namespace TeledokApp.ViewModels
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
