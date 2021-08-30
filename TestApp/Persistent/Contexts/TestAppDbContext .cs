using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestApp.Core.Entities.Identity;
using TestApp.Core.Entities.Lookup;
using TestApp.Core.Interfaces;
using TestApp.Extentions;

namespace TestApp.Persistent.Contexts
{
    public class TestAppDbContext : IdentityDbContext<ApplicationUser> 
    {
        public DbSet<LookupType> LookupTypes { set; get; }
        public DbSet<LookupType> LookupItems { set; get; }

        public TestAppDbContext(DbContextOptions<TestAppDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            FluentApi(builder);
            SeedData(builder);
            GlobalFilters(builder);

        }

        private void GlobalFilters(ModelBuilder builder)
        {
            builder.SetQueryFilterOnAllEntities<ISoftDelete>(w=>!w.IsDeleted);
        }


        private void FluentApi(ModelBuilder builder)
        {


        }

        private void SeedData(ModelBuilder builder)
        {
            var hash = new PasswordHasher<ApplicationUser>();
            var ADMIN_ID = Guid.NewGuid().ToString();
            var ROLE_ID = Guid.NewGuid().ToString();
            var ROLE_ID_2 = Guid.NewGuid().ToString();
            var ROLE_ID_3 = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole[]
                {
                    new IdentityRole {  Id = ROLE_ID, Name = "superadmin", NormalizedName = "superadmin".ToUpper() } ,
                    new IdentityRole {  Id = ROLE_ID_2, Name = "admin", NormalizedName = "admin".ToUpper() } ,
                    new IdentityRole {  Id = ROLE_ID_3, Name = "user", NormalizedName = "user".ToUpper() } ,


                });

            var ar  = new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "aaltair.developer@gmail.com",
                NormalizedEmail = "aaltair.developer@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+962788260020",
                PhoneNumberConfirmed = true,
                FirstName = "Alaa1",
                SecondName = "Abbas1",
                LastName = "Altair1",
                BirthDate = new DateTime(1993, 1, 27),
                PasswordHash = hash.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = String.Empty,
                CreatedOn = DateTime.Now
            };
            JObject obj = new JObject();
            obj.Add("ar", JObject.Parse(JsonConvert.SerializeObject(ar)));
            var en = new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "aaltair.developer@gmail.com",
                NormalizedEmail = "aaltair.developer@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+962788260020",
                PhoneNumberConfirmed = true,
                FirstName = "Alaa",
                SecondName = "Abbas",
                LastName = "Altair",
                BirthDate = new DateTime(1993, 1, 27),
                PasswordHash = hash.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = String.Empty,
                CreatedOn = DateTime.Now,
            };

            obj.Add("en", JObject.Parse(JsonConvert.SerializeObject(en)));
            en.Translate = obj.ToString();
            builder.Entity<ApplicationUser>().HasData(en);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

        }

    }
    
            
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestAppDbContext>
    {
        public TestAppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<TestAppDbContext>();
            var connectionString = configuration.GetConnectionString("TestAppConnection");
            builder.UseSqlServer(connectionString);
            return new TestAppDbContext(builder.Options);
        }
    }

}