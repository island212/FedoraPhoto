using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FedoraPhoto.Models;

namespace FedoraPhoto.DAL
{
    public class PhotoRepository : GenericRepository<Photo>
    {
        public PhotoRepository(Model1 context) : base(context) { }
    }
}