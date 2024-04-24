using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUTH_Log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 16383, nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", maxLength: 16383, nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", maxLength: 16383, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", maxLength: 16383, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_Media",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileNameUpload = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FilePathThumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_Module",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_Permission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BikeLock",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LockName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PathQr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Power = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeLock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTicket",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryTicketName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTicket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapLocation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MD_Log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 16383, nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", maxLength: 16383, nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", maxLength: 16383, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", maxLength: 16383, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MD_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MD_Unit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MD_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_EmailService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Host = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SenderName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DefaultCredentials = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_EmailService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_Module",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Segment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLog = table.Column<bool>(type: "bit", nullable: false),
                    StatusCodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_Function",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Permissions = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Function", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_Function_AUTH_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "AUTH_Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Point = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    AvatarId = table.Column<long>(type: "bigint", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsLock = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    AuthenId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_User_AUTH_Media_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "AUTH_Media",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AUTH_User_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bike",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BikeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocationId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    LockId = table.Column<long>(type: "bigint", maxLength: 20, nullable: true),
                    StatusId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bike_BikeLock_LockId",
                        column: x => x.LockId,
                        principalTable: "BikeLock",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bike_MapLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "MapLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bike_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityAvaiable = table.Column<int>(type: "int", nullable: false),
                    NumOfSeats = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Station_MapLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "MapLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Station_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_PermissionFunction",
                columns: table => new
                {
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    FunctionId = table.Column<long>(type: "bigint", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_PermissionFunction", x => new { x.PermissionId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK_AUTH_PermissionFunction_AUTH_Function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "AUTH_Function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AUTH_PermissionFunction_AUTH_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "AUTH_Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Transaction_AUTH_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserFunction",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    FunctionId = table.Column<long>(type: "bigint", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserFunction", x => new { x.UserId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK_AUTH_UserFunction_AUTH_Function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "AUTH_Function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AUTH_UserFunction_AUTH_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserPermission",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserPermission", x => new { x.PermissionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AUTH_UserPermission_AUTH_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "AUTH_Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AUTH_UserPermission_AUTH_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserRefreshToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RefreshExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserRefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_UserRefreshToken_AUTH_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Complain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SenderId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complain_AUTH_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAuthentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuthentication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAuthentication_AUTH_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    NotificationId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotification_AUTH_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNotification_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PathQr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingTime = table.Column<TimeSpan>(type: "time", maxLength: 255, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", maxLength: 255, nullable: false),
                    CategoryTicketId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BikeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_AUTH_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Bike_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bike",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_CategoryTicket_CategoryTicketId",
                        column: x => x.CategoryTicketId,
                        principalTable: "CategoryTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BikeStation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BikeId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    StationId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeStation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BikeStation_Bike_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bike",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BikeStation_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RefreshTokenId = table.Column<long>(type: "bigint", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_UserToken_AUTH_UserRefreshToken_RefreshTokenId",
                        column: x => x.RefreshTokenId,
                        principalTable: "AUTH_UserRefreshToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AUTH_UserToken_AUTH_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComplainReply",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ComplainId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    SenderId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplainReply_AUTH_User_Id",
                        column: x => x.Id,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplainReply_Complain_Id",
                        column: x => x.Id,
                        principalTable: "Complain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", maxLength: 50, nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    StationId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    TicketId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trip_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RateStar = table.Column<long>(type: "bigint", maxLength: 1, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TripId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    SenderId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rate_AUTH_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rate_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateReply",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RateId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    SenderId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RateReply_AUTH_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AUTH_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateReply_Rate_RateId",
                        column: x => x.RateId,
                        principalTable: "Rate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Transaction_UserId",
                table: "_Transaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Function_ControllerName",
                table: "AUTH_Function",
                column: "ControllerName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Function_ModuleId",
                table: "AUTH_Function",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_PermissionFunction_FunctionId",
                table: "AUTH_PermissionFunction",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_PermissionFunction_PermissionId",
                table: "AUTH_PermissionFunction",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_User_AvatarId",
                table: "AUTH_User",
                column: "AvatarId",
                unique: true,
                filter: "[AvatarId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_User_StatusId",
                table: "AUTH_User",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_User_UserName",
                table: "AUTH_User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserFunction_FunctionId",
                table: "AUTH_UserFunction",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserFunction_UserId",
                table: "AUTH_UserFunction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserPermission_PermissionId",
                table: "AUTH_UserPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserPermission_UserId",
                table: "AUTH_UserPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserRefreshToken_UserId",
                table: "AUTH_UserRefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserToken_RefreshTokenId",
                table: "AUTH_UserToken",
                column: "RefreshTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserToken_UserId",
                table: "AUTH_UserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bike_LocationId",
                table: "Bike",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bike_LockId",
                table: "Bike",
                column: "LockId",
                unique: true,
                filter: "[LockId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bike_StatusId",
                table: "Bike",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BikeLock_PathQr",
                table: "BikeLock",
                column: "PathQr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BikeStation_BikeId",
                table: "BikeStation",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_BikeStation_StationId",
                table: "BikeStation",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Complain_SenderId",
                table: "Complain",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MD_Unit_Code",
                table: "MD_Unit",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rate_SenderId",
                table: "Rate",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_TripId",
                table: "Rate",
                column: "TripId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RateReply_RateId",
                table: "RateReply",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_RateReply_SenderId",
                table: "RateReply",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_LocationId",
                table: "Station",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Station_StatusId",
                table: "Station",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Module_Name",
                table: "SYS_Module",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Module_Segment",
                table: "SYS_Module",
                column: "Segment",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_BikeId",
                table: "Ticket",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CategoryTicketId",
                table: "Ticket",
                column: "CategoryTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PathQr",
                table: "Ticket",
                column: "PathQr",
                unique: true,
                filter: "[PathQr] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_StationId",
                table: "Trip",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TicketId",
                table: "Trip",
                column: "TicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthentication_CardId",
                table: "UserAuthentication",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthentication_UserId",
                table: "UserAuthentication",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification_NotificationId",
                table: "UserNotification",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification_UserId",
                table: "UserNotification",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Transaction");

            migrationBuilder.DropTable(
                name: "AUTH_Log");

            migrationBuilder.DropTable(
                name: "AUTH_PermissionFunction");

            migrationBuilder.DropTable(
                name: "AUTH_UserFunction");

            migrationBuilder.DropTable(
                name: "AUTH_UserPermission");

            migrationBuilder.DropTable(
                name: "AUTH_UserToken");

            migrationBuilder.DropTable(
                name: "BikeStation");

            migrationBuilder.DropTable(
                name: "ComplainReply");

            migrationBuilder.DropTable(
                name: "MD_Log");

            migrationBuilder.DropTable(
                name: "MD_Unit");

            migrationBuilder.DropTable(
                name: "RateReply");

            migrationBuilder.DropTable(
                name: "SYS_EmailService");

            migrationBuilder.DropTable(
                name: "SYS_Module");

            migrationBuilder.DropTable(
                name: "UserAuthentication");

            migrationBuilder.DropTable(
                name: "UserNotification");

            migrationBuilder.DropTable(
                name: "AUTH_Function");

            migrationBuilder.DropTable(
                name: "AUTH_Permission");

            migrationBuilder.DropTable(
                name: "AUTH_UserRefreshToken");

            migrationBuilder.DropTable(
                name: "Complain");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "AUTH_Module");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "Station");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "AUTH_User");

            migrationBuilder.DropTable(
                name: "Bike");

            migrationBuilder.DropTable(
                name: "CategoryTicket");

            migrationBuilder.DropTable(
                name: "AUTH_Media");

            migrationBuilder.DropTable(
                name: "BikeLock");

            migrationBuilder.DropTable(
                name: "MapLocation");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
