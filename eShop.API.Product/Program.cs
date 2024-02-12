using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQL Server Service Registration
builder.Services.AddDbContext<EShopContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("EShop_SoloConnection")));

/**********
 ** CORS Cross-Origin Resource Sharing**
 **********/
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
        opt.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
    );
});

RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

RegisterEndpoints();

/************************
 ** CORS Configuration **
 ************************/
app.UseCors("CorsAllAccessPolicy");

app.Run();

void RegisterEndpoints()
{

    app.AddEndpoint<Product, ProductPostDTO, ProductPutDTO, ProductGetDTO>(); //can be for size color season ...
    app.AddEndpoint<ProductBrand, ProductBrandDTO>();
    app.AddEndpoint<Brand, BrandPostDTO, BrandPutDTO, BrandGetDTO>(); //can be for size color season ...
    app.AddEndpoint<ProductColor, ProductColorDTO>();
    app.AddEndpoint<Color, ColorPostDTO, ColorPutDTO, ColorGetDTO>();
    app.AddEndpoint<ProductSize, ProductSizeDTO>();
    app.AddEndpoint<Size, SizePostDTO, SizePutDTO, SizeGetDTO>();
    app.AddEndpoint<ProductSeason, ProductSeasonDTO>();
    app.AddEndpoint<Season, SeasonPostDTO, SeasonPutDTO, SeasonGetDTO>();
    app.MapGet($"/api/productsbycategory/" + "{categoryId}", async (IDbService db, int categoryId) =>
    {
        try
        {
            var result = await ((ProductDbService)db).GetProductsByCategoryAsync(categoryId);
            return Results.Ok(result);
        }
        catch
        {
        }

        return Results.BadRequest($"Couldn't get the requested products of type {typeof(Product).Name}.");
    });

    /*app.MapGet($"/api/categorieswithdata", async (IDbService db) =>
    {
        try
        {
            return Results.Ok(await ((CategoryDbService)db).GetCategoriesWithAllRelatedDataAsync());
        }
        catch
        {
        }

        return Results.BadRequest($"Couldn't get the requested products of type {typeof(Product).Name}.");
    });*/
}

void RegisterServices()
{
    ConfigureAutoMapper();
    builder.Services.AddScoped<IDbService, ProductDbService>();
}

void ConfigureAutoMapper()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Product, ProductPostDTO>().ReverseMap();
        cfg.CreateMap<Product, ProductPutDTO>().ReverseMap();
        cfg.CreateMap<Product, ProductGetDTO>().ReverseMap();
        cfg.CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
        //cfg.CreateMap<Product, ProductSmallGetDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorPostDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorPutDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorGetDTO>().ReverseMap();
        //cfg.CreateMap<Color, ColorSmallGetDTO>().ReverseMap();
        cfg.CreateMap<ProductColor, ProductColorDTO>().ReverseMap();
        cfg.CreateMap<Size, SizePostDTO>().ReverseMap();
        cfg.CreateMap<Size, SizePutDTO>().ReverseMap();
        cfg.CreateMap<Size, SizeGetDTO>().ReverseMap();
        //cfg.CreateMap<Size, SizeSmallGetDTO>().ReverseMap();
        cfg.CreateMap<ProductSize, ProductSizeDTO>().ReverseMap();
        cfg.CreateMap<Brand, BrandPostDTO>().ReverseMap();
        cfg.CreateMap<Brand, BrandPutDTO>().ReverseMap();
        cfg.CreateMap<Brand, BrandGetDTO>().ReverseMap();
        //cfg.CreateMap<Brand, BrandSmallGetDTO>().ReverseMap();
        cfg.CreateMap<ProductBrand, ProductBrandDTO>().ReverseMap();
        cfg.CreateMap<Season, SeasonPostDTO>().ReverseMap();
        cfg.CreateMap<Season, SeasonPutDTO>().ReverseMap();
        cfg.CreateMap<Season, SeasonGetDTO>().ReverseMap();
        //cfg.CreateMap<Season, SeasonSmallGetDTO>().ReverseMap();
        cfg.CreateMap<ProductSeason, ProductSeasonDTO>().ReverseMap();
        //cfg.CreateMap<Filter, FilterGetDTO>().ReverseMap();
        //cfg.CreateMap<Size, OptionDTO>().ReverseMap();
        //cfg.CreateMap<Color, OptionDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}

