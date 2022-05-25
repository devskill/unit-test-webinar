using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp
{
    public class DbContextConcrete : IDbContext
    {
        private TrainingDbContext _trainingDbContext;
        public DbContextConcrete(TrainingDbContext context)
        {
            _trainingDbContext = context;
        }

        public void AddItem(Course course)
        {
            _trainingDbContext.Courses.Add(course);
        }

        public void Save()
        {
            _trainingDbContext.SaveChanges();
        }
    }
}
