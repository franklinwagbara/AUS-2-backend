using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AUS2.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    TypeOfAppoinment = table.Column<string>(nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    AppointmentNote = table.Column<string>(nullable: true),
                    AppointmentVenue = table.Column<string>(nullable: true),
                    ScheduledBy = table.Column<string>(nullable: true),
                    ScheduledDate = table.Column<DateTime>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    LastApprovedCustDate = table.Column<DateTime>(nullable: true),
                    LastCustComment = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    SchduleExpiryDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisteredAddressId = table.Column<int>(nullable: true),
                    RegisteredAddress = table.Column<string>(nullable: true),
                    CacNumber = table.Column<string>(nullable: true),
                    TIN = table.Column<string>(nullable: true),
                    NationalityId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    YearIncorporated = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "flowStages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StateType = table.Column<string>(nullable: true),
                    Rate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flowStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InspectionForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    PlantManagerName = table.Column<string>(nullable: true),
                    FireFightingFacilities = table.Column<string>(nullable: true),
                    PowerSource = table.Column<string>(nullable: true),
                    AirCompressor = table.Column<string>(nullable: true),
                    FirstAidBox = table.Column<string>(nullable: true),
                    AdequateHouseKeeping = table.Column<string>(nullable: true),
                    ProvisionOfWater = table.Column<string>(nullable: true),
                    MechanicalLeakTester = table.Column<string>(nullable: true),
                    GasDetector = table.Column<string>(nullable: true),
                    MaintenanceFacilities = table.Column<string>(nullable: true),
                    AdequateSafetyFacilities = table.Column<string>(nullable: true),
                    ProtectiveWears = table.Column<string>(nullable: true),
                    SecurityCheckList = table.Column<string>(nullable: true),
                    EarthingProtectiveDevices = table.Column<string>(nullable: true),
                    EmergencyShutDownSystem = table.Column<string>(nullable: true),
                    PerimeterFence = table.Column<string>(nullable: true),
                    TrainingForOperators = table.Column<string>(nullable: true),
                    SupervisorComment = table.Column<string>(nullable: true),
                    LicenseType = table.Column<string>(nullable: true),
                    ApplicationType = table.Column<int>(nullable: false),
                    InspectionBy = table.Column<string>(nullable: true),
                    DistanceToPublicBuilding = table.Column<string>(nullable: true),
                    ProximityToAccessRoad = table.Column<string>(nullable: true),
                    ProximityToAdjourningFeatures = table.Column<string>(nullable: true),
                    SizeOfLand = table.Column<string>(nullable: true),
                    HighTensionRightofWay = table.Column<string>(nullable: true),
                    AccessibilityToSite = table.Column<string>(nullable: true),
                    InspectorComment = table.Column<string>(nullable: true),
                    AnnualCngcompression = table.Column<string>(nullable: true),
                    PipeLineRightofWays = table.Column<string>(nullable: true),
                    NbrLoadingPoint = table.Column<string>(nullable: true),
                    UploadedImage = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    ThirdPartyContactNamePhoneNo = table.Column<string>(nullable: true),
                    ThirdPartyCompanyName = table.Column<string>(nullable: true),
                    StorageCapacity = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissingDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    DocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissingDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutOfOffices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reliever = table.Column<string>(nullable: true),
                    Relieved = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutOfOffices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateCode = table.Column<string>(nullable: true),
                    StateName = table.Column<string>(nullable: true),
                    StateAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmittedDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    DocId = table.Column<int>(nullable: false),
                    DocSource = table.Column<string>(nullable: true),
                    DocType = table.Column<string>(nullable: true),
                    DocName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    UserType = table.Column<string>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    Client = table.Column<string>(nullable: true),
                    LoginTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    LoginMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    LicenseSerial = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phases_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldOffices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    FieldType = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOffices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldOffices_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGAs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(nullable: false),
                    LgaCode = table.Column<string>(nullable: true),
                    LgaName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGAs_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhaseDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatoeryId = table.Column<int>(nullable: false),
                    PhaseId = table.Column<int>(nullable: false),
                    ApptypeId = table.Column<int>(nullable: false),
                    DocId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DocType = table.Column<string>(nullable: true),
                    IsMandatory = table.Column<bool>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    SortId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhaseDocuments_ApplicationTypes_ApptypeId",
                        column: x => x.ApptypeId,
                        principalTable: "ApplicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhaseDocuments_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhaseDocuments_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    UserType = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    ElpsId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    LoginCount = table.Column<int>(nullable: true),
                    Signature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_FieldOffices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "FieldOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(nullable: false),
                    LgaId = table.Column<int>(nullable: false),
                    ElpsId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Cordinates = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_LGAs_LgaId",
                        column: x => x.LgaId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    TableName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    AffectedColumns = table.Column<string>(nullable: true),
                    PrimaryKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    PhaseId = table.Column<int>(nullable: false),
                    FacilityId = table.Column<int>(nullable: false),
                    ApplicationTypeId = table.Column<int>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    FlowStageId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    IsLegacy = table.Column<string>(nullable: true),
                    CurrentUser = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_ApplicationTypes_ApplicationTypeId",
                        column: x => x.ApplicationTypeId,
                        principalTable: "ApplicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_flowStages_FlowStageId",
                        column: x => x.FlowStageId,
                        principalTable: "flowStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    FieldLocationApply = table.Column<string>(nullable: true),
                    CurrentStageId = table.Column<short>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: true),
                    TriggeredBy = table.Column<string>(nullable: true),
                    TriggeredByRole = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    TargetedTo = table.Column<string>(nullable: true),
                    TargetedToRole = table.Column<string>(nullable: true),
                    NextStateId = table.Column<short>(nullable: true),
                    StatusMode = table.Column<string>(nullable: true),
                    ActionMode = table.Column<string>(nullable: true),
                    ApplicationRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppHistories_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationForm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    LandSize = table.Column<int>(nullable: true),
                    WellLocationCategory = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    Block = table.Column<string>(nullable: true),
                    Terrain = table.Column<string>(nullable: true),
                    WellSpudName = table.Column<string>(nullable: true),
                    WellPreSpudName = table.Column<string>(nullable: true),
                    WellSurfaceCoordinates = table.Column<string>(nullable: true),
                    WellClassApplied = table.Column<string>(nullable: true),
                    ProposedRig = table.Column<string>(nullable: true),
                    ExpectedVolumes = table.Column<string>(nullable: true),
                    TargetReserves = table.Column<string>(nullable: true),
                    Afe = table.Column<string>(nullable: true),
                    EstimatedOperationsDays = table.Column<int>(nullable: false),
                    WellName = table.Column<string>(nullable: true),
                    NatureOfOperation = table.Column<string>(nullable: true),
                    WellCompletionInterval = table.Column<string>(nullable: true),
                    RigForOperation = table.Column<string>(nullable: true),
                    PreOperationProductionRate = table.Column<string>(nullable: true),
                    PostOperationProductionRate1 = table.Column<string>(nullable: true),
                    InitialReservesAllocationOfWell = table.Column<string>(nullable: true),
                    CumulativeProductionForWell = table.Column<string>(nullable: true),
                    PlugbackInterval = table.Column<string>(nullable: true),
                    LastProductionRate = table.Column<string>(nullable: true),
                    SpudDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationForm_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtraPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rrreference = table.Column<string>(nullable: true),
                    AppReceiptId = table.Column<string>(nullable: true),
                    TxnAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Arrears = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankCode = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    TxnMessage = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    RetryCount = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    LastRetryDate = table.Column<DateTime>(nullable: true),
                    ExtraPaymentAppRef = table.Column<string>(nullable: true),
                    SanctionType = table.Column<string>(nullable: true),
                    ExtraPaymentBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraPayments_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Read = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    SenderId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    ApplicationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    UserType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rrreference = table.Column<string>(nullable: true),
                    AppReceiptId = table.Column<string>(nullable: true),
                    TxnAmount = table.Column<decimal>(nullable: true),
                    Arrears = table.Column<decimal>(nullable: false),
                    BankCode = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    TxnMessage = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    RetryCount = table.Column<int>(nullable: true),
                    LastRetryDate = table.Column<DateTime>(nullable: true),
                    ActionBy = table.Column<string>(nullable: true),
                    ServiceCharge = table.Column<decimal>(nullable: true),
                    ProcessingFee = table.Column<decimal>(nullable: true),
                    ApplicationRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    PermiTNo = table.Column<string>(nullable: true),
                    ElpsId = table.Column<int>(nullable: false),
                    IssuedDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    Printed = table.Column<bool>(nullable: false),
                    Signature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permits_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppHistories_ApplicationId",
                table: "AppHistories",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForm_ApplicationId",
                table: "ApplicationForm",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationTypeId",
                table: "Applications",
                column: "ApplicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_FacilityId",
                table: "Applications",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_FlowStageId",
                table: "Applications",
                column: "FlowStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PhaseId",
                table: "Applications",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OfficeId",
                table: "AspNetUsers",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraPayments_ApplicationId",
                table: "ExtraPayments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_LgaId",
                table: "Facilities",
                column: "LgaId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOffices_StateId",
                table: "FieldOffices",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_LGAs_StateId",
                table: "LGAs",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ApplicationId",
                table: "Messages",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_ApplicationId",
                table: "Permits",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseDocuments_ApptypeId",
                table: "PhaseDocuments",
                column: "ApptypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseDocuments_CategoryId",
                table: "PhaseDocuments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseDocuments_PhaseId",
                table: "PhaseDocuments",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_CategoryId",
                table: "Phases",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppHistories");

            migrationBuilder.DropTable(
                name: "ApplicationForm");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ExtraPayments");

            migrationBuilder.DropTable(
                name: "InspectionForms");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MissingDocuments");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "OutOfOffices");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Permits");

            migrationBuilder.DropTable(
                name: "PhaseDocuments");

            migrationBuilder.DropTable(
                name: "SubmittedDocuments");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "ApplicationTypes");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "flowStages");

            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LGAs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "FieldOffices");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
