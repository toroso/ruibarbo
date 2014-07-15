using System;
using System.Collections.Generic;
using System.Reflection;

namespace ruibarbo.core
{
    public interface IApplication
    {
        Assembly MainAssembly { get; }
        IEnumerable<Uri> Resources { get; }
        void Start();
    }
}