using System.Reflection;
using ThaiDuongWarehouse.Api.Applications.Mapping;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WarehouseDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnect"), b => 
    {
        b.MigrationsAssembly("ThaiDuongWarehouse.Api");
        b.EnableRetryOnFailure(2, TimeSpan.FromSeconds(4), null);
    });
    opt.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ILotAdjustmentRepository, LotAdjustmentRepository>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IItemLotRepository, ItemLotRepository>();
builder.Services.AddScoped<IInventoryLogEntryRepository, InventoryLogEntryRepository>();
builder.Services.AddScoped<IGoodsReceiptRepository, GoodsReceiptRepository>();
builder.Services.AddScoped<IGoodsIssueRepository, GoodsIssueRepository>();
builder.Services.AddScoped<IFinishedProductReceiptRepository, FinishedProductReceiptRepository>();
builder.Services.AddScoped<IFinishedProductInventoryRepository, FinishedProductInventoryRepository>();
builder.Services.AddScoped<IFinishedProductIssueRepository, FinishedProductIssueRepository>();
builder.Services.AddScoped<IItemClassRepository, ItemClassRepository>();
builder.Services.AddScoped<IIsolatedItemLotRepository, IsolatedItemLotRepository>();
builder.Services.AddScoped<IItemLotLocationRepository, ItemLotLocationRepository>();

builder.Services.AddScoped<IEmployeeQueries, EmployeeQueries>();
builder.Services.AddScoped<IItemQueries, ItemQueries>();
builder.Services.AddScoped<IDepartmentQueries, DepartmentQueries>();
builder.Services.AddScoped<ILotAdjustmentQueries, LotAdjustmentQueries>();
builder.Services.AddScoped<IWarehouseQueries, WarehouseQueries>();
builder.Services.AddScoped<IItemLotQueries, ItemLotQueries>();
builder.Services.AddScoped<IGoodsReceiptQueries, GoodsReceiptQueries>();
builder.Services.AddScoped<IInventoryLogEntryQueries, InventoryLogEntryQueries>();
builder.Services.AddScoped<IWarningQueries, WarningQueries>();
builder.Services.AddScoped<IGoodsIssueQueries, GoodsIssueQueries>();
builder.Services.AddScoped<IImportHistoryQueries, ImportHistoryQueries>();
builder.Services.AddScoped<IExportHistoryQueries, ExportHistoryQueries>();
builder.Services.AddScoped<IFinishedProductReceiptQueries, FinishedProductReceiptQueries>();
builder.Services.AddScoped<IFinishedProductIssueQueries, FinishedProductIssueQueries>();
builder.Services.AddScoped<IFinishedProductInventoryQueries, FinishedProductInventoryQueries>();

builder.Services.AddScoped<FinishedProductInventoryService>();
builder.Services.AddScoped<ItemLotLocationRepository>();
builder.Services.AddScoped<InventoryLogEntryService>();

builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMemoryCache();

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
