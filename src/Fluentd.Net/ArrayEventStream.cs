using System.Collections.Generic;
using System.Linq;

namespace Fluentd.Net
{
    public class ArrayEventStream : IEventStream
    {
        private readonly EventRecord[] eventRecords;

        public ArrayEventStream(params EventRecord[] eventRecords)
        {
            this.eventRecords = eventRecords;
        }

        public IEnumerator<EventRecord> GetEnumerator()
        {
            return eventRecords.Cast<EventRecord>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return eventRecords.GetEnumerator();
        }
    }
}
