using CourseProjectDb.Models;
using CourseProjectDb;
using CourseDb.Models;

namespace CourseProjectNew
{
    public static class DbSeeder
    {
        public static void SeedAirplaneTypes(ApplicationContext context)
        {
            if (!context.AirplaneTypes.Any())
            {
                var airplaneTypes = new List<AirplaneType>
                {
                    new AirplaneType { Name = "Passenger" },
                    new AirplaneType { Name = "Cargo" },
                    new AirplaneType { Name = "Business Class" },
                    new AirplaneType { Name = "Military" }
                };

                context.AirplaneTypes.AddRange(airplaneTypes);
                context.SaveChanges();

                var airplanes = new List<Airplane>
                {
                    new Airplane { AirplaneType = airplaneTypes[0], Name = "Airbus A320", ReleaseDate = DateOnly.FromDateTime(new DateTime(2000, 1, 1)), AirplaneStatus = AirplaneStatus.InAirport, FlightsCount = 0, ArrivalTime = new TimeOnly(10, 0, 0), DepartureTime = new TimeOnly(12, 0, 0), SeatsCount = 50 },
                    new Airplane { AirplaneType = airplaneTypes[1], Name = "Boeing 747", ReleaseDate = DateOnly.FromDateTime(new DateTime(2005, 1, 1)), AirplaneStatus = AirplaneStatus.InAirport, FlightsCount = 0, ArrivalTime = new TimeOnly(14, 0, 0), DepartureTime = new TimeOnly(16, 0, 0), SeatsCount = 75 },
                    new Airplane { AirplaneType = airplaneTypes[2], Name = "Airbus A380", ReleaseDate = DateOnly.FromDateTime(new DateTime(2010, 1, 1)), AirplaneStatus = AirplaneStatus.InAirport, FlightsCount = 0, ArrivalTime = new TimeOnly(18, 0, 0), DepartureTime = new TimeOnly(20, 0, 0), SeatsCount = 125 },
                    new Airplane { AirplaneType = airplaneTypes[3], Name = "Boeing 777", ReleaseDate = DateOnly.FromDateTime(new DateTime(2015, 1, 1)), AirplaneStatus = AirplaneStatus.InAirport, FlightsCount = 0, ArrivalTime = new TimeOnly(22, 0, 0), DepartureTime = new TimeOnly(0, 0, 0), SeatsCount = 100 }
                };
                context.Airplanes.AddRange(airplanes);
                context.SaveChanges();
            }
        }
        public static void SeedDepartments(ApplicationContext context)
        {
            if (!context.Departments.Any())
            {
                var departments = new List<Department>();

                var departmentHead1 = new Employee
                {
                    SecondName = "Smith",
                    FirstName = "John",
                    Surname = "Doe",
                    JobTitle = "Head",
                    WorkingSince = DateOnly.FromDateTime(DateTime.Now.AddYears(-5)),
                    Gender = "Male",
                    DateOfBirth = DateOnly.FromDateTime(new DateTime(1980, 1, 1)),
                    AmountOfKids = 2,
                    Salary = 5000m
                };

                var employees1 = new List<Employee>();
                for (int j = 1; j <= 12; j++)
                {
                    employees1.Add(new Employee
                    {
                        SecondName = $"Employee{j}",
                        FirstName = $"FirstName{j}",
                        Surname = $"Surname{j}",
                        JobTitle = "Staff",
                        WorkingSince = DateOnly.FromDateTime(DateTime.Now.AddYears(-j)),
                        Gender = j % 2 == 0 ? "Male" : "Female",
                        DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, j, 1)),
                        AmountOfKids = j % 3,
                        Salary = 3000m + j * 100
                    });
                }

                departments.Add(new Department { DepartmentName = "Engineering", DepartmentHead = departmentHead1, Employees = employees1 });

