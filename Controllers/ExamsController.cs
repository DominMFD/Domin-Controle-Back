using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/exams")]
public class ExamsController : ControllerBase {

  private IExamService service;

  public ExamsController(IExamService service) {
    this.service = service;
  }

  [HttpGet]
  public IActionResult ListAllExams(int page = 1) {
    var exams = service.ListAllExams(page);
    
    return StatusCode(200, exams);

  }

  [HttpPost]
  public IActionResult AddExam([FromBody] ExamDto examDto) {

    try {

      var exam = service.AddExam(examDto);
      return StatusCode(201, exam);

    } catch(ExamsError error) {
      return StatusCode(400, new ExamsError(error.Message));
    }
  }
}