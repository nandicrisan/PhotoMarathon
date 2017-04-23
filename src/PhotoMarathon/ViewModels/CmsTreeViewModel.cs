using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PhotoMarathon.Data.Entities.Enumes;

namespace PhotoMarathon.ViewModels
{
    public class CmsTreeViewModel
    {
        public List<Node> Nodes { get; set; }
    }

    public class Node
    {
        public string text { get; set; }
        public int id { get; set; }
        public CmsStructureType type { get; set; }
        public List<Node> nodes { get; set; }
    }
}
