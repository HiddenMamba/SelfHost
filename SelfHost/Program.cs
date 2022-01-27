using SelfHost.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ReceivedValuesDbContext>(o => o.UseInMemoryDatabase("NoteDB"), ServiceLifetime.Transient);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);



//Present existing values
app.MapGet("/", async (ReceivedValuesDbContext receivedValuesDbContext) =>
{
    return  Results.Ok(await receivedValuesDbContext.Notes.ToListAsync());
});

//Receive Value
app.MapPost("/SaveToFile", async (string receivedValue, ReceivedValuesDbContext receivedValuesDbContext) =>
{
    
    Note note = new Note();
    note.ReceivedNote = receivedValue;

    await semaphoreSlim.WaitAsync();
    try
    {
        receivedValuesDbContext.Notes.Add(note);
        receivedValuesDbContext.SaveChanges();
        using var writeFile = File.AppendText("receivedValues.txt");
        await writeFile.WriteLineAsync(receivedValue);
    }
    catch (Exception)
    {
        throw;
    }
    finally
    {
        semaphoreSlim.Release();
    }
});

app.Run();
