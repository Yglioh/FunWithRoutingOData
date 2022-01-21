using System.Collections.Generic;

namespace Models.V1
{
    /// <summary>
    /// Example of a first level entity.
    /// </summary>
    public class Project
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public ICollection<ProcessFlow> ProcessFlows { get; set;}
    }
}
