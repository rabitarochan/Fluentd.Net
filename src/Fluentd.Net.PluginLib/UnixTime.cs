using System;

namespace Fluentd.Net
{

    public struct UnixTime
    {
        public readonly long Value;

        public UnixTime(long value)
        {
            Value = value;
        }

        public UnixTime(DateTime time)
        {
            Value = GetUnixTime(time);
        }

        public static UnixTime Now()
        {
            return new UnixTime(DateTime.Now);
        }

        public DateTime ToDateTime(DateTimeKind kind = DateTimeKind.Local)
        {
            var time = EpochTime.AddSeconds(Value);
            return (kind == DateTimeKind.Local) ? time.ToLocalTime() : time;
        }

        public override bool Equals(object obj)
        {
            if (obj is long) return Value == (long) obj;
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return ToDateTime().ToString("yyyy-MM-dd HH:mm:ss zzz");
        }


        // private

        private static readonly DateTime EpochTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        private static long GetUnixTime(DateTime time)
        {
            var utc = time.ToUniversalTime();
            var elapsedTime = utc - EpochTime;
            return Convert.ToInt64(elapsedTime.TotalSeconds);
        }
    }

}
