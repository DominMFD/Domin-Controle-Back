using Microsoft.EntityFrameworkCore;

public class MockDb : IDbContextFactory<ControlContext>
{
    public ControlContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ControlContext>()
            .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

        return new ControlContext(options);
    }
}