using System;
using Tactical.DDD;

namespace BoostRoom.Integration.Tests.Infrastructure
{
    public class TestId : EntityId
    {
        private readonly Guid _id;

        public TestId()
        {
            _id = Guid.NewGuid();
        }

        public override string ToString() =>
            _id.ToString();
    }
}