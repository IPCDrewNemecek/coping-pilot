using MergingtonHighSchool.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure JSON serialization to use camelCase
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

// In-memory activity database
var activities = new Dictionary<string, Activity>
{
    ["Chess Club"] = new Activity
    {
        Description = "Learn strategies and compete in chess tournaments",
        Schedule = "Fridays, 3:30 PM - 5:00 PM",
        MaxParticipants = 12,
        Participants = new List<string> { "michael@mergington.edu", "daniel@mergington.edu" }
    },
    ["Programming Class"] = new Activity
    {
        Description = "Learn programming fundamentals and build software projects",
        Schedule = "Tuesdays and Thursdays, 3:30 PM - 4:30 PM",
        MaxParticipants = 20,
        Participants = new List<string> { "emma@mergington.edu", "sophia@mergington.edu" }
    },
    ["Gym Class"] = new Activity
    {
        Description = "Physical education and sports activities",
        Schedule = "Mondays, Wednesdays, Fridays, 2:00 PM - 3:00 PM",
        MaxParticipants = 30,
        Participants = new List<string> { "john@mergington.edu", "olivia@mergington.edu" }
    },
    // Sports-related activities
    ["Soccer Team"] = new Activity
    {
        Description = "Join the soccer team and compete in local tournaments",
        Schedule = "Tuesdays and Thursdays, 4:00 PM - 5:30 PM",
        MaxParticipants = 22,
        Participants = new List<string> { "liam@mergington.edu", "noah@mergington.edu" }
    },
    ["Basketball Practice"] = new Activity
    {
        Description = "Practice basketball skills and play friendly matches",
        Schedule = "Wednesdays, 3:30 PM - 5:00 PM",
        MaxParticipants = 15,
        Participants = new List<string> { "ava@mergington.edu", "mia@mergington.edu" }
    },
    // Artistic activities
    ["Art Workshop"] = new Activity
    {
        Description = "Explore painting, drawing, and other artistic techniques",
        Schedule = "Thursdays, 3:00 PM - 4:30 PM",
        MaxParticipants = 10,
        Participants = new List<string> { "amelia@mergington.edu", "harper@mergington.edu" }
    },
    ["Drama Club"] = new Activity
    {
        Description = "Participate in acting, scriptwriting, and stage performances",
        Schedule = "Mondays, 4:00 PM - 5:30 PM",
        MaxParticipants = 18,
        Participants = new List<string> { "elijah@mergington.edu", "james@mergington.edu" }
    },
    // Intellectual activities
    ["Mathletes"] = new Activity
    {
        Description = "Compete in math competitions and solve challenging problems",
        Schedule = "Fridays, 3:30 PM - 5:00 PM",
        MaxParticipants = 16,
        Participants = new List<string> { "charlotte@mergington.edu", "henry@mergington.edu" }
    },
    ["Debate Team"] = new Activity
    {
        Description = "Engage in debates and improve public speaking skills",
        Schedule = "Tuesdays, 4:00 PM - 5:30 PM",
        MaxParticipants = 12,
        Participants = new List<string> { "lucas@mergington.edu", "ella@mergington.edu" }
    }
};

// API Endpoints
app.MapGet("/api/activities", () => Results.Ok(activities))
    .WithName("GetActivities");

app.MapPost("/api/activities/{activityName}/signup", (string activityName, SignupRequest request) =>
{
    // Validate activity exists
    if (!activities.ContainsKey(activityName))
    {
        return Results.NotFound(new { detail = "Activity not found" });
    }

    var activity = activities[activityName];

    // Validate student is not already signed up
    if (activity.Participants.Contains(request.Email))
    {
        return Results.BadRequest(new { detail = "Student is already signed up for this activity" });
    }

    // Add student
    activity.Participants.Add(request.Email);
    return Results.Ok(new { message = $"Signed up {request.Email} for {activityName}" });
})
    .WithName("SignupForActivity");

// SPA fallback - serve index.html for client-side routes
app.MapFallbackToFile("index.html");

app.Run();
