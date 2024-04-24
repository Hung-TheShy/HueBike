using Core.Models.Base;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.Authen.FunctionAggregate;
using Infrastructure.AggregatesModel.Authen.PermissionAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeStationAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.MapLocationAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.StationAggregate;
using Infrastructure.AggregatesModel.MasterData.Notification;
using Infrastructure.AggregatesModel.MasterData.StatusAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.RateAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TicketAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TripAggregate;
using Infrastructure.AggregatesModel.MasterData.UnitAggregate;
using Infrastructure.AggregatesModel.MasterData.UserAggregate;
using Infrastructure.AggregatesModel.MasterData.UserAggregate.AuthenticationAggregate;
using Infrastructure.AggregatesModel.MasterData.UserAggregate.ComplainAggregate;
using Infrastructure.EntityConfigurations.Authen;
using Infrastructure.EntityConfigurations.Authen.AccountConfig;
using Infrastructure.EntityConfigurations.Authen.FunctionConfig;
using Infrastructure.EntityConfigurations.Authen.LogConfig;
using Infrastructure.EntityConfigurations.Authen.PermissionConfig;
using Infrastructure.EntityConfigurations.MasterData.BikeManagementConfig.BikeConfig;
using Infrastructure.EntityConfigurations.MasterData.BikeManagementConfig.BikeStationConfig;
using Infrastructure.EntityConfigurations.MasterData.BikeManagementConfig.MapLocationConfig;
using Infrastructure.EntityConfigurations.MasterData.LogConfig;
using Infrastructure.EntityConfigurations.MasterData.NotificationConfig;
using Infrastructure.EntityConfigurations.MasterData.StatusConfig;
using Infrastructure.EntityConfigurations.MasterData.TripManagementConfig.RateConfig;
using Infrastructure.EntityConfigurations.MasterData.TripManagementConfig.TicketConfig;
using Infrastructure.EntityConfigurations.MasterData.TripManagementConfig.TripConfig;
using Infrastructure.EntityConfigurations.MasterData.UnitConfig;
using Infrastructure.EntityConfigurations.MasterData.UserConfig;
using Infrastructure.EntityConfigurations.MasterData.UserConfig.ComplainConfig;
using Infrastructure.EntityConfigurations.MasterData.UserConfig.UserAuthenticationConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Core.Common.Constants;

namespace Infrastructure.EF
{
    public class BaseDbContext : DbContext
    {
        //public const string DEFAULT_SCHEMA = "public";
        private readonly AppModule _module;

        public BaseDbContext()
        {

        }
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {

        }

        public BaseDbContext(DbContextOptions<BaseDbContext> options, AppModule module) : base(options) => _module = module ?? new AppModule { Name = "N/A" };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Xử lý khi gặp lỗi Unable to create an object of type 'DbContext'. For the different patterns supported at design time
            // 2. Cấu hình
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();

                IConfiguration configuration = builder.Build();
                var connectionString = configuration.GetConnectionString(CONFIG_KEYS.APP_CONNECTION_STRING);
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        // AUTH
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserFunction> UserFunctions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionFunction> PermissionFunctions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        // MASTER DATA
        public DbSet<Unit> Units { get; set; }

        //User
        public DbSet<UserAuthentication> UserAuthentications { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<ComplainReply> ComplainReplys { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        //Status & notification
        public DbSet<Status> Status { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        //Bike
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<BikeLock> BikeLocks { get; set; }
        public DbSet<BikeStation> BikeStations { get; set; }
        public DbSet<MapLocation> MapLocations { get; set; }
        public DbSet<Station> Station { get; set; }

        //Trip
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CategoryTicket> CategoryTickets { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<RateReply> RateReplys { get; set; }


        // SYSTEM
        public DbSet<AggregatesModel.System.Module> SystemModules { get; set; }
        public DbSet<AggregatesModel.System.EmailServer> EmailServices { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.HasDefaultSchema(DEFAULT_SCHEMA);
            base.OnModelCreating(builder);

            // Cấu hình entities

            // AUTH
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserTokenConfiguration());
            builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            builder.ApplyConfiguration(new FunctionConfiguration());
            builder.ApplyConfiguration(new ModuleConfiguration());
            builder.ApplyConfiguration(new UserFunctionConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
            builder.ApplyConfiguration(new PermissionFunctionConfiguration());
            builder.ApplyConfiguration(new UserPermissionConfiguration());
            builder.ApplyConfiguration(new AuthenMediaConfiguration());
            builder.ApplyConfiguration(new AuthenticationLogConfiguration());

            // MASTER DATA
            builder.ApplyConfiguration(new UnitConfiguration());
            builder.ApplyConfiguration(new MasterDataLogConfiguration());

            //User
            builder.ApplyConfiguration(new UserAuthenticationConfiguration());
            builder.ApplyConfiguration(new ComplainConfiguration());
            builder.ApplyConfiguration(new ComplainReplyConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());

            //Status & Notification
            builder.ApplyConfiguration(new StatusConfiguration());
            builder.ApplyConfiguration(new NotificationConfiguration());
            builder.ApplyConfiguration(new UserNotificationConfiguration());

            //Bike management
            builder.ApplyConfiguration(new BikeConfiguration());
            builder.ApplyConfiguration(new BikeLockConfiguration());
            builder.ApplyConfiguration(new BikeStationConfiguration());
            builder.ApplyConfiguration(new MapLocationConfiguration());
            builder.ApplyConfiguration(new BikeStationConfiguration());

            //Trip management
            builder.ApplyConfiguration(new TripConfiguration());
            builder.ApplyConfiguration(new TicketConfiguration());
            builder.ApplyConfiguration(new CategoryTicketConfiguration());
            builder.ApplyConfiguration(new RateConfiguration());
            builder.ApplyConfiguration(new RateReplyConfiguration());

            // SYSTEM
            builder.ApplyConfiguration(new EntityConfigurations.System.ModuleConfiguration());
            builder.ApplyConfiguration(new EntityConfigurations.System.EmailServiceConfiguration());
        }
    }
}
