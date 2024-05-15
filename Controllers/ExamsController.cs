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

  [HttpGet("{id}")]
  public IActionResult getAExame([FromRoute] long id) {
    try {

      var exam = service.getAExam(id);
      return StatusCode(200, exam);

    } catch (ExamsError error) {
      return StatusCode(400, new ExamsError(error.Message));
    }
  }

  [HttpGet("/order")]
  public IActionResult getAllExamsInAlphabeticalOrder(int page = 1) {
    try {

      var exams = service.getAllExamsInAlphabeticalOrder(page);
      return StatusCode(200, exams);

    } catch (ExamsError error) {
      return StatusCode(400, new ExamsError(error.Message));
    } 
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