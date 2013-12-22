using System.Collections.Generic;
using System.Linq;

namespace Fluentd.Net
{
    public class OneEventStream : IEventStream
    {
        private readonly EventRecord eventRecord;

        public OneEventStream(UnixTime time, string record)
        {
            eventRecord = new EventRecord(time, record);
        }

        public IEnumerator<EventRecord> GetEnumerator()
        {
            return Enumerable.Repeat(eventRecord, 1).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
