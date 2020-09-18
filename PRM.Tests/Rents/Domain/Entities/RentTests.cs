using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Products;
using PRM.Domain.Products.Enums;
using PRM.Domain.Products.Extensions;
using PRM.Domain.Renters;
using PRM.Domain.Rents;
using Xunit;

namespace PRM.Tests.Rents.Domain.Entities
{
    public interface IRentTests
    {
        void ItCreateNotLateRentSuccessfully();
        void ItCreateLateRentSuccessfully();
        void ItFailsToRentWithoutProducts();
        void ItFailsToRentUnavailableProducts();
        void ItRentProducts();
        void ItFinishRent();
        void ItAddDiscount();
        void ItAddDamageFee();

    }

    public class RentTests : IRentTests
    {
        [Fact]
        public void ItCreateNotLateRentSuccessfully()
        {
            // Arrange
            var rentPeriod = new DateRange(DateTime.Now, DateTime.Now.AddDays(10));
            var productsToRent = GetProductsToRent();
            var renter = new Renter();

            // Act
            var rent = new Rent(rentPeriod, productsToRent, renter);

            // Assert
            Assert.Equal(renter.Id, rent.RenterId);
            Assert.Equal(rentPeriod, rent.RentPeriod);
            Assert.Equal(30, rent.DailyPrice);
            Assert.Equal(15, rent.DailyLateFee);
            Assert.False(rent.WasProductDamaged);
            Assert.Equal(0, rent.DamageFee);
            Assert.Equal(0, rent.Discount);
            Assert.Equal(300, rent.CurrentRentPaymentValue);
            Assert.Equal(300, rent.PriceWithFees);
            Assert.Equal(300, rent.PriceWithDiscount);
            Assert.Equal(300, rent.PriceWithoutFees);
            Assert.Equal(300, rent.PriceWithDiscount);
            Assert.Equal(150, rent.AverageTicket);
            Assert.Equal(150, rent.AverageTicketWithDiscount);
            Assert.Equal(150, rent.AverageTicketWithoutFees);
            Assert.Equal(150, rent.AverageTicketWithoutFeesWithDiscount);
            Assert.Equal(2, rent.RentedProductsCount);
            Assert.Equal(10, rent.RentDays);
            Assert.Equal(0, rent.LateFee);
            Assert.True(rent.IsOpen);
            Assert.False(rent.IsClosed);
            Assert.False(rent.IsLate);
            Assert.Equal(0, rent.LateDays);
            Assert.False(rent.IsFinished);
        }

        private List<Product> GetProductsToRent()
        {
            var product1 = new Product
            {
                Status = ProductStatus.Available,
                RentDailyPrice = 10,
                RentDailyLateFee = 5
            };

            var product2 = new Product
            {
                Status = ProductStatus.Available,
                RentDailyPrice = 20,
                RentDailyLateFee = 10
            };

            var productsToRent = new List<Product>
            {
                product1,
                product2
            };

            return productsToRent;
        }

        [Fact]
        public void ItCreateLateRentSuccessfully()
        {
            // Arrange
            var rentPeriod = new DateRange(DateTime.Now.Date.AddDays(-11), DateTime.Now.Date.AddDays(-1));
            var productsToRent = GetProductsToRent();
            var renter = new Renter();

            // Act
            var rent = new Rent(rentPeriod, productsToRent, renter);

            // Assert
            Assert.Equal(renter.Id, rent.RenterId);
            Assert.Equal(rentPeriod, rent.RentPeriod);
            Assert.Equal(30, rent.DailyPrice);
            Assert.Equal(15, rent.DailyLateFee);
            Assert.False(rent.WasProductDamaged);
            Assert.Equal(0, rent.DamageFee);
            Assert.Equal(0, rent.Discount);
            Assert.Equal(315, rent.CurrentRentPaymentValue);
            Assert.Equal(315, rent.PriceWithFees);
            Assert.Equal(315, rent.PriceWithDiscount);
            Assert.Equal(300, rent.PriceWithoutFees);
            Assert.Equal(300, rent.PriceWithoutFeesWithDiscount);
            Assert.Equal(157.5M, rent.AverageTicket);
            Assert.Equal(157.5M, rent.AverageTicketWithDiscount);
            Assert.Equal(150, rent.AverageTicketWithoutFees);
            Assert.Equal(150, rent.AverageTicketWithoutFeesWithDiscount);
            Assert.Equal(2, rent.RentedProductsCount);
            Assert.Equal(11, rent.RentDays);
            Assert.Equal(15, rent.LateFee);
            Assert.True(rent.IsLate);
            Assert.Equal(1, rent.LateDays);
            Assert.True(rent.IsOpen);
            Assert.False(rent.IsClosed);
            Assert.False(rent.IsFinished);
        }

