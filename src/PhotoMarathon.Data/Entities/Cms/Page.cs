using System.Collections.Generic;
using PhotoMarathon.Data.Entities.Base;

namespace PhotoMarathon.Data.Entities.Cms
{
    public class Page : CmsBase
    {
        public virtual ICollection<Section> Sections { get; set; }
    }
}
