using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.Infrastructure.Accounts.RavenDB;
using EventStore.ClientAPI;
using Raven.Client.Documents;
using Raven.Embedded;
using Tactical.DDD;
using Tactical.DDD.EventSourcing;
using Xunit;
using BoostRoom.Integration.Tests;
using Moq;
using Raven.Client.ServerWide;

namespace BoostRoom.Integration.Tests.AccountsTests.ProjectionTests
{
    public class UniqueAccountsProjectionTests : IntegrationAbstractTest 
    {
        private readonly UniqueAccountsProjection _projection;

        public UniqueAccountsProjectionTests(IntegrationFixture fixture) : base(fixture)
        {
            var eventStore = new Mock<BoostRoom.Infrastructure.IEventStore>();

            _projection = new UniqueAccountsProjection(DocumentStore, eventStore.Object);
        }

        [Fact]
        public void Projection_Applies_ClientRegistered_Event()
        {
            _projection.On(new ClientRegistered(
                "foo",
                "user123",
                "john@mail.com",
                "pass12345",
                "John",
                "Doe",
                "Address 123",
                "London",
                "1234",
                "UK",
                true));

            using (var session = DocumentStore.OpenSession())
            {
                var accounts = session.Query<UniqueAccountsProjection.AccountEntry>()
                    .ToList();

                var account = accounts.FirstOrDefault();

                Assert.NotNull(account);

                Assert.Equal("john@mail.com", account.Email);
                Assert.Equal("user123", account.Username);
            }
        }

        [Fact]
        public void Projection_Applies_Multiple_ClientRegisteredEvents()
        {
            var events = new[]
            {
                new ClientRegistered(
                    "foo",
                    "user123",
                    "john@mail.com",
                    "pass12345",
                    "John",
                    "Doe",
                    "Address 123",
                    "London",
                    "1234",
                    "UK",
                    true
                ),
                new ClientRegistered(
                    "foo",
                    "user345",
                    "john@mail2.com",
                    "pass12345",
                    "John",
                    "Doe",
                    "Address 123",
                    "London",
                    "1234",
                    "UK",
                    true
                )
            };

            foreach (var e in events)
            {
                _projection.On(e);
            }

            using (var session = DocumentStore.OpenSession())
            {
                var accounts = session.Query<UniqueAccountsProjection.AccountEntry>()
                    .ToList();

                Assert.Equal(2, accounts.Count);
            }
        }
    }
}