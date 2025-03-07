var app = WebApplication.CreateBuilder(args).Build();
app.UseHttpsRedirection();


//Dummy data will be respond from the endpoints

// Endpoint 1
app.MapGet("/emplyee/{id:int}", (int id, IConfiguration config) =>
{
    return new PersonRecord(id, "Kamal", "Perera");
});

// Endpoint 2
app.MapGet("/person/{id:int}", (int id, IConfiguration config) =>
{
    return new PersonRecord(
        id, 
        config.GetValue<string>("TestInfo:FirstName")!,
        config.GetValue<string>("TestInfo:LastName")!);
});

// Endpoint 3
app.MapPost("/person", (PersonRecord person, IConfiguration config) =>
{
    int id = config.GetValue<int>("TestInfo:Id");
    var newPerson = person with { Id = id };
    return newPerson;
});

app.Run();

record PersonRecord(int Id, string FirstName, string LastName);