public interface IMedicineService {
  MedicineModel AddMedicine(MedicineDto medicineDto);
  List<MedicineModel> ListAllMedicines();
}