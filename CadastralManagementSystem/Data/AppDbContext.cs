using Microsoft.EntityFrameworkCore;

public class Property
{
    public int Id { get; set; }
    public string? Address { get; set; }
    public string? CadastralNumber { get; set; }
    public string? Description { get; set; }
    public double Area { get; set; }
    public string? Owner { get; set; }
}

public class Proposal
{
    public int Id { get; set; }
    public string? Address { get; set; }
    public string? CadastralNumber { get; set; }
    public string? Description { get; set; }
    public double Area { get; set; }
    public string? Owner { get; set; }
}


public class AppDbContext : DbContext
{
    public DbSet<Property>? Properties { get; set; }
    public DbSet<Proposal>? Proposals { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
