using System;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Collections.Generic;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieStore.Controllers;
using MovieStore.Models;

namespace MovieStore.Tests.Controllers
{
    [TestClass]
    public class MovieStoreControllerTest
    {
        [TestMethod]
        public void MovieStore_Index_TestView()
        {
            //Arrange
            MoviesController controller = new MoviesController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_ListOfMovies()
        {
            //Arrange
            MoviesController controller = new MoviesController();

            //Act
            var result = controller.ListOfMovies();

            //Assert
            Assert.AreEqual("Terminator 1", result[0].Title);
            Assert.AreEqual("Terminator 2", result[1].Title);
            Assert.AreEqual("Terminator 3", result[2].Title);
        }

        [TestMethod]
        public void MovieStore_IndexRedirect_Success()
        {
            //Arrange
            MoviesController controller = new MoviesController();

            //Act
            var result = controller.IndexRedirect(1) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Create", result.RouteValues["action"]);
            Assert.AreEqual("HomeController", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void MovieStore_IndexRedirect_Failure()
        {
            //Arrange
            MoviesController controller = new MoviesController();

            //Act
            var result = controller.IndexRedirect(0) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode) result.StatusCode);
        }

        /*
        [TestMethod]
        public void MovieStore_ListFromDb()
        {
            //Goal is to use our own list(movie) instead of calling the bd
            //Step 1
            var list = new List<Movie> {
                new Movie { MovieId = 1, Title="Jaws"},
                new Movie { MovieId = 2, Title="Jurrrasic Park"}
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(list.Expression);

            //Step 4 connect dbset to context
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            //Step 5 send the mock context to controller
            //Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            ViewResult result = controller.ListFromDb() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
        */
        [TestMethod]
        public void MovieStore_ListFromDb()
        {
            //Goal is to use our own list(movie) instead of calling the bd
            //Step 1
            var list = new List<Movie> {
                new Movie { MovieId = 1, Title="Jaws"},
                new Movie { MovieId = 2, Title="Jurrrasic Park"}
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(list.Expression);

            //Step 4 connect dbset to context
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            //Step 5 send the mock context to controller
            //Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            ViewResult result = controller.ListFromDb() as ViewResult;
            List<Movie> resultModel = result.Model as List<Movie>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Jaws", resultModel[0].Title);
            Assert.AreEqual("Jurrasic Park", resultModel[1].Title);
        }

        [TestMethod]
        public void MovieStore_Details_Success()
        {
            //Goal is to use our own list(movie) instead of calling the bd
            //Step 1
            var list = new List<Movie> {
                new Movie { MovieId = 1, Title="Jaws"},
                new Movie { MovieId = 2, Title="Jurrrasic Park"}
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(list.Expression);

            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            //Step 4 connect dbset to context
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            //Step 5 send the mock context to controller
            //Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            ViewResult result = controller.Details(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_Details_No_Id()
        {
            //Goal is to use our own list(movie) instead of calling the bd
            //Step 1
            var list = new List<Movie> {
                new Movie { MovieId = 1, Title="Jaws"},
                new Movie { MovieId = 2, Title="Jurrrasic Park"}
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(list.Expression);

            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            //Step 4 connect dbset to context
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            //Step 5 send the mock context to controller
            //Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            //Id is null
            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void MovieStore_Details_MovieNull()
        {
            //Goal is to use our own list(movie) instead of calling the bd
            //Step 1
            var list = new List<Movie> {
                new Movie { MovieId = 1, Title="Jaws"},
                new Movie { MovieId = 2, Title="Jurrrasic Park"}
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(list.Expression);

            //Pass movie as null
            Movie movie = null;
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(movie);

            //Step 4 connect dbset to context
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            //Step 5 send the mock context to controller
            //Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act

            HttpStatusCodeResult result = controller.Details(1) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }

    }
}
