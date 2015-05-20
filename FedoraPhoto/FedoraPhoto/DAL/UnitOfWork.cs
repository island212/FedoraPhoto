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

        private PhotographeRepository photographeRepository;

        public PhotographeRepository PhotographeRepository
        {
            get
            {
                if(this.photographeRepository == null)
                {
                    this.photographeRepository = new PhotographeRepository(context);
                }
                return photographeRepository;
            }
        }

        private PhotoRepository photoRepository;

        public PhotoRepository PhotoRepository
        {
            get
            {
                if(this.photoRepository == null)
                {
                    this.photoRepository = new PhotoRepository(context);
                }
                return photoRepository;
            }
        }

        private AgentRepository agentRepository;

        public AgentRepository AgentRepository
        {
            get
            {
                if(this.agentRepository == null)
                {
                    this.agentRepository = new AgentRepository(context);
                }
                return agentRepository;
            }
        }

        private FactureRepository factureRepository;

        public FactureRepository FactureRepository
        {
            get
            {
                if(this.factureRepository == null)
                {
                    this.factureRepository = new FactureRepository(context);
                }
                return factureRepository;
            }
        }

        private ForfaitRepository forfaitRepository;

        public ForfaitRepository ForfaitRepository
        {
            get
            {
                if(this.forfaitRepository == null)
                {
                    this.forfaitRepository = new ForfaitRepository(context);
                }
                return forfaitRepository;
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