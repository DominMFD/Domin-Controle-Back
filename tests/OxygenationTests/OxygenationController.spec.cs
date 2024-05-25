using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class OxygenationControllerTest {
    Mock<IOxygenationService> mockService = new Mock<IOxygenationService>();

    [Fact]
    public void AddOxygenationAndReturnSuccessfulStatusCode() {
        var oxygenationDto = new OxygenationDto {
            Date = DateTime.Now,
            Value = 80,
        };

        var oxygenationModel = new OxygenationModel {
            Id = 1,
            Date = DateTime.Now,
            Value = 80,
        };

        mockService.Setup(service => service.AddOxygenation(oxygenationDto)).Returns(oxygenationModel);
        var controller = new OxygenationController(mockService.Object);

        var result = controller.AddOxygenation(oxygenationDto) as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(oxygenationModel, result.Value);
    }

    [Fact]
    public void AddOxygenationAndReturnErrorStatusCode() {
        var oxygenationDto = new OxygenationDto {
            Date = DateTime.Now,
        };

        var oxygenationModel = new OxygenationModel {
            Id = 1,
            Date = DateTime.Now,
            Value = 80,
        };

        var errorMessage = "Error adding oxygenation";

        mockService.Setup(service => service.AddOxygenation(oxygenationDto)).Throws(new OxygenationError(errorMessage));

        var controller = new OxygenationController(mockService.Object);

        var result = controller.AddOxygenation(oxygenationDto) as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(errorMessage, (result.Value as OxygenationError).Message);
    }

    [Fact]
    public void GetOxygenationAndReturnSuccessfulStatusCode() {
        var oxygenationModel = new OxygenationModel {
            Id = 1,
            Date = DateTime.Now,
            Value = 80,
        };

        var oxygenationDto = new OxygenationDto {
            Date = DateTime.Now,
            Value = 80,
        };

        mockService.Setup(service => service.AddOxygenation(oxygenationDto)).Returns(oxygenationModel);
        mockService.Setup(service => service.GetAOxygenation(1)).Returns(oxygenationModel);
        var controller = new OxygenationController(mockService.Object);

        controller.AddOxygenation(oxygenationDto);

        var result = controller.GetAOxygenation(1) as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(oxygenationModel, result.Value);   
    }  

    [Fact]
    public void GetOxygenationAndReturnErrorStatusCode() {
        var errorMessage = "Error get a oxygenation";

        mockService.Setup(service => service.GetAOxygenation(2)).Throws(new OxygenationError(errorMessage));
        var controller = new OxygenationController(mockService.Object);

        var result = controller.GetAOxygenation(2) as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(errorMessage, (result.Value as OxygenationError).Message);
    }

    [Fact]
    public void ListAllOxygenationAndReturnStatusCode() {
        var oxygenations = new List<OxygenationModel>{
            new OxygenationModel() {
                Id = 1,
                Date = DateTime.Now,
                Value = 80,
            },
            new OxygenationModel{
                Id = 2,
                Date = DateTime.Now,
                Value = 75,
            }
        };

        var oxygenationDto = new OxygenationDto{
            Date = DateTime.Now,
            Value = 80,
        };
        var oxygenationDto2 = new OxygenationDto{
            Date = DateTime.Now,
            Value = 75,
        };

        mockService.Setup(service => service.AddOxygenation(oxygenationDto));
        mockService.Setup(service => service.AddOxygenation(oxygenationDto2));

        mockService.Setup(service => service.ListAllOxygenation("date", "asc", 1)).Returns(oxygenations);

        var controller = new OxygenationController(mockService.Object);
        var result = controller.ListAllOxygenation() as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(oxygenations, result.Value);
    }
}