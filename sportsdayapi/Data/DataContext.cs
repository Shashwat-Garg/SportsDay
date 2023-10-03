namespace sportsdayapi.Data
{
    using Microsoft.EntityFrameworkCore;
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// The database context necessary for the application.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options">Options for creating the db context</param>
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the db set containing information about users.
        /// </summary>
        public DbSet<User> Users { get; set; } = null;

        /// <summary>
        /// Gets or sets the db set containing information about events
        /// </summary>
        public DbSet<Event> Events { get; set; } = null;

        /// <summary>
        /// Gets or sets the db set containing information about user registered events
        /// </summary>
        public DbSet<UserEvent> UserEvents { get; set; } = null;

        /// <summary>
        /// Used to add keys and other model related properties when the db model is being created.
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEvent>()
                .HasKey(userEvent => new { userEvent.user_id, userEvent.event_id });

            modelBuilder.Entity<UserEvent>()
                .HasOne(userEvent => userEvent.user)
                .WithMany(user => user.user_events)
                .HasForeignKey(userEvent => userEvent.user_id);

            modelBuilder.Entity<UserEvent>()
                .HasOne(userEvent => userEvent.@event)
                .WithMany(evnt => evnt.user_events)
                .HasForeignKey(userEvent => userEvent.event_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
