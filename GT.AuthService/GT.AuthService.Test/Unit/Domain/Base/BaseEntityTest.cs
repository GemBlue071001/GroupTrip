using GT.AuthService.Domain.Base;
using Xunit;
using Assert = Xunit.Assert;

namespace GT.AuthService.Test.Unit.Domain.Base
{
    public class BaseEntityTests
    {
        [Fact]
        public void Constructor_ShouldInitializeIdAndTimestamps()
        {
            // Arrange & Act
            var entity = new BaseEntity();
            
            // Assert
            Assert.NotNull(entity.Id);
            Assert.NotEmpty(entity.Id);
            Assert.Equal(32, entity.Id.Length); // GUID without hyphens
            Assert.True(entity.CreatedTime <= DateTime.Now);
            Assert.Equal(entity.CreatedTime, entity.LastUpdatedTime);
        }

        [Fact]
        public void Constructor_ShouldGenerateUniqueIds()
        {
            // Arrange & Act
            var entity1 = new BaseEntity();
            var entity2 = new BaseEntity();
            
            // Assert
            Assert.NotEqual(entity1.Id, entity2.Id);
        }

        [Fact]
        public void Properties_ShouldBeSettableAndGettable()
        {
            // Arrange
            var entity = new BaseEntity();
            var testDate = DateTime.Now.AddDays(-1);
            
            // Act
            entity.CreatedBy = "TestUser";
            entity.LastUpdatedBy = "UpdateUser";
            entity.DeletedBy = "DeleteUser";
            entity.DeletedTime = testDate;
            
            // Assert
            Assert.Equal("TestUser", entity.CreatedBy);
            Assert.Equal("UpdateUser", entity.LastUpdatedBy);
            Assert.Equal("DeleteUser", entity.DeletedBy);
            Assert.Equal(testDate, entity.DeletedTime);
        }
    }
}