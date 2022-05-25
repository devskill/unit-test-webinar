// See https://aka.ms/new-console-template for more information

using ExampleApp;

CourseManagement courseManagement = new CourseManagement(new DbContextConcrete(new TrainingDbContext()));
await courseManagement.CreateCourseAsync("Laravel", 18000, new DateTime(2022, 08, 08));

