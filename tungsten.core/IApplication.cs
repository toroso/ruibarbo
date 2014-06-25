using System;
using System.Collections.Generic;
using System.Reflection;

namespace tungsten.core
{
    public interface IApplication
    {
        Assembly MainAssembly { get; }
        IEnumerable<Uri> Resources { get; }
        void Start();
    }
}