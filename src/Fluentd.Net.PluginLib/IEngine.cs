namespace Fluentd.Net
{

    public interface IEngine
    {
        void Emit(string tag, UnixTime time, string jsonRecord);

        void EmitArray(string tag, params EventRecord[] records);

        void EmitStream(string tag, IEventStream stream);
    }

}