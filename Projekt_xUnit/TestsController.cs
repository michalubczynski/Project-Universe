using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingBLL.Controllers;
using Tests_xUnit.FakeClasses;
using Universe.Models.planet;

namespace Tests_xUnit
{
	public class TestsController
	{
		// 1
		[Fact]
		public void GetAllPlanetsCountTest()
		{
			var mockBLL = new Mock<IService>();
			int count = 3;
			mockBLL.Setup(m => m.GetAllPlanetsCount().Result).Returns(count);
			var serviceController = new ServiceController(mockBLL.Object);
			var result = serviceController.GetAllPlanetsCount();
			// var viewResult = (ViewResult)result;
			// w przykadzie dziaa, a u nas blad kompilacji...
			Assert.Equal("5", result.Result.ToString());
		}
		// 3.1
		[Fact]
		public void APIRandomStarsTest()
		{
			var mockService = new MockService();
			var serviceAPIController = new ServiceAPIController(mockService);
			var count = 6;
			serviceAPIController.AddRandomStars(count);
			Assert.Equal(count, mockService.StarCount);
		}
		// 3.2
		[Fact]
		public void APIGetHeaviestPlanetTest()
		{
			var mockService = new Mock<IService>();
			var p = new Planet
			{
				Id = 1
			};
			mockService.Setup(m => m.GetHeaviestPlanet().Result).Returns(p);
			var serviceAPIController = new ServiceAPIController(mockService.Object);
			var result = serviceAPIController.GetHeaviestPlanet().Result;
			Assert.Equal<Planet>(p, result);
		}
	}
}
