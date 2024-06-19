using DynamicApplication.DOMAIN.Models;
using DynamicApplication.INFRA.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicApplication.INFRA;
public static class Seeder
{
    public static async Task Run(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider
            .GetRequiredService<CosmosDbContext>();

        if (!context.QuestionTypes.Any())
        {

            var questionTypes = new List<QuestionType>
            {
                new() { Id = 1, QuestionTypeName = "paragraph" },
                new() { Id = 2, QuestionTypeName = "dropdown" },
                new() { Id = 3, QuestionTypeName = "number" },
                new() { Id = 4, QuestionTypeName = "date" },
                new() { Id = 5, QuestionTypeName = "yesno" },
                new() { Id = 6, QuestionTypeName = "multiplechoice" },
            };
            
            await context.QuestionTypes.AddRangeAsync(questionTypes);
            await context.SaveChangesAsync();
        }
    }
}
