using Moq;
using Xunit;

public class OxygenationServiceTest {

    [Fact(DisplayName = "Add Oxygenation in DataBase")]
    public async Task AddOxygenationInDataBase() {
        var oxygenationDate = DateTime.Now;
        var oxygenationValue = 80;

        var oxygenationDto = new OxygenationDto{
            Date = oxygenationDate,
            Value = oxygenationValue,
        };

        await using var context = new MockDb().CreateDbContext();
        var oxygenationService = new OxygenationService(context);

        var result = oxygenationService.AddOxygenation(oxygenationDto);

        Assert.Collection(context.Oxygenation, oxygenation => {
            Assert.Equal(oxygenationDate, oxygenation.Date);
            Assert.Equal(oxygenationValue, oxygenation.Value);
        });
    }

    [Fact]
    public async Task GetAOxygenationInDataBase() {
        var oxygenationId = 1;
        var oxygenationDate = DateTime.Now;
        var oxygenationValue = 80;

        var oxygenationDto = new OxygenationDto{
            Date = oxygenationDate,
            Value = oxygenationValue,
        };

        await using var context = new MockDb().CreateDbContext();
        var oxygenationService = new OxygenationService(context);

        oxygenationService.AddOxygenation(oxygenationDto);

        var result = oxygenationService.GetAOxygenation(oxygenationId);

        Assert.Equal(result.Id, oxygenationId);
        Assert.Equal(result.Date, oxygenationDate);
        Assert.Equal(result.Value, oxygenationValue);
    }

    [Fact]
    public async Task GetAllOxygenationsInDataBase() {
        var sortBy = "date";
        var order = "asc";
        var page = 1;

        var oxygenationDate = DateTime.Now;
        var oxygenationValue = 80;

        var oxygenationDate2 = DateTime.Now;
        var oxygenationValue2 = 75;

        var oxygenationDto = new OxygenationDto{
            Date = oxygenationDate,
            Value = oxygenationValue
        };
        var oxygenationDto2 = new OxygenationDto{
            Date = oxygenationDate2,
            Value = oxygenationValue2
        };

        await using var context = new MockDb().CreateDbContext();
        var oxygenationService = new OxygenationService(context);

        var oxygenation1 = oxygenationService.AddOxygenation(oxygenationDto);
        var oxygenation2 = oxygenationService.AddOxygenation(oxygenationDto2);

        List<OxygenationModel> oxygenations = [oxygenation1, oxygenation2];

        var result = oxygenationService.ListAllOxygenation(sortBy, order, page);
        var ascValue = oxygenationService.ListAllOxygenation("oxy", "asc", 1);
        var descValue = oxygenationService.ListAllOxygenation("oxy", "desc", 1);

        Assert.Equal(2, result.Count);
        Assert.Equal(result, oxygenations);
        Assert.True(ascValue.SequenceEqual(oxygenations.OrderBy(e => e.Value)));
        Assert.True(descValue.SequenceEqual(oxygenations.OrderByDescending(e => e.Value)));
    }
}