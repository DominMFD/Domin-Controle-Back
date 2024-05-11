public interface IOxygenationService {

    List<OxygenationModel> ListAllOxygenation(int page);
    OxygenationModel AddOxygenation(OxygenationDto oxygenationDto);
}