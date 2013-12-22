using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fluentd.Net.Config;

namespace Fluentd.Net
{
    public class Engine : IEngine
    {
        public void Emit(string tag, UnixTime time, string jsonRecord)
        {
            throw new NotImplementedException();
        }

        public void EmitArray(string tag, params EventRecord[] records)
        {
            throw new NotImplementedException();
        }

        public void EmitStream(string tag, IEventStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
