using System;
using System.Diagnostics;
using EventStore.ClientAPI;
using System.Collections.Generic;
using EventStore.ClientAPI.SystemData;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Threading.Tasks;
using EventStore.ClientAPI.Exceptions;
using Raven.Embedded;
using Xunit;

namespace BoostRoom.Integration.Tests
{
    public class IntegrationFixture : IAsyncLifetime 
    {
        public IEventStoreConnection EventStoreConnection { get; private set; }

        private const string EventStoreImage = "eventstore/eventstore";
        
        private string _eventStoreContainer; 

        private DockerClient _client;

        public IntegrationFixture() 
        {
            EmbeddedServer.Instance.StartServer(new ServerOptions
            {
                CommandLineArgs = new List<string>() {"RunInMemory=true"}
            });
        }

        public async Task InitializeAsync()
        {
            _eventStoreContainer = "es" + Guid.NewGuid().ToString("N");
            
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

            Console.WriteLine("[docker] creating container " + _eventStoreContainer);

            var port = 1113;
            
            await _client.Containers.CreateContainerAsync(
                new CreateContainerParameters
                {
                    Image = EventStoreImage,
                    Name = _eventStoreContainer,
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

            Console.WriteLine("[docker] starting container " + _eventStoreContainer);

            await _client.Containers.StartContainerAsync(_eventStoreContainer, new ContainerStartParameters { });
            
            var endpoint = new Uri($"tcp://127.0.0.1:{port}");

            var settings = ConnectionSettings
                .Create()
                .KeepReconnecting()
                .KeepRetrying()
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));

            var connectionName =
                $"M={Environment.MachineName},P={Process.GetCurrentProcess().Id},T={DateTimeOffset.UtcNow.Ticks}";

            EventStoreConnection = EventStore.ClientAPI.EventStoreConnection.Create(settings, endpoint, connectionName);

            Console.WriteLine("[docker] connecting to EventStore");

            await EventStoreConnection.ConnectAsync();

            // Fix for EventStore throwing NotAuthenticatedException
            // because of not being ready but accepting connections
            var connected = false;

            while (!connected)
            {
                try
                {
                    await EventStoreConnection.DeleteStreamAsync("stream", -1);
                    connected = true;
                }
                catch (NotAuthenticatedException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public async Task DisposeAsync()
        {
            if (_client != null)
            {
                EventStoreConnection?.Dispose();

                Console.WriteLine("[docker] stopping container " + _eventStoreContainer);

                await _client.Containers.StopContainerAsync(_eventStoreContainer, new ContainerStopParameters { });

                Console.WriteLine("[docker] removing container " + _eventStoreContainer);

                await _client.Containers.RemoveContainerAsync(_eventStoreContainer,
                    new ContainerRemoveParameters {Force = true});

                _client.Dispose();
            }
            
            EmbeddedServer.Instance.Dispose();
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