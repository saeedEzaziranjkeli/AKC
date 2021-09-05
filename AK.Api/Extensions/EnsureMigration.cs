using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AK.Api.Extensions
{
    public static class EnsureMigration
    {
        public static Task EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            return Task.Run(async () =>
            {
                var context = app.ApplicationServices.GetService<T>();
                if(!await context.Database.EnsureCreatedAsync())
                    await context.Database.MigrateAsync();
            });

        }
    }
}
