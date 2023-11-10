﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using HttpClientBuilder.UnitTests.Utils;

namespace HttpClientBuilder.UnitTests.Results
{
    public class ResultExtensionTests
    {

        [Fact]
        public void HandleResult_ProvidesValue_WhenSuccess()
        {
            // Arrange
            var result = new RequestResult<WeatherForecast>(HttpStatusCode.OK, BogusWeatherData.GetForecast());

            // Act
            result.Handle(
                value: (code, forecast) =>
                {
                    // Assert
                    code.Should().Be(HttpStatusCode.OK);
                    forecast.Should().NotBeNull();
                },
                error: exception =>
                {
                    // Assert
                    exception.Should().BeNull();
                });
        } 
        
        [Fact]
        public void HandleResult_ProvidesException_WhenRequestFailed()
        {
            // Arrange
            var result = new RequestResult<WeatherForecast>(HttpStatusCode.BadRequest, new NullReferenceException());

            // Act
            result.Handle(
                value: (code, forecast) =>
                {
                    // Assert
                    code.Should().Be(HttpStatusCode.BadRequest);
                    forecast.Should().BeNull();
                },
                error: exception =>
                {
                    // Assert
                    exception.Should().NotBeNull();
                    exception.Should().BeAssignableTo<NullReferenceException>();
                });
        }

        [Fact]
        public void HandleResult_ProvidesException_WhenRequestThrew()
        {
            // Arrange
            var result = new RequestResult<WeatherForecast>(new NullReferenceException());

            // Act
            result.Handle(
                value: (code, forecast) =>
                {
                    // Assert
                    code.Should().Be(null);
                    forecast.Should().BeNull();
                },
                error: exception =>
                {
                    // Assert
                    exception.Should().NotBeNull();
                    exception.Should().BeAssignableTo<NullReferenceException>();
                });
        }

        [Fact]
        public void EnsureResult_InvokesPredicate_WhenSuccess()
        {
            // Arrange
            var result = new RequestResult<WeatherForecast>(HttpStatusCode.OK, BogusWeatherData.GetForecast());

            // Act
            var predicateResult = result.Ensure(
                predicate: forecast =>
                {
                    // Assert
                    forecast.Should().NotBeNull();

                    return true;
                });

            // Assert
            predicateResult.Success.Should().BeTrue();
        }

        [Fact]
        public void EnsureResult_FailsResult_WhenPredicateFailed()
        {
            // Arrange
            var result = new RequestResult<WeatherForecast>(HttpStatusCode.OK, BogusWeatherData.GetForecast());

            // Act
            var predicateResult = result.Ensure(
                predicate: forecast =>
                {
                    // Assert
                    forecast.Should().NotBeNull();
                    return false;
                });

            // Assert
            predicateResult.Success.Should().BeFalse();
        }
    }
}