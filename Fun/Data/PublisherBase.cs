using System;
using System.Collections.Generic;
using System.Linq;

namespace Fun.Data
{
    public abstract class PublisherBase<T> : IPublisher<T>
    {
        private readonly HashSet<ISubscriber<T>> _subscribers;

        protected PublisherBase()
        {
            _subscribers = new HashSet<ISubscriber<T>>();
        }

        public IDisposable Subscribe(ISubscriber<T> subscriber)
        {
            _subscribers.Add(subscriber);
            return new Unsubscriber(this, subscriber);
        }

        protected IEnumerable<ISubscriber<T>> Subscribers => _subscribers.AsEnumerable();

        private class Unsubscriber : IDisposable
        {
            private readonly PublisherBase<T> _publisher;

            private readonly ISubscriber<T> _subscriber;

            public Unsubscriber(PublisherBase<T> publisher, ISubscriber<T> subscriber)
            {
                _publisher = publisher;
                _subscriber = subscriber;
            }

            public void Dispose() => _publisher._subscribers.Remove(_subscriber);
        }
    }
}
