using FedoraPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FedoraPhoto.DAL
{
    public class UnitOfWork
    {
        private Model1 context = new Model1();

        private SeanceRepository seanceRepository;

        public SeanceRepository SeanceRepository
        {
            get
            {
                if(this.seanceRepository == null)
                {
                    this.seanceRepository = new SeanceRepository(context);
                }
                return seanceRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}