public class OxygenationService : IOxygenationService {

    private ControlContext context;

    public OxygenationService(ControlContext context) {
        this.context = context;
    }

    public List<OxygenationModel> ListAllOxygenation(int page) {
        if(page < 1) page = 1;

        int limit = 10;
        int offset = (page - 1) * limit;

        return context.Oxygenation.Skip(offset).Take(limit).ToList();
    }

    public OxygenationModel AddOxygenation(OxygenationDto oxygenationDto) {
        if(oxygenationDto.Date == DateTime.MinValue) throw new OxygenationError("Invalid Date");

        OxygenationModel oxygenation = new OxygenationModel{
            Date = oxygenationDto.Date,
            Value = oxygenationDto.Value,
        };

        context.Oxygenation.Add(oxygenation);
        context.SaveChanges();

        return oxygenation;
    }
}