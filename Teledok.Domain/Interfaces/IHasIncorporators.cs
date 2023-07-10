using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledok.Domain.Models;

namespace Teledok.Domain.Interfaces
{
    public interface IHasIncorporators
    {
        IEnumerable<Incorporator> GetIncorporators();
    }
}
