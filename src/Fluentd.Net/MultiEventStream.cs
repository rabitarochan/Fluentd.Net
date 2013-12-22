using System.Collections.Generic;

namespace Fluentd.Net
{
    public class MultiEventStream : IEventStream
    {
        private readonly IList<EventRecord> eventRecords;
 
        public MultiEventStream()
        {
            eventRecords = new List<EventRecord>();
        }

        public void Add(UnixTime time, string jsonRecord)
        {
            Add(new EventRecord(time, jsonRecord));
        }

        public void Add(EventRecord record)
        {
            eventRecords.Add(record);
        }

        public IEnumerator<EventRecord> GetEnumerator()
        {
            return eventRecords.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
