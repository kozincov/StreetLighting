using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationLighting.Data
{
    public class DbInitializer
    {
        public static void Initialize(LightingContext context)
        {
            context.Database.EnsureCreated();


            

            Random randObj = new Random(1);

            string StreetName;
            string[] streetName = { "Гагарина", "Набережная", "Школьная", "Сухого", "Фестивальная", "Совецкая", "Ковалева", "Снежкова", "Молодежная", "Речицкое шоссе", "Проспект октября", "Ленина", "Паново", "Гомельская", "Илича" };
            if (!context.Streets.Any())
            {


                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[0]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[1]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[2]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[3]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[4]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[5]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[6]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[7]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[8]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[9]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[10]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[11]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[12]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[13]
                    });
                context.Streets.Add(
                    new Streets()
                    {
                        StreetName = streetName[14]
                    });
                context.SaveChanges();

            }
            if (!context.Lamps.Any())
            {
                context.Lamps.Add(new Lamps { LampName = "Лампа1", LampType = "Торшерный", LifeTime = 6, Power = 12 });
                context.Lamps.Add(new Lamps { LampName = "Лампа2", LampType = "Подвесной", LifeTime = 12, Power = 34 });
                context.Lamps.Add(new Lamps { LampName = "Лампа3", LampType = "Встраиваемый", LifeTime = 24, Power = 8 });
                context.Lamps.Add(new Lamps { LampName = "Лампа4", LampType = "Торшерный", LifeTime = 6, Power = 12 });
                context.Lamps.Add(new Lamps { LampName = "Лампа5", LampType = "Настенный", LifeTime = 48, Power = 36 });
                context.SaveChanges();
            }
            if (!context.Lanterns.Any())
            {
                context.Lanterns.Add(new Lanterns { LanternName = "Светильник1", LanternType = "Бра" });
                context.Lanterns.Add(new Lanterns { LanternName = "Светильник2", LanternType = "Встраиваемый" });
                context.Lanterns.Add(new Lanterns { LanternName = "Светильник3", LanternType = "Торшер" });
                context.Lanterns.Add(new Lanterns { LanternName = "Светильник4", LanternType = "Галагенка" });
                context.Lanterns.Add(new Lanterns { LanternName = "Светильник5", LanternType = "Бра" });
                context.SaveChanges();
            }
            List<string> sections = new List<string>() {"Квадрат","Чикаго","Болотка","Центральный","Заподный","Железнодорожный","Волотова","Новобелица","Пятый","Козинцовский","Батькавский","Секретный","Мусорской","Гопнинский","Айтишнитский" };
            if (!context.Sections.Any())
            {
                for (int i=1;i<=100;i++)
                {
                    context.Sections.Add(new Sections { BeginAndEnd = new Random().Next(10,200), SectionName = sections[new Random().Next(0,15)], StreetId = new Random().Next(34, 48) });
                }
                context.SaveChanges();
            }
            if (!context.StreetLightings.Any())
            {
                for (int i = 1; i <= 100; i++)
                {
                    context.StreetLightings.Add(new StreetLightings { CountLantern = new Random().Next(5, 15), Failure = DateTime.Parse("01/01/201" + new Random().Next(1, 9)), LampId = new Random().Next(12, 16), LanternId = new Random().Next(1, 5), SectionId = new Random().Next(5, 104) });
                }
                context.SaveChanges();
            }

        }

    }
}
