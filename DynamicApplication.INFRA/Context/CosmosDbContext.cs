using DynamicApplication.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DynamicApplication.INFRA.Context
{
    public class CosmosDbContext : DbContext
    {
        public CosmosDbContext(DbContextOptions options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAutoscaleThroughput(1000);

            modelBuilder.HasDefaultContainer("QuestionType");

            modelBuilder.Entity<Question>()
             .ToContainer(nameof(Question))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator();

            modelBuilder.Entity<QuestionType>()
             .ToContainer(nameof(QuestionType))
             .HasPartitionKey(o => o.Id)
             .HasNoDiscriminator();

            modelBuilder.Entity<Form>()
             .ToContainer(nameof(Form))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator();

            modelBuilder.Entity<Answer>()
             .ToContainer(nameof(Answer))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator();

            modelBuilder.Entity<MultipleChoiceAnswer>()
             .ToContainer(nameof(MultipleChoiceAnswer))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator();

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Form> Forms {  get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }

    }
}
