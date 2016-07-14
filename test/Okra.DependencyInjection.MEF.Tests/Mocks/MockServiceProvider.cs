﻿using Okra.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.DependencyInjection.MEF.Tests.Mocks
{
    public class MockServiceProvider : IServiceProvider, IDisposable
    {
        Dictionary<Type, Func<object>> _serviceDictionary = new Dictionary<Type, Func<object>>();

        public MockServiceProvider()
        {
        }

        public bool IsDisposed
        {
            get;
            private set;
        }

        public MockServiceProvider With<T>(T service)
        {
            _serviceDictionary[typeof(T)] = () => service ;
            return this;
        }

        public MockServiceProvider WithInjector<T>(IServiceInjector<T> serviceInjector)
        {
            _serviceDictionary[typeof(IServiceInjector<T>)] = () => serviceInjector;
            _serviceDictionary[typeof(T)] = () => serviceInjector.Service;
            return this;
        }

        public object GetService(Type serviceType)
        {
            return _serviceDictionary[serviceType]();
        }

        public void Dispose()
        {
            this.IsDisposed = true;
        }
    }
}
