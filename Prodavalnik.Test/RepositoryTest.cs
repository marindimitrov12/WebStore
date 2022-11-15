
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Prodavalnik.Areas.Identity.Data;
using Prodavalnik.Controllers;
using Prodavalnik.Core.IConfiguration;
using Prodavalnik.Core.IReposotories;
using Prodavalnik.Core.Repositories;
using Prodavalnik.Data;
using Prodavalnik.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Prodavalnik.Test
{
    public class RepositoryTest
    {
        [Fact]
        public void Test_UnitOfWork_CompileAsync_is_Called()
        {
            //Arrange
            var unit = new Mock<IUnitOfWork>();
            unit.Setup(x => x.CompliteAsync()).Verifiable();
            //Act
            unit.Object.CompliteAsync();
            //Assert
            unit.Verify(x=>x.CompliteAsync(),Times.Once);
        }
        [Fact]
        public void Test_UnitOfWork_Dispose_is_Called()
        {
            //Arrange
            var unit = new Mock<IUnitOfWork>();
            unit.Setup(x => x.Dispose()).Verifiable();
            //Act
            unit.Object.Dispose();
            //Assert
            unit.Verify(x => x.Dispose(), Times.Once);
        }
        [Fact]
        public async Task When_CompliteAsync_is_execute_SaveChanges_is_Called()
        {
            //Arrange
            var db=new Mock<ApplicationDbContext>();
           
            var loggerF = new Mock<ILoggerFactory>();
            var unit = new UnitOfWork(db.Object,loggerF.Object);
            //Act
            unit.CompliteAsync();
            //Assert
            db.Verify(x=>x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()),Times.Once);
        }
        [Fact]
        public void DisposeAsync_Is_Called_Correctly()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            //Act
            mock.Object.DisposeAsync();
            //Assert
            mock.Verify(x=>x.DisposeAsync(),Times.Once);

        }
        
       
        

internal static Mock<DbSet<T>> GetMockDbSet<T>(ICollection<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(entities.Add);
            return mockSet;
        }

    }
}