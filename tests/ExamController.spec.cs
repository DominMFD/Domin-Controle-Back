using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ExamControllerTest {

    Mock<IExamService> mockService = new Mock<IExamService>();

    [Fact]
    public void AddExamAndReturnStatusCode() {

        
        var examDto = new ExamDto 
        {
            Date = DateTime.Now,
            Hematocrito = 58.6,
            Rni = 1.7,
        };
        var examDto2 = new ExamDto 
        {
            Date = DateTime.Now,
            Rni = 1.7,
        };

        var examModel = new ExamModel{
            Id = 1,
            Date = DateTime.Now,
            Hematocrito = 58.6,
            Rni = 1.7,
        };

        var errorMessage = "Error adding exam";
        
        mockService.Setup(service => service.AddExam(examDto)).Returns(examModel);
        var controller = new ExamsController(mockService.Object);

        var result = controller.AddExam(examDto) as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(examModel, result.Value);

        mockService.Setup(service => service.AddExam(examDto2)).Throws(new ExamsError(errorMessage));

        result = controller.AddExam(examDto2) as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(errorMessage, (result.Value as ExamsError).Message);
    }

    [Fact]
    public void GetAExamAndReturnStatusCode() {

        var examModel = new ExamModel{
            Id = 1,
            Date = DateTime.Now,
            Hematocrito = 58.6,
            Rni = 1.7,
        };
        var examModel2 = new ExamModel{
            Id = 2,
            Date = DateTime.Now,
            Hematocrito = 59.6,
            Rni = 1.7,
        };
        var examDto = new ExamDto 
        {
            Date = DateTime.Now,
            Hematocrito = 58.6,
            Rni = 1.7,
        };

        var errorMessage = "Error get a exam";

        mockService.Setup(service => service.AddExam(examDto)).Returns(examModel);
         mockService.Setup(service => service.getAExam(1)).Returns(examModel);
        var controller = new ExamsController(mockService.Object);

        controller.AddExam(examDto);

        var result = controller.getAExame(1) as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(examModel, result.Value);

        mockService.Setup(service => service.getAExam(2)).Throws(new ExamsError(errorMessage));

        result = controller.getAExame(2) as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(errorMessage, (result.Value as ExamsError).Message);
    }

    [Fact]
    public void ListAllExamsAndReturnStatusCode() {
        var exams = new List<ExamModel>{
            new ExamModel() {
                Id = 1,
                Date = DateTime.Now,
                Hematocrito = 58.6,
                Rni = 1.7,
            },
            new ExamModel{
                Id = 2,
                Date = DateTime.Now,
                Hematocrito = 59.6,
                Rni = 1.7,
            }
        };
         var examDto = new ExamDto 
        {
            Date = DateTime.Now,
            Hematocrito = 58.6,
            Rni = 1.7,
        };
         var examDto2 = new ExamDto 
        {
            Date = DateTime.Now,
            Hematocrito = 59.6,
            Rni = 1.7,
        };

         mockService.Setup(service => service.AddExam(examDto));
         mockService.Setup(service => service.AddExam(examDto2));

        mockService.Setup(service => service.ListAllExams("date", "asc", 1)).Returns(exams);

        var controller = new ExamsController(mockService.Object);
        var result = controller.ListAllExams() as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(exams, result.Value);

    }
}