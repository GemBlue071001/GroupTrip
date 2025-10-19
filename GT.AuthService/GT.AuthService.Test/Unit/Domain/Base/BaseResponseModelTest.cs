using GT.AuthService.Domain.Base;
using GT.AuthService.Domain.Constant;
using Microsoft.AspNetCore.Http;
using Xunit;
using Assert = Xunit.Assert;

namespace GT.AuthService.Test.Unit.Domain.Base
{
    public class BaseResponseModelTests
    {
        [Fact]
        public void Constructor_WithAllParameters_ShouldSetProperties()
        {
            // Arrange
            var data = "Test Data";
            var additionalData = new { Extra = "Info" };
            var message = "Test Message";
            var statusCode = StatusCodes.Status200OK;
            var code = ResponseCodeConstants.SUCCESS;

            // Act
            var response = new BaseResponseModel<string>(statusCode, code, data, additionalData, message);

            // Assert
            Assert.Equal(data, response.Data);
            Assert.Equal(additionalData, response.AdditionalData);
            Assert.Equal(message, response.Message);
            Assert.Equal(statusCode, response.StatusCode);
            Assert.Equal(code, response.Code);
        }

        [Fact]
        public void OkResponseModel_ShouldReturnSuccessResponse()
        {
            // Arrange
            var data = "Success Data";
            var additionalData = new { Count = 10 };

            // Act
            var response = BaseResponseModel<string>.OkResponseModel(data, additionalData);

            // Assert
            Assert.Equal(data, response.Data);
            Assert.Equal(additionalData, response.AdditionalData);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(ResponseCodeConstants.SUCCESS, response.Code);
        }

        [Fact]
        public void NotFoundResponseModel_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var data = "Not Found Data";

            // Act
            var response = BaseResponseModel<string>.NotFoundResponseModel(data);

            // Assert
            Assert.Equal(data, response.Data);
            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
            Assert.Equal(ResponseCodeConstants.NOT_FOUND, response.Code);
        }

        [Fact]
        public void BadRequestResponseModel_ShouldReturnBadRequestResponse()
        {
            // Arrange
            var data = "Bad Request Data";

            // Act
            var response = BaseResponseModel<string>.BadRequestResponseModel(data);

            // Assert
            Assert.Equal(data, response.Data);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.Equal(ResponseCodeConstants.FAILED, response.Code);
        }

        [Fact]
        public void InternalErrorResponseModel_ShouldReturnInternalErrorResponse()
        {
            // Arrange
            var data = "Error Data";

            // Act
            var response = BaseResponseModel<string>.InternalErrorResponseModel(data);

            // Assert
            Assert.Equal(data, response.Data);
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
            Assert.Equal(ResponseCodeConstants.FAILED, response.Code);
        }
    }
}
