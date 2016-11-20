using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Contract.Model
{
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;

    public class ProjectSelectorDto : IDEntity
    {
        public long ProjectID { get; set; }

        public SolutionDto Solution { get; set; }
    }
}
