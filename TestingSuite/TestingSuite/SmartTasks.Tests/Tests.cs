using Domain.Entities;

namespace SmartTasks.Tests
{
    public class Tests
    {
        [Fact]
        public void ShouldCreateTask()
        {
            //arrange
            var title = "test";
            var description = "description";
            var expDate = DateTime.Now;
            var priority = 1;
            var labels = new List<string> { "t1", "t2" };

            //act
            var task = new SmartTask(title, description, expDate, priority, labels);

            //assert
            Assert.Equal(task.Title, title);
            Assert.Equal(task.Description, description);
            Assert.Equal(task.ExpDate, expDate);
        }
    }
}