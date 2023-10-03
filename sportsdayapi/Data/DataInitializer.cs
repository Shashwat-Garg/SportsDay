namespace sportsdayapi.Data
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// Used to initialize the data in the development environment
    /// </summary>
    public class DataInitializer
    {
        /// <summary>
        /// Initializes data in the database in development environment
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public static void Initialize(DataContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            // Data is already present, nothing to do
            if (dbContext.Events.Any())
            {
                return;
            }

            dbContext.Events.AddRange(
                new Event[]
                {
                    new Event
                    {
                        id = 1,
                        event_name = "Butterfly 100M",
                        event_category = "Swimming",
                        start_time = DateTime.Parse("2022-12-17 13:00:00"),
                        end_time = DateTime.Parse("2022-12-17 14:00:00")
                    },
                    new Event
                    {
                        id = 2,
                        event_name = "Backstroke 100M",
                        event_category = "Swimming",
                        start_time = DateTime.Parse("2022-12-17 13:30:00"),
                        end_time = DateTime.Parse("2022-12-17 14:30:00")
                    },
                    new Event
                    {
                        id = 3,
                        event_name = "Freestyle 400M",
                        event_category = "Swimming",
                        start_time = DateTime.Parse("2022-12-17 15:00:00"),
                        end_time = DateTime.Parse("2022-12-17 16:00:00")
                    },
                    new Event
                    {
                        id = 4,
                        event_name = "High Jump",
                        event_category = "Athletics",
                        start_time = DateTime.Parse("2022-12-17 13:00:00"),
                        end_time = DateTime.Parse("2022-12-17 14:00:00")
                    },
                    new Event
                    {
                        id = 5,
                        event_name = "Triple Jump",
                        event_category = "Athletics",
                        start_time = DateTime.Parse("2022-12-17 16:00:00"),
                        end_time = DateTime.Parse("2022-12-17 17:00:00")
                    },
                    new Event
                    {
                        id = 6,
                        event_name = "Long Jump",
                        event_category = "Athletics",
                        start_time = DateTime.Parse("2022-12-17 17:00:00"),
                        end_time = DateTime.Parse("2022-12-17 18:00:00")
                    },
                    new Event
                    {
                        id = 7,
                        event_name = "100M Sprint",
                        event_category = "Athletics",
                        start_time = DateTime.Parse("2022-12-17 17:00:00"),
                        end_time = DateTime.Parse("2022-12-17 18:00:00")
                    },
                    new Event
                    {
                        id = 8,
                        event_name = "Lightweight 60kg",
                        event_category = "Boxing",
                        start_time = DateTime.Parse("2022-12-17 18:00:00"),
                        end_time = DateTime.Parse("2022-12-17 19:00:00")
                    },
                    new Event
                    {
                        id = 9,
                        event_name = "Middleweight 75 kg",
                        event_category = "Boxing",
                        start_time = DateTime.Parse("2022-12-17 19:00:00"),
                        end_time = DateTime.Parse("2022-12-17 20:00:00")
                    },
                    new Event
                    {
                        id = 10,
                        event_name = "Heavyweight 91kg",
                        event_category = "Boxing",
                        start_time = DateTime.Parse("2022-12-17 20:00:00"),
                        end_time = DateTime.Parse("2022-12-17 22:00:00")
                    }
                });

            dbContext.SaveChanges();
        }
    }
}
