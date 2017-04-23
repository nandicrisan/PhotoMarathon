using System;
using PhotoMarathon.Data.Entities.Base;

namespace PhotoMarathon.Data.Entities.Cms
{
    public abstract class CmsBase : IEntityBase
    {
        public int Id { get; set; }
        //Friendly name in administration site also the slug generated from this
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Slug { get; set; }
        public DateTime DateModified { get; set; }
    }
}
