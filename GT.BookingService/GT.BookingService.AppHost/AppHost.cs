var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GT_BookingService_Api>("gt-bookingservice-api");

builder.AddProject<Projects.GT_BookingService_Application>("gt-bookingservice-application");

builder.AddProject<Projects.GT_BookingService_Domain>("gt-bookingservice-domain");

builder.AddProject<Projects.GT_BookingService_Infrastructure>("gt-bookingservice-infrastructure");

builder.AddProject<Projects.GT_BookingService_Test>("gt-bookingservice-test");

builder.Build().Run();
