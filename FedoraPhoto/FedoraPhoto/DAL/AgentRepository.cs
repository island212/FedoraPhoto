using FedoraPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FedoraPhoto.DAL
{
    public class AgentRepository : GenericRepository<Agent>
    {
        public AgentRepository(Model1 context) : base(context) { }

        public IEnumerable<Agent> ObtenirAgents()
        {
            return Get();
        }

        public IEnumerable<Agent> ObtenirAgentsTries()
        {
            return Get(orderBy: a => a.OrderBy(al => al.Nom));
        }

        public Agent ObtenirAgentParID(int? id)
        {
            return GetByID(id);
        }

        public void InsererAgent(Agent agent)
        {
            Insert(agent);
        }
    }
}