using WorkbenchTimeTracker.Server.Domain;

namespace WorkbenchTimeTracker.Server.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Seed(TimeTrackerDbContext db)
        {
            // Only seed if there are no data
            if (db.People.Any() || db.Tasks.Any())
                return;

            // --- People ---
            var paul = Person.Create("Paul Atreides");
            var jessica = Person.Create("Lady Jessica");
            var duncan = Person.Create("Duncan Idaho");
            var gurney = Person.Create("Gurney Halleck");
            var chani = Person.Create("Chani");
            var baron = Person.Create("Baron Vladimir Harkonnen");
            var thufir = Person.Create("Thufir Hawat");
            var stilgar = Person.Create("Stilgar");
            var alia = Person.Create("Alia Atreides");
            var irulan = Person.Create("Princess Irulan");

            var people = new[]
            {
                paul, jessica, duncan, gurney, chani, baron, thufir, stilgar, alia, irulan
            };

            db.People.AddRange(people);

            // --- Tasks ---
            var paulVisions = Domain.Task.Create(
                title: "Face the Visions of Jihad",
                description: "Embrace or resist the terrifying visions of a galaxy-wide jihad.",
                assignee: paul
            );
            var aliaPrescience = Domain.Task.Create(
                title: "Test Limits of Prescience",
                description: "Explore the boundaries of prophetic visions at personal risk.",
                assignee: alia
            );
            var gurneyBallads = Domain.Task.Create(
                title: "Compose War Ballads",
                description: "Write inspiring ballads to rally warriors for imminent battle.",
                assignee: gurney
            );
            var sietch = Domain.Task.Create(
                title: "Sietch Defense Inspection",
                description: "Secure the perimeter and assess vulnerabilities to Harkonnen spies."
            );

            var tasks = new List<Domain.Task>
            {
                sietch,
                paulVisions,
                aliaPrescience,
                gurneyBallads,
                Domain.Task.Create(
                    title: "Ride Shai-Hulud",
                    description: "Undertake a sacred and perilous journey atop a giant sandworm.",
                    assignee: paul
                ),
                Domain.Task.Create(
                    title: "Bene Gesserit Ritual Training",
                    description: "Train initiates in mind-body control and Truthsaying rituals.",
                    assignee: jessica
                ),
                Domain.Task.Create(
                    title: "Scout Hidden Spice Deposits",
                    description: "Discover and catalog unknown spice caches deep in the desert.",
                    assignee: chani
                ),
                Domain.Task.Create(
                    title: "Plot Assassination of House Atreides Heir",
                    description: "Arrange covert operations to eliminate the Atreides bloodline.",
                    assignee: baron
                ),
                Domain.Task.Create(
                    title: "Decode Imperial Transmissions",
                    description: "Unravel encrypted messages to foresee the Emperor’s next move.",
                    assignee: thufir
                ),
                Domain.Task.Create(
                    title: "Negotiate Tribal Water Pact",
                    description: "Mediate between tribes for control of vital water resources.",
                    assignee: stilgar
                ),
            };

            db.Tasks.AddRange(tasks);

            // --- TimeRecords ---
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            paulVisions.CreateTimeRecord(paul, today.AddDays(-2), TimeSpan.FromHours(2.5));
            paulVisions.CreateTimeRecord(paul, today.AddDays(-1), TimeSpan.FromHours(3));
            paulVisions.CreateTimeRecord(paul, today, TimeSpan.FromHours(1.5));

            aliaPrescience.CreateTimeRecord(alia, today.AddDays(-1), TimeSpan.FromHours(2));
            aliaPrescience.CreateTimeRecord(alia, today, TimeSpan.FromHours(2.25));

            gurneyBallads.CreateTimeRecord(gurney, today, TimeSpan.FromHours(1));

            sietch.CreateTimeRecord(duncan, today, TimeSpan.FromHours(4));
            sietch.CreateTimeRecord(stilgar, today.AddDays(-2), TimeSpan.FromHours(2));

            db.SaveChanges();
        }
    }
}
