using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FedoraPhoto.DAL
{
    interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}