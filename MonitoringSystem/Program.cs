using MonitoringSystem;
using Quartz;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        //// Add the required Quartz.NET services
        //services.AddQuartz(q => {
        //    // Use a Scoped container to create jobs.
        //    q.UseMicrosoftDependencyInjectionJobFactory();
        //    // Create a "key" for the job
        //    var jobKey = new JobKey("Task1");
        //    // Register the job with the DI container
        //    q.AddJob<GetData>(opts => 
        //    opts.WithIdentity(jobKey));
        //    // Create a trigger for the job
        //    q.AddTrigger(opts => opts.ForJob(jobKey) // link to the Task1
        //        .WithIdentity("Task1-trigger") // give the trigger a unique name
        //        .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds
        //});
        //// Add the Quartz.NET hosted service
        //services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

    })
    .Build();

await host.RunAsync();
