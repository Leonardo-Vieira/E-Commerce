using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
using Autofac;
using Domain_Core.Bus;
using Domain_Core.Events;
using Newtonsoft.Json;

namespace Domain_Core.Bus {
    public class EventBus : IEventBus 
    {
        private const string QUEUE_CLIENT = "queue.client";
        private const string QUEUE_ORDER = "queue.order";
        private const string QUEUE_PRODUCT = "queue.product";
        private ConnectionFactory _factory = new ConnectionFactory ("activemq:tcp://" + Environment.MachineName + ":61616");
       // private ConnectionFactory _factory = new ConnectionFactory ("tcp://172.20.128.2:61616");
        private readonly List<Type> _eventTypes;
        private readonly Dictionary<string, Type> _handlersTypes;
        private readonly ILifetimeScope _autofac;
        public EventBus (ILifetimeScope autofac) 
        {
            _eventTypes = new List<Type>();
            _handlersTypes = new Dictionary<string, Type>();
            _autofac = autofac;
        }
        public void Publish (IntegrationEvent @event) 
        {
            IConnection conn = _factory.CreateConnection();
            ISession session = conn.CreateSession (AcknowledgementMode.ClientAcknowledge);

            var eventName = @event.GetType().Name;

            IDestination destinationOrder = SessionUtil.GetDestination (session, QUEUE_ORDER);
            IDestination destinationClient = SessionUtil.GetDestination (session, QUEUE_CLIENT);
            IDestination destinationProduct = SessionUtil.GetDestination (session, QUEUE_PRODUCT);

            IMessageProducer producerOrder = session.CreateProducer (destinationOrder);
            IMessageProducer producerClient = session.CreateProducer (destinationClient);
            IMessageProducer producerProduct = session.CreateProducer (destinationProduct);

            conn.Start ();
            var message = session.CreateTextMessage (JsonConvert.SerializeObject (@event));
            message.Properties.SetString ("RoutingKey", eventName);
            producerOrder.Send (message);
            producerClient.Send (message);
            producerProduct.Send (message);
            conn.Stop ();

        }

        public void Consume (string destination) 
        {
            IConnection conn = _factory.CreateConnection();
            ISession session = conn.CreateSession (AcknowledgementMode.ClientAcknowledge);
            conn.Start ();
            var queue = session.GetDestination(destination);

            var consumer = session.CreateConsumer(queue);
            consumer.Listener += new MessageListener (ConsumeMessage);
        }

        public void Subscribe<TIntergrationEvent, TEventHandler>()
        where TIntergrationEvent : IntegrationEvent
        where TEventHandler : IEventHandler 
        {
            var eventName = typeof (TIntergrationEvent).Name;

            _eventTypes.Add (typeof (TIntergrationEvent));
            _handlersTypes[eventName] = (typeof (TEventHandler));

        }

        public void Unsubscribe<TIntergrationEvent, TEventHandler> ()
        where TIntergrationEvent : IntegrationEvent
        where TEventHandler : IEventHandler 
        {
            throw new NotImplementedException ();
        }

        public async void ConsumeMessage (IMessage message) 
        {
            string eventName = message.Properties.GetString("RoutingKey");

            Type eventType = _eventTypes.Find(t => t.Name == eventName);
            if (eventType != null) 
            {

                var integrationEvent = JsonConvert.DeserializeObject ((message as ITextMessage).Text, eventType);
                Type concreteType = typeof (IEventHandler<>).MakeGenericType (eventType);

                using (var scope = _autofac.BeginLifetimeScope()) 
                {
                    var handler = scope.ResolveOptional(_handlersTypes[eventName]);
                    await (Task) concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                }
            }
            message.Acknowledge ();
        }
    }
}