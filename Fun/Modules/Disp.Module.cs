using System;
using System.Collections.Generic;
using System.Text;

namespace Fun
{
    public static class Disp
    {
        public static Disp<T, TDisposable> Create<T, TDisposable>(T value, TDisposable resource)
            where TDisposable : IDisposable
        {
            return new Disp<T, TDisposable>(value, resource);
        }
    }
}