                var departmentHead2 = new Employee
                {
                    SecondName = "Johnson",
                    FirstName = "Jane",
                    Surname = "Doe",
                    JobTitle = "Head",
                    WorkingSince = DateOnly.FromDateTime(DateTime.Now.AddYears(-4)),
                    Gender = "Female",
                    DateOfBirth = DateOnly.FromDateTime(new DateTime(1982, 1, 1)),
                    AmountOfKids = 1,
                    Salary = 5500m
                };

                var employees2 = new List<Employee>();
                for (int j = 1; j <= 12; j++)
                {
                    employees2.Add(new Employee
                    {
                        SecondName = $"Employee{j}",
                        FirstName = $"FirstName{j}",
                        Surname = $"Surname{j}",
                        JobTitle = "Staff",
                        WorkingSince = DateOnly.FromDateTime(DateTime.Now.AddYears(-j)),
                        Gender = j % 2 == 0 ? "Female" : "Male",
                        DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, j, 1)),
                        AmountOfKids = j % 2,
                        Salary = 3500m + j * 100
                    });
                }

                departments.Add(new Department { DepartmentName = "Operations", DepartmentHead = departmentHead2, Employees = employees2 });

                for (int i = 1; i <= 2; i++)
                {
                    var brigades = new List<Brigade>();
                    for (int j = 1; j <= 2; j++)
                    {
                        var brigadeEmployees = departments[i - 1].Employees.Skip((j - 1) * 7).Take(7).ToList();
                        // Make the first employee of each brigade a pilot
                        brigadeEmployees[0].JobTitle = "Pilot";
                        brigades.Add(new Brigade { Name = $"Brigade{i * 10 + j}", Employees = brigadeEmployees });
                    }
                    departments[i - 1].Brigades = brigades;
                }

                context.Departments.AddRange(departments);
                context.SaveChanges();

                var medicalWatches = new List<MedicalWatch>();

                foreach (var department in departments)
                {
                    foreach (var employee in department.Employees)
                    {
                        medicalWatches.Add(new MedicalWatch
                        {
                            Employee = employee,
                            Diagnosis = "Healthy",
                            IsPassed = true,
                            BeginTime = DateTime.Now,
                            EndTime = DateTime.Now.AddHours(8)
                        });
                    }
                }

                context.MedicalWatches.AddRange(medicalWatches);
                context.SaveChanges();
            }
        }
        public static void SeedPassengers(ApplicationContext context)
        {
            if (!context.Passengers.Any())
            {
                var passengers = new List<Passenger>();
                for (int i = 0; i < 100; i++)
                {
                    passengers.Add(new Passenger
                    {
                        Name = $"Passenger{i}",
                        FirstName = $"FirstName{i}",
                        SecondName = $"SecondName{i}",
                        LastName = $"LastName{i}",
                        PassportNumber = i,
                        InternationalPassport = i,
                        Tickets = new List<Ticket>()
                    });
                }

                context.Passengers.AddRange(passengers);
                context.SaveChanges();
            }
        }
        public static void SeedFlights(ApplicationContext context)
        {
            var brigades = context.Brigades.ToList();
            var airplanes = context.Airplanes.ToList();
            var passengers = context.Passengers.ToList();
            foreach (var airplane in airplanes)
            {
                airplane.Flights = new List<Flight>();
                for (int i = 0; i < 10; i++)
                {
                    var flight = new Flight
                    {
                        DepartureDays = (DaysOfWeek)(1 << i % 7),
                        Airplane = airplane,
                        Brigade = brigades[i % brigades.Count], // Используем бригады
                        FlightType = FlightType.Domestic,
                        FlightStatus = FlightStatus.Scheduled,
                        Origin = "Origin" + i,
                        Destination = "Destination" + i,
                        Tickets = new List<Ticket>()
                    };

                    foreach (var passenger in passengers.Skip(i * 10).Take(10))
                    {
                        flight.Tickets.Add(new Ticket
                        {
                            Passenger = passenger,
                            SoldTime = DateTime.Now.AddMonths(-i),
                            Status = TicketStatus.Available,
                            Price = 125
                        });
                    }

                    airplane.Flights.Add(flight);
                }
            }
            context.SaveChanges();
        }
        
    }

}
