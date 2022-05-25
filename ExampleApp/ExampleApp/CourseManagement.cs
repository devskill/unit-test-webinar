using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp
{
    public class CourseManagement
    {
        private readonly IDbContext _dbContext;

        public CourseManagement(IDbContext context)
        {
            _dbContext = context;
        }

        public async Task CreateCourseAsync(string title, double fees, DateTime classStartDate)
        {
            if (!await IsValidCourseTitleAsync(title))
                throw new InvalidOperationException("Course title is invalid");

            fees = Math.Round(fees);

            if (classStartDate.Subtract(DateTime.Now).TotalDays < 30)
                throw new Exception("Class start date should be at least 30 days ahead");

            Course course = new Course();
            course.Title = title;
            course.Fee = fees;
            course.ClassStartDate = classStartDate;

            _dbContext.AddItem(course);
            _dbContext.Save();
        }

        private async Task<bool> IsValidCourseTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return false;
            else if (title.Length > 100)
                return false;
            else
                return true;
        }
    }
}
