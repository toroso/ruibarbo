using System.Collections.Generic;

namespace tungsten.core.Search
{
    internal interface IAmFoundByUpdatable
    {
        void FoundBy(IEnumerable<By> bys);
    }
}