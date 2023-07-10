using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Models;

namespace Teledoc.Domain.Interfaces
{
    public interface IHasIncorporators
    {
        IEnumerable<Incorporator> GetIncorporators();
    }
}
