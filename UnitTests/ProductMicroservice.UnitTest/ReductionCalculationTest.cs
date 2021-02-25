namespace ProductMicroservice.UnitTest
{
    using DeliVeggieSharedLibrary.Interfaces;
    using Moq;
    using ProductMicroservice.Services;
    using System;
    using System.Threading.Tasks;
    using Xunit;
    public class ReductionCalculationTest
    {
        [Fact]
        public async Task GetReducedPrice_WhenReduction3Percent_Return30()
        {
            //Arrange
            var mockDbServiceAsync = new Mock<IDbService>();
            mockDbServiceAsync
                .Setup(t => t.GetReductionforTodayAsync(DateTime.Now.DayOfWeek))
                    .ReturnsAsync(await Task.FromResult(.3));

            var price = 100;

            //Act
            var _sut = new ReductionCalculation(mockDbServiceAsync.Object);
            var reducedPrice = await _sut.GetReducedPrice(price);

            //Assert
            Assert.Equal(30, reducedPrice);
        }
        [Fact]
        public async Task GetReducedPrice_WhenReduction0Percent_ReturnOriginalPrice()
        {
            //Arrange
            var mockDbServiceAsync = new Mock<IDbService>();
            mockDbServiceAsync
                .Setup(t => t.GetReductionforTodayAsync(DateTime.Now.DayOfWeek))
                    .ReturnsAsync(await Task.FromResult(0));

            var price = 100;

            //Act
            var _sut = new ReductionCalculation(mockDbServiceAsync.Object);
            var reducedPrice = await _sut.GetReducedPrice(price);

            //Assert
            Assert.True(reducedPrice == 100);
        }
        [Fact]
        public async Task GetReducedPrice_WhenReductionNull_ReturnOriginalPrice()
        {
            //Arrange
            DayOfWeek dayOfWeek = (DayOfWeek)7;

            var mockDbServiceAsync = new Mock<IDbService>();
            mockDbServiceAsync
                .Setup(t => t.GetReductionforTodayAsync(dayOfWeek))
                    .ReturnsAsync(null);

            var price = 100;

            //Act
            var _sut = new ReductionCalculation(mockDbServiceAsync.Object);
            var reducedPrice = await _sut.GetReducedPrice(price);

            //Assert
            Assert.True(reducedPrice == 100);
        }
    }
}
