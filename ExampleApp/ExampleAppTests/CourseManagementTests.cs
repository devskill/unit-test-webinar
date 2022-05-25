using Autofac.Extras.Moq;
using ExampleApp;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace ExampleAppTests
{
    public class CourseManagementTests
    {
        private AutoMock _mock;
        private Mock<IDbContext> _dbContextMock;
        private CourseManagement _courseManagement;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            _dbContextMock = _mock.Mock<IDbContext>();
            _courseManagement = _mock.Create<CourseManagement>();

        }

        [TearDown]
        public void CleanUp()
        {
            _dbContextMock?.Reset();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task CreateCourse_TitleMissing_ThrowsException(string title)
        {
            // Arrange
            const double fee = 8000;
            DateTime startDate = new DateTime(2022, 08, 08);

            // Act
            await Should.ThrowAsync<InvalidOperationException>(async () => 
                await _courseManagement.CreateCourseAsync(title, fee, startDate));
        }

        [Test]
        public async Task CreateCourse_ValidCourseValues_CreatesCourse()
        {
            // Arrange
            const string title = "Asp.net";
            const double fee = 30000;
            DateTime startDate = new DateTime(2022, 09, 08);

            _dbContextMock.Setup(x => x.AddItem(It.Is<Course>(y => y.Fee == fee
                && y.Title == title))).Verifiable();

            _dbContextMock.Setup(x => x.Save()).Verifiable();

            // Act
            await _courseManagement.CreateCourseAsync(title, fee, startDate);

            // Assert
            _dbContextMock.VerifyAll();
        }
    }
}