namespace StringDetector.Domain.Migrations
{
    using StringDetector.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<StringDetector.Domain.Entities.EntitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StringDetector.Domain.Entities.EntitiesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            Guid[] jobKeys = new[] { Guid.Parse("628b9071-d851-4778-8d22-c6dfcd7ecfb1"), Guid.Parse("e9a2479f-a527-4336-a9ed-5e72bbde5a4f") };
            Guid[] jobStateKeys = new[] { Guid.Parse("8a8cf144-40aa-4cea-9ca6-2b852ae0d308"), Guid.Parse("d4311f71-ef74-4b37-9a87-8fde050fe4df"), Guid.Parse("0f14398a-755b-49b8-b938-be219a4e234d"), Guid.Parse("6970d9b2-4e2e-4368-8ed0-ec5bc086eea9"), Guid.Parse("d1fcf873-0f46-4eda-8ce1-3fba31b99d26"), Guid.Parse("cadb5b31-7d56-4a0a-a790-9d60f574ab28") };
            Guid[] autoKeys= new[] {Guid.Parse("045bb9e5-e2b5-4696-a6ea-15113aa16307")};
            context.Jobs.AddOrUpdate(job => job.Key,
                new JobEntity { Key = jobKeys[0], ProjectName = "BugTracker", JobNumber = "000001", SourcePath = "basepath\\Project\\BugTracker\\src", Configuration = " SOURCE_DIRECTORIES = ['{here}']", Report = "c:\\hwsig.log" },
                new JobEntity { Key = jobKeys[1], ProjectName = "BugTrackerAdmin", JobNumber = "000002", SourcePath = "basepath\\Project\\BugTrackerAdmin\\src", Configuration = " SOURCE_DIRECTORIES = ['{here}']", Report = "basepath\\Report\\BugTrackerAdmin\\" }
                );
            context.JobStates.AddOrUpdate(jobState => jobState.Key,
            new JobStateEntity { Key = jobStateKeys[0], JobKey = jobKeys[0], JobStatus = JobStatusEnum.JOB_CRATED, CreatedOn = DateTime.Now.AddDays(-20) },
            new JobStateEntity { Key = jobStateKeys[1], JobKey = jobKeys[0], JobStatus = JobStatusEnum.BEGIN_LAUNCH, CreatedOn = DateTime.Now.AddDays(-19).AddHours(1) },
            new JobStateEntity { Key = jobStateKeys[2], JobKey = jobKeys[0], JobStatus = JobStatusEnum.RUNNING, CreatedOn = DateTime.Now.AddDays(-19).AddHours(1) },
            new JobStateEntity { Key = jobStateKeys[3], JobKey = jobKeys[0], JobStatus = JobStatusEnum.ENDS_WITH_SUCCESS, CreatedOn = DateTime.Now.AddDays(-19).AddHours(2) },
            new JobStateEntity { Key = jobStateKeys[4], JobKey = jobKeys[1], JobStatus = JobStatusEnum.JOB_CRATED, CreatedOn = DateTime.Now.AddDays(-20) },
            new JobStateEntity { Key = jobStateKeys[5], JobKey = jobKeys[1], JobStatus = JobStatusEnum.BEGIN_LAUNCH, CreatedOn = DateTime.Now.AddDays(-18).AddHours(4) }
              );

            context.AutoKeys.AddOrUpdate(autoKey => autoKey.Key,
                new AutoGenerateKeyEntity { Key = autoKeys[0], NextKey = 990001 });

        }
    }
}
