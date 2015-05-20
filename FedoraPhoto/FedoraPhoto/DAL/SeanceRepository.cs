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

        public IEnumerable<Seance> ObtenirSeancesByPhotographeId(int id)
        {
            return Get().Where(x => x.PhotographeID == id);
        }

        public IEnumerable<Seance> ObtenirSeancesTries()
        {
            return Get(orderBy: a => a.OrderByDescending(al => al.DateSeance));
        }

        public IEnumerable<Seance> ObtenirSeancesTriesParStatut()
        {
            var seances = ObtenirSeances();
            List<Seance> seancesTries = new List<Seance>();
            var lst_StatutSeance = new Dictionary<string, List<Seance>>();

            foreach (Seance seance in seances)
            {
                if (!lst_StatutSeance.ContainsKey(seance.Statut))
                {
                    lst_StatutSeance[seance.Statut] = new List<Seance>();
                }
                lst_StatutSeance[seance.Statut].Add(seance);
            }

            if (lst_StatutSeance.ContainsKey("demandée"))
            {
                foreach (var seance in lst_StatutSeance["demandée"])
                {
                    seancesTries.Add(seance);
                }
            }

            if (lst_StatutSeance.ContainsKey("Confirmée"))
            {
                foreach (var seance in lst_StatutSeance["Confirmée"])
                {
                    seancesTries.Add(seance);
                }
            }

            if (lst_StatutSeance.ContainsKey("Reportée"))
            {
                foreach (var seance in lst_StatutSeance["Reportée"])
                {
                    seancesTries.Add(seance);
                }
            }

            if (lst_StatutSeance.ContainsKey("Réalisé"))
            {
                foreach (var seance in lst_StatutSeance["Réalisé"])
                {
                    seancesTries.Add(seance);
                }
            }

            if (lst_StatutSeance.ContainsKey("Livrée"))
            {
                foreach (var seance in lst_StatutSeance["Livrée"])
                {
                    seancesTries.Add(seance);
                }
            }

            if (lst_StatutSeance.ContainsKey("Facturée"))
            {
                foreach (var seance in lst_StatutSeance["Facturée"])
                {
                    seancesTries.Add(seance);
                }
            }

            seances = seancesTries.AsQueryable();
            return seances;
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