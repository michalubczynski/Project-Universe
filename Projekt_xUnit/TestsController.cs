using BLL_BuisnessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingBLL.Controllers;

namespace Tests_xUnit
{
	public class TestsController
	{
		// punkt 1
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
	}
}
