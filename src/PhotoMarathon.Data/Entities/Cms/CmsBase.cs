using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using PhotoMarathon.Data.Entities.Base;

namespace PhotoMarathon.Data.Entities.Cms
{
    public abstract class CmsBase : IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime DateModified { get; set; }
    }
}
