namespace PhotoMarathon.Data.Entities.Cms
{
    public class Article : CmsBase
    {
        public string Content { get; set; }
        public virtual Section Section { get; set; }
    }
}
