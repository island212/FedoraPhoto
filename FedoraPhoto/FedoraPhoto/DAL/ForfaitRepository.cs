using FedoraPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FedoraPhoto.DAL
{
    public class ForfaitRepository : GenericRepository<Forfait>
    {
        public ForfaitRepository(Model1 context) : base(context) { }

        public IEnumerable<Forfait> ObtenirForfaits()
        {
            return Get();
        }

        public IEnumerable<Forfait> ObtenirForfaitsTries()
        {
            return Get(orderBy: a => a.OrderBy(al => al.NomForfait));
        }

        public Forfait ObtenirForfaitParID(int? id)
        {
            return GetByID(id);
        }

        public void InsererForfait(Forfait forfait)
        {
            Insert(forfait);
        }
    }
}