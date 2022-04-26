using BLL.Entities;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ShopContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ShopContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Brands.Any())
            {
                context.Brands.AddRange(
                    new Brand
                    {
                        BrandName = "ELSEVE"
                    },
                    new Brand
                    {
                        BrandName = "L'Oreal Paris"
                    },
                    new Brand
                    {
                        BrandName = "Batiste"
                    },
                    new Brand
                    {
                        BrandName = "Davines"
                    }
                );
                context.SaveChanges();
            }

            if (!context.ProductTypes.Any())
            {
                context.ProductTypes.AddRange(
                    new ProductType
                    {
                        ProductTypeName = "Шампунь"
                    },
                    new ProductType
                    {
                        ProductTypeName = "Кондиционер"
                    },
                    new ProductType
                    {
                        ProductTypeName = "Сухой шампунь"
                    },
                    new ProductType
                    {
                        ProductTypeName = "Маска"
                    }
                );
                context.SaveChanges();
            }

            if (!context.HairTypes.Any())
            {
                context.HairTypes.AddRange(
                    new HairType { HairTypeName = "Для всех типов волос" },
                    new HairType { HairTypeName = "Для жирных волос" },
                    new HairType { HairTypeName = "Для непослушных волос" },
                    new HairType { HairTypeName = "Для окрашенных волос" },
                    new HairType { HairTypeName = "Для ослабленных волос" },
                    new HairType { HairTypeName = "Для придания объёма" },
                    new HairType { HairTypeName = "Для сухих и повреждённых волос" },
                    new HairType { HairTypeName = "Против перхоти" }
                );
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Name = "admin" },
                    new Role { Name = "user" }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Email = "admin@mir.ru", Password = "qwerty123$", RoleID = 1 },
                    new User { Email = "user@mir.ru", Password = "user123$", RoleID = 2 }
                );
                context.SaveChanges();
            }

            //if (!context.HairTypes.Any())
            //{
            //    context.HairTypes.AddRange(
            //        new HairType { HairTypeName = "Для всех типов волос" }
            //    );
            //    context.SaveChanges();
            //}
        }
    }
}
