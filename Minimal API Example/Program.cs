
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

List<Customer> customers = new List<Customer>();
customers.Add(new Customer(1, "Sabine", "Smith"));


app.MapGet("/", () => new Customer(1, "Peter", "Drucker"));
app.MapGet("/{id}", (int id) =>
{
    return customers.FirstOrDefault(x => x.Id == id);
});

app.MapPost("/", (Customer c) =>
{
    customers.Add(c);
    return $"New customer registered {c.FirstName} {c.LastName}";
});

app.MapPut("/", (HttpContext context, Customer c) =>
{
    var existingCustomer = customers.FirstOrDefault(x => x.Id == c.Id);
    if (existingCustomer != null)
    {
        existingCustomer.FirstName = c.FirstName;
        existingCustomer.LastName = c.LastName;

        return $"Customer edited {c.FirstName} {c.LastName}";
    }


    context.Response.StatusCode = 404;
    return "Customer not found";
});

app.MapDelete("/{id}", (HttpContext context, int id) =>
{
    var existingCustomer = customers.FirstOrDefault(x => x.Id == id);
    if (existingCustomer != null)
    {

        customers.Remove(existingCustomer);

        return $"Customer deleted {id}";
    }
    
    context.Response.StatusCode = 404;
    return "Customer not found";
});


await app.RunAsync();
