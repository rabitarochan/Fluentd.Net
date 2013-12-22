using Fluentd.Net;

namespace Fluentd.Net
{
    public struct EventRecord
    {
        public UnixTime Time;
        public string Record;

        public EventRecord(UnixTime time, string jsonRecord)
        {
            Time = time;
            Record = jsonRecord;
        }
    }
}
