
//Create application
var app = WebApplication.CreateBuilder(args).Build();
app.UseHttpsRedirection();


//----Dummy data will be respond from the endpoints--------

// Endpoint 1
app.MapGet("/employee/{id:int}", (int id) =>
{
    return new EmployeeRecord(id, "Kamal", "Perera");
});

// Endpoint 2
app.MapPost("/employee", (EmployeeRecord employee) =>
{
    return employee;
});


//---With Dependency Injection logics in place (IConfiguration) -------------
// Endpoint 3
app.MapGet("/person/{id:int}", (int id, IConfiguration config) =>
{
    return new PersonRecord(
        id, 
        config.GetValue<string>("TestInfo:FirstName")!,
        config.GetValue<string>("TestInfo:LastName")!);
});

// Endpoint 4
app.MapPost("/person", (PersonRecord person, IConfiguration config) =>
{
    int id = config.GetValue<int>("TestInfo:Id");
    var newPerson = person with { Id = id };
    return newPerson;
});

//Start
app.Run();


//Classes
record EmployeeRecord(int Id, string FirstName, string LastName);
record PersonRecord(int Id, string FirstName, string LastName);