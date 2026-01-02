public class OxygenationService : IOxygenationService {

    private ControlContext context;

    public OxygenationService(ControlContext context) {
        this.context = context;
    }

    public List<OxygenationModel> ListAllOxygenation(
        string sortBy,
        string order,
        int page) {
        if(page < 1) page = 1;

        int limit = 10;
        int offset = (page - 1) * limit;

        if(sortBy.ToLower() == "date") {
            if(order.ToLower() == "desc") {
                return context.Oxygenation.OrderByDescending(e => e.Date).Skip(offset).Take(limit).ToList();
            }
        }

        if (sortBy.ToLower() == "time") {
            if (order.ToLower() == "desc") {
                return context.Oxygenation
                    .OrderByDescending(e => e.Date.TimeOfDay) 
                    .Skip(offset)
                    .Take(limit)
                    .ToList();
            }
        return context.Oxygenation
            .OrderBy(e => e.Date.TimeOfDay)
            .Skip(offset)
            .Take(limit)
            .ToList();
}

        if(sortBy.ToLower() == "oxy") {
            if(order.ToLower() == "desc") {
                return context.Oxygenation.OrderByDescending(e => e.Value).Skip(offset).Take(limit).ToList();
            }
            return context.Oxygenation.OrderBy(e => e.Value).Skip(offset).Take(limit).ToList();
        }

        return context.Oxygenation.OrderBy(e => e.Date).Skip(offset).Take(limit).ToList();
    }

    public OxygenationModel GetAOxygenation(long id) {
        var oxygenation = context.Oxygenation.Find(id);

        if (oxygenation == null) throw new OxygenationError("Oxygenation not found");

        return oxygenation;
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