using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("exams")]
public class ExamsController : ControllerBase {

  private IExamService service;

  public ExamsController(IExamService service) {
    this.service = service;
  }

  [HttpGet]
  public IActionResult ListAllExams(
    [FromQuery] string sortBy = "date",
    [FromQuery] string order = "asc",
    int page = 1) {
    var exams = service.ListAllExams(sortBy, order, page);
    
    return StatusCode(200, exams);

  }

  [HttpGet("{id}")]
  public IActionResult GetAExame([FromRoute] long id) {
    try {

      var exam = service.GetAExam(id);
      return StatusCode(200, exam);

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

  [HttpDelete("{id}")]
  public IActionResult DeleteExam([FromRoute] long id) {
    try {
      service.DeleteExam(id);
      return NoContent();

    } catch (ExamsError error) {
      return NotFound(new { message = error.Message });
    }
  }
}