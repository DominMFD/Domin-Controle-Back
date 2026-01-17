using Microsoft.EntityFrameworkCore;
using Google.Cloud.Storage.V1;

public class MedicineService : IMedicineService {
  private ControlContext context;
  private StorageClient storageClient;
  private const string BucketName = "domincontrole.appspot.com";

  public MedicineService(ControlContext context) {
    this.context = context;
    this.storageClient = StorageClient.Create();
  }

     public MedicineModel AddMedicine(MedicineDto medicineDto)
    {
        if (medicineDto.Image == null || medicineDto.Image.Length == 0) {
          throw new Exception("Imagem inválida");
        }
        
        var fileExtension = Path.GetExtension(medicineDto.Image.FileName);
        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        var objectName = $"medicines/{fileName}";

        using (var stream = medicineDto.Image.OpenReadStream())
        {
            storageClient.UploadObject(
                bucket: BucketName,
                objectName: objectName,
                contentType: medicineDto.Image.ContentType,
                source: stream
            );
        }

        var imageUrl = $"https://storage.googleapis.com/{BucketName}/{objectName}";

        var medicine = new MedicineModel
        {
            Name = medicineDto.Name,
            Dosage = medicineDto.Dosage,
            Description = medicineDto.Description,
            Image = imageUrl
        };

        context.Medicine.Add(medicine);
        context.SaveChanges();

        return medicine;
    }
}