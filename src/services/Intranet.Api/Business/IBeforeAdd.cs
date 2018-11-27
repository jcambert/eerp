using EntityFrameworkCore.Triggers;
using Intranet.Api.models;
using System;

namespace Intranet.Api.Business
{
    public interface IBeforeAdd : IDisposable
    {
    }

    public interface IAfterAdd : IDisposable
    {

    }

    public class Toto : IBeforeAdd
    {

        public void Dispose()
        {
            
        }
    }
}
