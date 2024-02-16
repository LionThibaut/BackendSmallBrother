using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef.DbEntities;

public class BackSmallBrotherContext : DbContext
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public BackSmallBrotherContext(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public DbSet<DbClient> Clients { get; set; }
    public DbSet<DbAnimal> Animals { get; set; }
    public DbSet<DbPost> Posts { get; set; }
    public DbSet<DbReporting> Reportings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionStringProvider.Get("db"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbClient>(entity =>
        {
            entity.ToTable("client");
            entity.HasKey(c => c.IdClient);
            entity.Property(c => c.IdClient).HasColumnName("id_client");
            entity.Property(c => c.FirstName).HasColumnName("first_name");
            entity.Property(c => c.LastName).HasColumnName("last_name");
            entity.Property(c => c.Gender).HasColumnName("gender");
            entity.Property(c => c.Mail).HasColumnName("mail");
            entity.Property(c => c.HashedPassword).HasColumnName("hashed_password");
            entity.Property(c => c.PhoneNumber).HasColumnName("phone_number");
            entity.Property(c => c.Town).HasColumnName("town");
            entity.Property(c => c.RoleClient).HasColumnName("role_client");
            entity.Property(c => c.UrlImage).HasColumnName("url_image");
        });

        modelBuilder.Entity<DbAnimal>(entity =>
        {
            entity.ToTable("animal");
            entity.HasKey(a => a.IdAnimal);
            entity.Property(a => a.IdAnimal).HasColumnName("id_animal");
            entity.Property(a => a.NameAnimal).HasColumnName("name_animal");
            entity.Property(a => a.Breed).HasColumnName("breed");
            entity.Property(a => a.Tag).HasColumnName("tag");
            entity.Property(a => a.Birthdate).HasColumnName("birthdate");
            entity.Property(a => a.DescriptionAnimal).HasColumnName("description_animal");
            entity.Property(a => a.Height).HasColumnName("height");
            entity.Property(a => a.Gender).HasColumnName("gender");
            entity.Property(a => a.TypeAnimal).HasColumnName("type_animal");
            entity.Property(a => a.StatutAnimal).HasColumnName("statut_animal");
            entity.Property(a => a.UrlImage).HasColumnName("url_image");
            entity.Property(a => a.IdClient).HasColumnName("id_client");
        });

        modelBuilder.Entity<DbPost>(entity =>
        {
            entity.ToTable("post");
            entity.HasKey(p => p.IdPost);
            entity.Property(p => p.IdPost).HasColumnName("id_post");
            entity.Property(p => p.DatePost).HasColumnName("date_post");
            entity.Property(p => p.NbAlert).HasColumnName("nb_alert");
            entity.Property(p => p.TownDisparition).HasColumnName("town_disparition");
            entity.Property(p => p.DescriptionPost).HasColumnName("description_post");
            entity.Property(p => p.UrlImage).HasColumnName("url_image");
            entity.Property(p => p.IdAnimal).HasColumnName("id_animal");
        });

        modelBuilder.Entity<DbReporting>(entity =>
        {
            entity.ToTable("reporting");
            entity.HasKey(p => p.IdReporting);
            entity.Property(p => p.IdReporting).HasColumnName("id_reporting");
            entity.Property(p => p.Longitude).HasColumnName("longitude");
            entity.Property(p => p.Latitude).HasColumnName("latitude");
            entity.Property(p => p.DescriptionReporting).HasColumnName("description_reporting");
            entity.Property(p => p.LastSeenDate).HasColumnName("last_seen_date");
            entity.Property(p => p.LastSeenHour).HasColumnName("last_seen_hour");
            entity.Property(p => p.IdPost).HasColumnName("id_post");
        });
    }
}