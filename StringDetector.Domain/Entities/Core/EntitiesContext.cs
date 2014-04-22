using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Entities {

    public class EntitiesContext : DbContext {

        public EntitiesContext() : base("StringDetector") { }

        public IDbSet<JobEntity> Jobs { get; set; }
        public IDbSet<JobStateEntity> JobStates { get; set; }
        public IDbSet<AutoGenerateKeyEntity> AutoKeys { get; set; }
    }
}