using System.Collections.Generic;

namespace Models.V1
{
    /// <summary>
    /// Example of a second level entity.
    /// </summary>
    public class ProcessFlow
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public ICollection<Parameter> Parameters { get; set; }
    }
}
