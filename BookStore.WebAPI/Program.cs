using Autofac;
using Autofac.Extensions.DependencyInjection;
using BookStore.Repository;
using BookStore.Repository.Common;
using BookStore.Service;
using BookStore.Service.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DEFAULT DI
// builder.Services.AddTransient<IAuthorService, AuthorService>();
// builder.Services.AddTransient<IBookService, BookService>();
// builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
// builder.Services.AddTransient<IBookRepository, BookRepository>();

// AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerDependency();
        builder.RegisterType<BookService>().As<IBookService>().InstancePerDependency();
        builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerDependency();
        builder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerDependency();
    }
);

var app = builder.Build();

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