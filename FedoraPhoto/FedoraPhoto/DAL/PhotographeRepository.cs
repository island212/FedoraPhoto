using FedoraPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FedoraPhoto.DAL
{
    public class PhotographeRepository : GenericRepository<Photographe>
    {
        public PhotographeRepository(Model1 context) : base(context) { }

        public IEnumerable<Photographe> ObtenirPhotographes()
        {
            return Get();
        }

        public IEnumerable<Photographe> ObtenirPhotographesTries()
        {
            return Get(orderBy: a => a.OrderByDescending(al => al.Nom));
        }

        public Photographe ObtenirPhotographeParID(int? id)
        {
            return GetByID(id);
        }

        public void InsererPhotographe(Photographe photographe)
        {
            Insert(photographe);
        }
    }
}