using System.Linq.Expressions;

namespace DelegatesLinQ.Homework
{
    // Data models for the homework
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double GPA { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public DateTime EnrollmentDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Major}) - GPA: {GPA}, Age: {Age}, Email: {Email}, Enrollment Date: {EnrollmentDate.ToShortDateString()}";
        }
    }

    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public double Grade { get; set; } // 0-4.0 scale
        public string Semester { get; set; }
        public string Instructor { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    /// <summary>
    /// Homework 3: LINQ Data Processor
    /// 
    /// This is the most challenging homework requiring students to:
    /// 1. Use complex LINQ operations with multiple data sources
    /// 2. Implement custom extension methods
    /// 3. Create generic delegates and expressions
    /// 4. Handle advanced scenarios like pivot operations, statistical analysis
    /// 5. Implement caching and performance optimization
    /// 
    /// Advanced Requirements:
    /// - Custom LINQ extension methods
    /// - Expression trees and dynamic queries
    /// - Performance comparison between different approaches
    /// - Statistical calculations and reporting
    /// - Data transformation and pivot operations
    /// </summary>
    public class LinqDataProcessor
    {
        private List<Student> _students;

        public LinqDataProcessor()
        {
            _students = GenerateSampleData();
        }

        // BASIC REQUIREMENTS (using techniques from sample codes)

        public void BasicQueries()
        {
            // TODO: Implement basic LINQ queries similar to 6_8_LinQObject
            // 1. Find all students with GPA > 3.5          
            // 2. Group students by major            
            // 3. Calculate average GPA per major           
            // 4. Find students enrolled in specific courses
            // 5. Sort students by enrollment date
            var sortedByEnrollment = _students.OrderBy(s => s.EnrollmentDate)
                .Select(s => new { s.Name, s.EnrollmentDate });

            Console.WriteLine("=== BASIC LINQ QUERIES ===");
            // Implementation needed by students
            Console.WriteLine("Students with GPA > 3.5:");
            _students.Where(s => s.GPA > 3.5)
                .ToList()
                .ForEach(s => Console.WriteLine($"{s.Name} - GPA: {s.GPA}"));

            Console.WriteLine("\nGrouped by Major:");
            var groupedByMajor = _students.GroupBy(s => s.Major)
                .Select(g => new { Major = g.Key, Students = g.ToList() });
            foreach (var group in groupedByMajor)
            {
                Console.WriteLine($"Major: {group.Major}");
                foreach (var student in group.Students)
                {
                    Console.WriteLine($"  {student.Name} - GPA: {student.GPA}");
                }
            }

            Console.WriteLine("\nAverage GPA by Major:");
            var averageGPAByMajor = groupedByMajor
                .Select(g => new { Major = g.Major, AverageGPA = g.Students.Average(s => s.GPA) });
            foreach (var avg in averageGPAByMajor)
            {
                Console.WriteLine($"Major: {avg.Major}, Average GPA: {avg.AverageGPA}");
            }

            Console.WriteLine("\nStudents enrolled in \"Intro to Programming\" courses:");
            var specificCourses = from s in _students
                                  where s.Courses.Any(c => c.Code == "CS101")
                                  select s.Name;
            foreach (var stu in specificCourses)
            {
                Console.WriteLine($"Student: {stu}");
            }
        }

        // Challenge 1: Create custom extension methods
        public void CustomExtensionMethods()
        {
            Console.WriteLine("=== CUSTOM EXTENSION METHODS ===");

            // TODO: Implement extension methods
            // 1. CreateAverageGPAByMajor() extension method           
            // 2. FilterByAgeRange(int min, int max) extension method  
            // 4. CalculateStatistics() extension method

            // Example usage (students need to implement the extensions):
            var highPerformers = _students.FilterByAgeRange(20, 25).Where(s => s.GPA > 3.5);
            Console.WriteLine("High performers (GPA > 3.5, Age 20-25):");
            foreach (var student in highPerformers)
            {
                Console.WriteLine($"{student.Name} - GPA: {student.GPA}, Age: {student.Age}");
            }
            Console.WriteLine();
            var gradeReport = _students.ToGradeReport();
            Console.WriteLine(gradeReport);

            var stats = _students.CalculateStatistics();
            Console.WriteLine("Student Statistics:");
            Console.WriteLine($"Mean GPA: {stats.MeanGPA:f2}");
            Console.WriteLine($"Median GPA: {stats.MedianGPA}");
            Console.WriteLine($"Mode GPA: {stats.ModeGPA}");
            Console.WriteLine($"Standard Deviation: {stats.StandardDeviation:f2}");
        }

        // Challenge 2: Dynamic queries using Expression Trees
        public void DynamicQueries()
        {
            Console.WriteLine("=== DYNAMIC QUERIES ===");

            // TODO: Research Expression Trees
            // Implement a method that builds LINQ queries dynamically based on user input
            // Example: BuildDynamicFilter("GPA", ">", "3.5")
            // This requires understanding of Expression<Func<T, bool>>

            // Students should implement:
            // 1. Dynamic filtering based on property name and value
            DynamicLinq<Student> dynamicLinq = new DynamicLinq<Student>();
            Console.WriteLine("Filtered Students with EnrollmentDate before 02/02/2022:");
            var filteredStudents = _students.AsQueryable().Where(dynamicLinq.BuildDynamicFilter("EnrollmentDate", "<=", new DateTime(2022, 2, 2).ToString())).ToList();
            foreach (var student in filteredStudents)
            {
                Console.WriteLine(student.ToString());
            }
            // 2. Dynamic sorting by any property 
            var sortedStudents = _students.AsQueryable().OrderBy(dynamicLinq.BuildDynamicProperty("GPA")).ToList();
            Console.WriteLine("\nSorted Students by GPA:");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine($"{student.Name} - GPA: {student.GPA}");
            }
            // 3. Dynamic grouping operations
            var groupedStudents = _students.AsQueryable()
                .GroupBy(dynamicLinq.BuildDynamicProperty("Major"))
                .Select(g => new { Major = g.Key, Students = g.ToList() })
                .ToList();
            Console.WriteLine("\nGrouped Students by Major:");
            foreach (var group in groupedStudents)
            {
                Console.WriteLine($"Major: {group.Major}");
                foreach (var student in group.Students)
                {
                    Console.WriteLine($"  {student.Name} - GPA: {student.GPA}");
                }
            }
        }

        // Challenge 3: Statistical Analysis with Complex Aggregations
        public void StatisticalAnalysis()
        {
            Console.WriteLine("=== STATISTICAL ANALYSIS ===");

            // TODO: Implement complex statistical calculations
            // 1. Calculate median GPA (requires custom logic)
            var meanGPA = _students.Select(s => s.GPA).ToList().Average();
            var medianGPA = _students.Select(s => s.GPA).ToList().CalculateMedian();
            Console.WriteLine($"Median GPA: {medianGPA}");
            // 2. Calculate standard deviation of GPAs
            var standardDeviation = _students.Select(s => s.GPA).ToList().CalculateStandardDeviation(meanGPA);
            Console.WriteLine($"Standard Deviation of GPA: {standardDeviation:f2}");
            // 3. Find correlation between age and GPA
            var correlation = _students.CalculateCorrelation();
            Console.WriteLine($"Correlation between Age and GPA: {correlation:f2}");
            // 4. Identify outliers using statistical methods
            var q1 = CalculateQuantile(_students.Select(s => s.GPA).ToList(), 0.25);
            var q3 = CalculateQuantile(_students.Select(s => s.GPA).ToList(), 0.75);
            var iqr = q3 - q1;
            var outliers = _students.Where(s => s.GPA < q1 - (1.5 * iqr) || s.GPA > q3 + (1.5 * iqr))
                .Select(s => new { s.Name, s.GPA });
            Console.WriteLine("Outliers GPA:");
            foreach (var outlier in outliers)
            {
                Console.WriteLine($"{outlier.Name} - GPA: {outlier.GPA}");
            }
            // 5. Create percentile rankings
            var percentileRankings = _students.Select(s => new
            {
                s.Name,
                GPA = s.GPA,
                Percentile = ((_students.Count(stu => stu.GPA < s.GPA) +0.5 * _students.Count(stu => stu.GPA == s.GPA)) / _students.Count ) * 100
                }).OrderByDescending(s => s.Percentile).ToList();
            Console.WriteLine("Percentile Rankings:");
            foreach (var ranking in percentileRankings)
            {
                Console.WriteLine($"{ranking.Name} - GPA: {ranking.GPA}, Percentile: {ranking.Percentile:f2}%");
            }
            // This requires research into statistical formulas and advanced LINQ usage
        }
        public double CalculateQuantile(List<double> values, double quantile)
        {
            if (quantile < 0 || quantile > 1 || !values.Any())
                throw new ArgumentException("Quantile must be between 0 and 1 and values cannot be empty.");
            var sorted = values.OrderBy(v => v).ToList();
            int index = (int)(quantile * (sorted.Count - 1));
            return sorted[index];
        }
        // Challenge 4: Data Pivot Operations
        public void PivotOperations()
        {
            Console.WriteLine("=== PIVOT OPERATIONS ===");

            // TODO: Research pivot table concepts
            // Create pivot tables showing:
            // 1. Students by Major vs GPA ranges (3.0-3.5, 3.5-4.0, etc.)
            var pivotTable = _students
            .GroupBy(s => s.Major)
            .Select(g => new
            {
                Major = g.Key,
                Students = g
                    .Where(s => s.GPA >= 3.0 && s.GPA < 3.5)
                    .Select(s => new { s.Name, s.GPA })
                    .ToList()
            })
            .ToList();

            Console.WriteLine("Pivot Table - Students by Major vs GPA Ranges:");
            foreach (var pivot in pivotTable) {
                Console.WriteLine($"Major: {pivot.Major}");
                foreach (var student in pivot.Students)
                {
                    Console.WriteLine($"  {student.Name} - GPA: {student.GPA}");
                }
            }

            // 2. Course enrollment by semester and major
            var courseEnrollment = _students
                .SelectMany(s => s.Courses, (s, c) => new { s.Major, c.Semester, c.Name, c.Instructor })
                .GroupBy(x => new { x.Major, x.Semester })
                .Select(g => new
                {
                    g.Key.Major,
                    g.Key.Semester,
                    Courses = g.Select(c => new { c.Name, c.Instructor }).ToList()
                })
                .ToList();
            Console.WriteLine("\nCourse Enrollment by Semester and Major:");
            foreach (var enrollment in courseEnrollment)
            {
                Console.WriteLine($"Major: {enrollment.Major}, Semester: {enrollment.Semester}");
                foreach (var course in enrollment.Courses)
                {
                    Console.WriteLine($"  Course: {course.Name}, Instructor: {course.Instructor}");
                }
            }
            // 3. Grade distribution across instructors
            var gradeDistribution = _students
                .SelectMany(s => s.Courses, (s, c) => new { c.Instructor, c.Grade })
                .GroupBy(x => x.Instructor)
                .Select(g => new
                {
                    Instructor = g.Key,
                    AverageGrade = g.Average(c => c.Grade),
                    GradeCount = g.Count()
                })
                .ToList();
            Console.WriteLine("\nGrade Distribution Across Instructors:");
            foreach (var distribution in gradeDistribution)
            {
                Console.WriteLine($"Instructor: {distribution.Instructor}, Average Grade: {distribution.AverageGrade:f2}, Total Courses: {distribution.GradeCount}");
            }

            // This requires understanding of GroupBy with multiple keys and complex projections
        }

        // Sample data generator
        private List<Student> GenerateSampleData()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1, Name = "Alice Johnson", Age = 20, Major = "Computer Science",
                    GPA = 3.8, EnrollmentDate = new DateTime(2022, 9, 1),
                    Email = "alice.j@university.edu",
                    Address = new Address { City = "Seattle", State = "WA", ZipCode = "98101" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "CS101", Name = "Intro to Programming", Credits = 3, Grade = 3.7, Semester = "Fall 2022", Instructor = "Dr. Smith" },
                        new Course { Code = "MATH201", Name = "Calculus II", Credits = 4, Grade = 3.9, Semester = "Fall 2022", Instructor = "Prof. Johnson" }
                    }
                },
                new Student
                {
                    Id = 2, Name = "Bob Wilson", Age = 22, Major = "Mathematics",
                    GPA = 3.2, EnrollmentDate = new DateTime(2021, 9, 1),
                    Email = "bob.w@university.edu",
                    Address = new Address { City = "Portland", State = "OR", ZipCode = "97201" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "MATH301", Name = "Linear Algebra", Credits = 3, Grade = 3.3, Semester = "Spring 2023", Instructor = "Dr. Brown" },
                        new Course { Code = "STAT101", Name = "Statistics", Credits = 3, Grade = 3.1, Semester = "Spring 2023", Instructor = "Prof. Davis" }
                    }
                },
                // Add more sample students...
                new Student
                {
                    Id = 3, Name = "Carol Davis", Age = 19, Major = "Computer Science",
                    GPA = 3.9, EnrollmentDate = new DateTime(2023, 9, 1),
                    Email = "carol.d@university.edu",
                    Address = new Address { City = "San Francisco", State = "CA", ZipCode = "94101" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "CS102", Name = "Data Structures", Credits = 4, Grade = 4.0, Semester = "Fall 2023", Instructor = "Dr. Smith" },
                        new Course { Code = "CS201", Name = "Algorithms", Credits = 3, Grade = 3.8, Semester = "Fall 2023", Instructor = "Prof. Lee" }
                    }
                }
            };
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 3: LINQ DATA PROCESSOR ===");
            Console.WriteLine();

            Console.WriteLine("BASIC REQUIREMENTS:");
            Console.WriteLine("1. Implement basic LINQ queries (filtering, grouping, sorting)");
            Console.WriteLine("2. Use SelectMany for course data");
            Console.WriteLine("3. Calculate averages and aggregations");
            Console.WriteLine();

            Console.WriteLine("ADVANCED REQUIREMENTS:");
            Console.WriteLine("1. Create custom LINQ extension methods");
            Console.WriteLine("2. Implement dynamic queries using Expression Trees");
            Console.WriteLine("3. Perform statistical analysis (median, std dev, correlation)");
            Console.WriteLine("4. Create pivot table operations");
            Console.WriteLine();

            LinqDataProcessor processor = new LinqDataProcessor();

            // Students should implement all these methods
            processor.BasicQueries();
            processor.CustomExtensionMethods();
            processor.DynamicQueries();
            processor.StatisticalAnalysis();
            processor.PivotOperations();

            Console.ReadKey();
        }
    }

    // TODO: Implement these extension methods
    public static class StudentExtensions
    {
        // Challenge: Implement custom extension methods
        public static IEnumerable<Student> FilterByAgeRange(this IEnumerable<Student> students, int minAge, int maxAge)
        {
            return students.Where(s => s.Age >= minAge && s.Age <= maxAge);
        }
        public static Dictionary<string, double> AverageGPAByMajor(this IEnumerable<Student> students)
        {
            return students.GroupBy(s => s.Major)
                .ToDictionary(g => g.Key, g => g.Average(s => s.GPA));
        }
        public static string ToGradeReport(this IEnumerable<Student> students)
        {
            String result = "Grade Report:\n";
            foreach (var student in students)
            {
                result += $"{student.Name} ({student.Major}): GPA = {student.GPA}\n";
                foreach (var course in student.Courses)
                {
                    result += $"  {course.Name} ({course.Code}): Grade = {course.Grade}\n";
                }
            }
            return result;
        }
        public static StudentStatistics CalculateStatistics(this IEnumerable<Student> students)
        {
            var gpas = students.Select(s => s.GPA).ToList();
            if (!gpas.Any())
                return new StudentStatistics();
            var mean = gpas.Average();
            var median = CalculateMedian(gpas);
            var mode = CalculateMode(gpas);
            var stdDev = CalculateStandardDeviation(gpas, mean);
            return new StudentStatistics
            {
                MeanGPA = mean,
                MedianGPA = median,
                ModeGPA = mode,
                StandardDeviation = stdDev
            };
        }
        public static double CalculateMedian(this List<double> values)
        {
            var sorted = values.OrderBy(v => v).ToList();
            int count = sorted.Count;
            if (count % 2 == 0)
            {
                return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
            }
            else
            {
                return sorted[count / 2];
            }
        }
        public static double CalculateMode(this List<double> values)
        {
            return values.GroupBy(v => v)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? 0.0;
        }
        public static double CalculateStandardDeviation(this List<double> values, double mean)
        {
            if (values.Count == 0) return 0.0;
            double sumOfSquares = values.Sum(v => Math.Pow(v - mean, 2));
            return Math.Sqrt(sumOfSquares / values.Count);
        }

        public static double CalculateCorrelation(this List<Student> students)
        {
            if (students == null || students.Count < 2)
                return 0.0;

            var ages = students.Select(s => (double)s.Age).ToList();
            var gpas = students.Select(s => s.GPA).ToList();

            double meanAge = ages.Average();
            double meanGPA = gpas.Average();

            double numerator = 0.0;
            double sumSqAge = 0.0;
            double sumSqGPA = 0.0;

            for (int i = 0; i < students.Count; i++)
            {
                double diffAge = ages[i] - meanAge;
                double diffGPA = gpas[i] - meanGPA;

                numerator += diffAge * diffGPA;
                sumSqAge += diffAge * diffAge;
                sumSqGPA += diffGPA * diffGPA;
            }

            double denominator = Math.Sqrt(sumSqAge) * Math.Sqrt(sumSqGPA);
            if (denominator == 0) return 0.0;

            return numerator / denominator;
        }

    }

    // TODO: Define this class for statistical operations
    public class StudentStatistics
    {
        // Properties for mean, median, mode, standard deviation, etc.
        public double MeanGPA { get; set; }
        public double MedianGPA { get; set; }
        public double ModeGPA { get; set; }
        public double StandardDeviation { get; set; }

    }

    public class DynamicLinq<T>
    {
        public Expression<Func<T, bool>> BuildDynamicFilter(string propertyName, string op, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var propertyType = typeof(T).GetProperty(propertyName)?.PropertyType
                ?? throw new ArgumentException($"Property {propertyName} not found");

            // Convert string value to the property's type
            object convertedValue = Convert.ChangeType(value, propertyType);

            Expression comparison;
            var constant = Expression.Constant(convertedValue, propertyType);

            switch (op.ToLower())
            {
                case ">":
                    comparison = Expression.GreaterThan(property, constant);
                    break;
                case "<":
                    comparison = Expression.LessThan(property, constant);
                    break;
                case "=":
                case "==":
                    comparison = Expression.Equal(property, constant);
                    break;
                case ">=":
                    comparison = Expression.GreaterThanOrEqual(property, constant);
                    break;
                case "<=":
                    comparison = Expression.LessThanOrEqual(property, constant);
                    break;
                default:
                    throw new ArgumentException($"Unsupported operator: {op}");
            }

            return Expression.Lambda<Func<T, bool>>(comparison, parameter);
        }

        public Expression<Func<T, object>> BuildDynamicProperty(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var convertedProperty = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<T, object>>(convertedProperty, parameter);
        }

        public static Func<IQueryable<T>, IOrderedQueryable<T>> BuildDynamicSort<T>(string propertyName, bool ascending)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(param, propertyName);
            var propertyType = property.Type;

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), propertyType);
            var lambda = Expression.Lambda(delegateType, property, param);

            var methodName = ascending ? "OrderBy" : "OrderByDescending";

            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), propertyType);

            return source => (IOrderedQueryable<T>)method.Invoke(null, new object[] { source, lambda });
        }

        public IEnumerable<IGrouping<object, T>> GroupByProperty(IQueryable<T> source, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var keySelector = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            return source.GroupBy(keySelector);
        }
    }

}