        [Fact]
        public void ItFailsToRentWithoutProducts()
        {
            // Arrange
            var rentPeriod = new DateRange(DateTime.Now.Date.AddDays(-11), DateTime.Now.Date.AddDays(-1));
            var productsToRent = new List<Product>();
            var renter = new Renter();

            // Act
            var exception = Assert.Throws<ValidationException>(() => new Rent(rentPeriod, productsToRent, renter));

            // Assert
            Assert.Equal("Trying to create a Rent without any Products", exception.Message);
        }

        [Fact]
        public void ItFailsToRentUnavailableProducts()
        {
            // Arrange
            var rentPeriod = new DateRange(DateTime.Now.Date.AddDays(-11), DateTime.Now.Date.AddDays(-1));
            var productsToRent = new List<Product>
            {
                new Product{Status = ProductStatus.Available},
                new Product{Status = ProductStatus.Unavailable}
            };
            var renter = new Renter();

            // Act
            var exception = Assert.Throws<ValidationException>(() => new Rent(rentPeriod, productsToRent, renter));

            // Assert
            Assert.Equal(productsToRent.GetProductsWithErrorMessage("Trying to rent unavailable products:", p => p.IsUnavailable), exception.Message);
        }

        [Fact]
        public void ItRentProducts()
        {
            // Arrange
            var rentPeriod = new DateRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            var productsToRent = new List<Product>
            {
                new Product{Status = ProductStatus.Available, RentDailyPrice = 10, RentDailyLateFee = 1}
            };
            var renter = new Renter();
            
            var rent = new Rent(rentPeriod, productsToRent, renter);

            // Act
            rent.RentProducts();

            // Assert
            Assert.True(rent.IsOpen);
        }

        [Fact]
        public void ItFinishRent()
        {
            // Arrange
            var rentPeriod = new DateRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            var productsToRent = new List<Product>
            {
                new Product{Status = ProductStatus.Available, RentDailyPrice = 10, RentDailyLateFee = 1}
            };
            var renter = new Renter();
            var rent = new Rent(rentPeriod, productsToRent, renter);
            
            // Act
            rent.FinishRent();

            // Assert
            Assert.True(rent.IsClosed);
        }

        [Fact]
        public void ItAddDiscount()
        {
            // Arrange
            var rentPeriod = new DateRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            var productsToRent = new List<Product>
            {
                new Product{Status = ProductStatus.Available, RentDailyPrice = 10, RentDailyLateFee = 1}
            };
            var renter = new Renter();
            var rent = new Rent(rentPeriod, productsToRent, renter);
            
            // Act
            rent.AddDiscount(5);

            
            // Assert
            Assert.Equal(5, rent.Discount);
        }

        [Fact]
        public void ItAddDamageFee()
        {
            // Arrange
            var rentPeriod = new DateRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            var productsToRent = new List<Product>
            {
                new Product{Status = ProductStatus.Available, RentDailyPrice = 10, RentDailyLateFee = 1}
            };
            var renter = new Renter();
            var rent = new Rent(rentPeriod, productsToRent, renter);
            
            // Act
            rent.AddDamageFee(5);

            
            // Assert
            Assert.Equal(5, rent.DamageFee);
            Assert.True(rent.WasProductDamaged);
        }
    }
}