﻿using Common.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using ProcessManagerActor.Interfaces;
using ProcessManagerActor.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISocketStatelessService
{
    public class SignalRHub : Hub
    {
        public void SendData(ReadingResult readingResult)
        {
            Clients.All.broadcastMessage(readingResult);
        }

        public void RequestSubscription(string deviceId, string channel)
        {
            // Subscribe to the right actor
            var proxy = ActorProxy.Create<IProcessManagerActor>(new ActorId($"{deviceId}_{channel}"), "ServiceFabricHackathon", "ProcessManagerActorService");
            proxy.SubscribeAsync<IDeviceReadingProcessedEvent>(new DeviceReadingProcessor());
        }
    }
}