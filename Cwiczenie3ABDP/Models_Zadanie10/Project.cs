using System;
using System.Collections.Generic;

namespace Cwiczenie3ABDP.Models_Zadanie10
{
    public partial class Project
    {
        public Project()
        {
            Task = new HashSet<Task>();
        }

        public int IdProject { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }

        public virtual ICollection<Task> Task { get; set; }
    }
}
