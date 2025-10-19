using GT.AuthService.Domain.Base;
using GT.AuthService.Domain.Constant;
using Microsoft.AspNetCore.Http;
using Xunit;
using Assert = Xunit.Assert;

namespace GT.AuthService.Test.Unit.Domain.Base
{
    public class BaseResponseTests
    {
        [Fact]
        public void Constructor_WithAllParameters_ShouldSetProperties()
        {
            // Arrange
            var testData = "Test Data";
            var message = "Test Message";
            var statusCode = StatusCodeHelper.OK;
            var code = "TEST_CODE";

            // Act
            var response = new BaseResponse<string>(statusCode, code, testData, message);

            // Assert
            Assert.Equal(testData, response.Data);
            Assert.Equal(message, response.Message);
            Assert.Equal(statusCode, response.StatusCode);
            Assert.Equal(code, response.Code);
        }

        // [Fact]
        // public void OkResponse_WithData_ShouldReturnSuccessResponse()
        // {
        //     // Arrange
        //     var testData = "Success Data";
        //
        //     // Act
        //     var response = BaseResponse<string>.OkResponse(testData);
        //
        //     // Assert
        //     Assert.Equal(testData, response.Data);
        //     Assert.Equal(StatusCodeHelper.OK, response.StatusCode);
        //     Assert.Equal("Success Data", response.Code);
        // }

        [Fact]
        public void OkResponse_WithMessage_ShouldReturnSuccessResponse()
        {
            // Arrange
            var message = "Operation successful";

            // Act
            var response = BaseResponse<string>.OkResponse(message);

            // Assert
            Assert.Equal(message, response.Message);
            Assert.Equal(StatusCodeHelper.OK, response.StatusCode);
            Assert.Equal("Success", response.Code);
        }
    }
}
