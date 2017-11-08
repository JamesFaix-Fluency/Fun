using System;

namespace Fun
{
    public interface IPublisher<T>
    {
        IDisposable Subscribe(ISubscriber<T> subscriber);
    }
}
