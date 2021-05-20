using System;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UdsideDownKittenGenerator.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    entityType.SetTableName(
            //        $"{entityType.GetTableName().Substring(0, 1).ToLowerInvariant()}{entityType.GetTableName()[1..]}");

            //    var tableName = entityType.GetTableName();

            //    if (string.IsNullOrEmpty(tableName))
            //        throw new Exception("TableName Empty");

            //    foreach (var property in entityType.GetDeclaredProperties())
            //        property.SetColumnName(
            //            $"{property.GetColumnName(StoreObjectIdentifier.Table(tableName, "dbo")).Substring(0, 1).ToLowerInvariant()}" +
            //            $"{property.GetColumnName(StoreObjectIdentifier.Table(tableName, "dbo"))[1..]}");
            //}
       // }
    }
}
