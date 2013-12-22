using System;
using System.ComponentModel.Composition;

namespace Fluentd.Net
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class OutputPluginAttribute : ExportAttribute, IPluginMetadata
    {
        public OutputPluginAttribute(string name) : base(typeof(IOutputPlugin))
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
