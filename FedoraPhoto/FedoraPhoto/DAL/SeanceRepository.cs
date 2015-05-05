using FedoraPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FedoraPhoto.DAL
{
    public class SeanceRepository : GenericRepository<Seance>
    {
        public SeanceRepository(Model1 context) : base(context) { }

        public IEnumerable<Seance> ObtenirSeances()
        {
            return Get();
        }

        public IEnumerable<Seance> ObtenirSeancesTries()
        {
            return Get(orderBy: a => a.OrderByDescending(al => al.DateSeance));
        }

        public Seance ObtenirSeanceParID(int? id)
        {
            return GetByID(id);
        }

        public void InsererSeance(Seance seance)
        {
            Insert(seance);
        }
    }
}