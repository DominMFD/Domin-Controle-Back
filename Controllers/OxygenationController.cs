using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/oxygenation")]
public class OxygenationController : ControllerBase {

    private IOxygenationService service;

    public OxygenationController(IOxygenationService service) {
        this.service = service;
    }

    [HttpGet]
    public IActionResult ListAllOxygenation(
        [FromQuery] string sortBy = "date",
        [FromQuery] string order = "asc",
        int page = 1) {
        var oxygenation = service.ListAllOxygenation(sortBy, order, page);

        return StatusCode(200, oxygenation);
    }

    [HttpGet("{id}")]
    public IActionResult GetAOxygenation([FromRoute] long id) {
        try {

            var exam = service.GetAOxygenation(id);
            return StatusCode(200, exam);

        } catch (OxygenationError error) {
            return StatusCode(400, new OxygenationError(error.Message));
        }
    }

    [HttpPost]
    public IActionResult AddOxygenation([FromBody] OxygenationDto oxygenationDto) {
        try {

            var oxygenation = service.AddOxygenation(oxygenationDto);
            return StatusCode(201, oxygenation);

        } catch(OxygenationError error) {
            return StatusCode(400, new OxygenationError(error.Message));
        }
    }
 }