public interface IOxygenationService {

    List<OxygenationModel> ListAllOxygenation(string sortBy, string order,int page);
    OxygenationModel GetAOxygenation(long id);
    OxygenationModel AddOxygenation(OxygenationDto oxygenationDto);
    void DeleteOxygenation(long id);
}