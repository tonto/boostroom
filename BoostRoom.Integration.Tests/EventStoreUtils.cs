using System.Threading;

namespace BoostRoom.Integration.Tests
{
    public static class EventStoreUtils
    {
        private static int _port = 1113;

        public static int NextTestPort => Interlocked.Increment(ref _port);
    }
}