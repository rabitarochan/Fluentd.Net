using System;
using System.ComponentModel.Composition;

namespace Fluentd.Net
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class InputPluginAttribute : ExportAttribute, IPluginMetadata
    {
        public InputPluginAttribute(string name) : base(typeof(IInputPlugin))
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
