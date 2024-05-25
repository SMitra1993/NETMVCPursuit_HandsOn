using MVCWebAppHandsOn.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace MVCWebAppHandsOn.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StudentContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstName="Carson",LastName="White",Email="Alexander",Phone="4567891234"},
            new Student{FirstName="Meredith",LastName="Tyson",Email="Alonso",Phone="4563461234"},
            new Student{FirstName="Arturo",LastName="Stark",Email="Anand",Phone="4568247234"},
            new Student{FirstName="Gytis",LastName="Pollock",Email="Barzdukas",Phone="9764891234"},
            new Student{FirstName="Yan",LastName="Brandon",Email="Li",Phone="4567893578"},
            new Student{FirstName="Peggy",LastName="Maxwell",Email="Justice",Phone="5149891234"},
            new Student{FirstName="Laura",LastName="Taylor",Email="Norman",Phone="4567865481"},
            new Student{FirstName="Nino",LastName="Wells",Email="Olivetto",Phone="4375942234"}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();
        }
    }
}