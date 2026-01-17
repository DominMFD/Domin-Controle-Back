using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("medicine")]
public class MedicineController : ControllerBase {
  private IMedicineService service;

  public MedicineController(IMedicineService service) {
    this.service = service;
  }

  [HttpPost]
  public IActionResult AddMedicine([FromForm] MedicineDto medicineDto) {
    try {
      var medicine = service.AddMedicine(medicineDto);
      return StatusCode(201, medicine);
    } catch (MedicineError error) {
      return StatusCode(400, new MedicineError(error.Message));
    }
  }
}