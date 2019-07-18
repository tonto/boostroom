using System;
using System.Diagnostics;
using EventStore.ClientAPI;
using System.Collections.Generic;
using EventStore.ClientAPI.SystemData;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Threading.Tasks;
using EventStore.ClientAPI.Exceptions;
using Xunit;

namespace BoostRoom.Integration.Tests
{
    // TODO - Make this abstract class instead of Fixture
    public class EmbeddedEventStoreFixture : IAsyncLifetime
    {
        public IEventStoreConnection Connection { get; private set; }

        private string EventStoreContainer { get; }

        private const string EventStoreImage = "eventstore/eventstore";

        private DockerClient _client;

        public EmbeddedEventStoreFixture()
        {
            EventStoreContainer = "es" + Guid.NewGuid().ToString("N");
        }

        public async Task InitializeAsync()
        {
            var address = Environment.OSVersion.Platform == PlatformID.Unix
                ? new Uri("unix:///var/run/docker.sock")
                : new Uri("npipe://./pipe/docker_engine");

            var config = new DockerClientConfiguration(address);

            _client = config.CreateClient();

            var images =
                await _client.Images.ListImagesAsync(new ImagesListParameters {MatchName = EventStoreImage});

            if (images.Count == 0)
            {
                Console.WriteLine("[docker] no image found - pulling latest");

                await _client.Images.CreateImageAsync(
                    new ImagesCreateParameters {FromImage = EventStoreImage, Tag = "latest"}, null,
                    IgnoreProgress.Forever);
            }

            Console.WriteLine("[docker] creating container " + EventStoreContainer);

            var port = EventStoreUtils.NextTestPort;
            
            await _client.Containers.CreateContainerAsync(
                new CreateContainerParameters
                {
                    Image = EventStoreImage,
                    Name = EventStoreContainer,
                    Tty = true,
                    HostConfig = new HostConfig
                    {
                        PortBindings = new Dictionary<string, IList<PortBinding>>
                        {
                            {
                                "1113/tcp",
                                new List<PortBinding>
                                {
                                    new PortBinding
                                    {
                                        HostPort = $"{port}"
                                    }
                                }
                            }
                        }
                    }
                });

            Console.WriteLine("[docker] starting container " + EventStoreContainer);

            await _client.Containers.StartContainerAsync(EventStoreContainer, new ContainerStartParameters { });
            
            var endpoint = new Uri($"tcp://127.0.0.1:{port}");

            var settings = ConnectionSettings
                .Create()
                .KeepReconnecting()
                .KeepRetrying()
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));

            var connectionName =
                $"M={Environment.MachineName},P={Process.GetCurrentProcess().Id},T={DateTimeOffset.UtcNow.Ticks}";

            Connection = EventStoreConnection.Create(settings, endpoint, connectionName);

            Console.WriteLine("[docker] connecting to EventStore");

            await Connection.ConnectAsync();

            // Fix for EventStore throwing NotAuthenticatedException
            // because of not being ready but accepting connections
            var connected = false;

            while (!connected)
            {
                try
                {
                    await Connection.DeleteStreamAsync("stream", -1);
                    connected = true;
                }
                catch (NotAuthenticatedException ex)
                {
                    Console.WriteLine("foo");
                }
            }
        }

        public async Task DisposeAsync()
        {
            if (_client != null)
            {
                Connection?.Dispose();

                Console.WriteLine("[docker] stopping container " + EventStoreContainer);

                await _client.Containers.StopContainerAsync(EventStoreContainer, new ContainerStopParameters { });

                Console.WriteLine("[docker] removing container " + EventStoreContainer);

                await _client.Containers.RemoveContainerAsync(EventStoreContainer,
                    new ContainerRemoveParameters {Force = true});

                _client.Dispose();
            }
        }

        private class IgnoreProgress : IProgress<JSONMessage>
        {
            public static readonly IProgress<JSONMessage> Forever = new IgnoreProgress();

            public void Report(JSONMessage value)
            {
            }
        }
    }
}