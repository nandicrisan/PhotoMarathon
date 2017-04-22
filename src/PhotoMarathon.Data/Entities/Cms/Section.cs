using System.Collections;
using PhotoMarathon.Data.Entities.Base;
using System.Collections.Generic;

namespace PhotoMarathon.Data.Entities.Cms
{
    public class Section : CmsBase
    {
        public string Content { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual Page Page { get; set; }
    }
}
