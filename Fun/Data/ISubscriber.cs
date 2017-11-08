namespace Fun
{
    public interface ISubscriber<T>
    {
        Unit OnNext(Result<T> result);

        Unit OnComplete();
    }
}
