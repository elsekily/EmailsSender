using EmailSender.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Persistence;
public class EmailSenderDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }
    public EmailSenderDbContext(DbContextOptions<EmailSenderDbContext> options)
    : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Message>(eb =>
        {
            eb.HasKey(e => e.Id);

            eb.Property(e => e.Subject)
               .IsRequired()
               .HasMaxLength(255);

            eb.Property(e => e.Body)
                   .IsRequired()
                   .HasColumnType("text");
        });
    }
}