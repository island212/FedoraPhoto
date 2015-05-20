using FedoraPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FedoraPhoto.DAL
{
    public class FactureRepository : GenericRepository<Facture>
    {
        public FactureRepository(Model1 context) : base(context) { }

        public IEnumerable<Facture> ObtenirFactures()
        {
            return Get();
        }

        public IEnumerable<Facture> ObtenirFacturesTries()
        {
            return Get(orderBy: a => a.OrderBy(al => al.FactureID));
        }

        public Facture ObtenirFactureParID(int? id)
        {
            return GetByID(id);
        }

        public void InsererFacture(Facture facture)
        {
            Insert(facture);
        }

    }
}