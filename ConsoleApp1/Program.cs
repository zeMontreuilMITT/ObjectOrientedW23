try
{
    School.RegisterStudent("Bob", "Turkey", "bob@school.ca");
    School.RegisterInstructor("Linda", "Canada", "linda@school.ca", 10000000);


    School.RegisterCourse("Intro to Burgers");
    School.RegisterCourse("Advanced Courses");

    Student studentToAdd = School.GetStudent("Bob");
    Course courseToAdd = School.GetCourse("Intro to Burgers");

    School.RegisterPersonForCourse(studentToAdd, courseToAdd);
} catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static class School
{
    public static string Name { get; } = "MITT";
    
    private static HashSet<Course> _courses = new HashSet<Course>();
    private static HashSet<Student> _students = new HashSet<Student>();
    private static HashSet<Instructor> _instructors = new HashSet<Instructor>();

    private static int _idCount = 1;

    public static void RegisterStudent(string newStudentName, 
        string newStudentCountry, 
        string newStudentEmail)
    {
        Student newStudent = new Student(_idCount, 
            newStudentName, 
            newStudentCountry, 
            newStudentEmail);

        _idCount++;
        _students.Add(newStudent);
    }
    public static void RegisterInstructor(string newInstructorName, 
        string newInstructorCountry, 
        string newInstructorEmail, 
        int newInstructorSalary)
    {
        Instructor newInstructor = new Instructor(_idCount, 
            newInstructorName, 
            newInstructorCountry, 
            newInstructorEmail, 
            newInstructorSalary);

        _idCount++;
        _instructors.Add(newInstructor);
    }

    public static void RegisterCourse(string courseTitle)
    {
        Course newCourse = new Course(courseTitle);
        _courses.Add(newCourse);
        Console.WriteLine($"Total of {_courses.Count} courses.");
    }
    public static void RegisterPersonForCourse(Person person, Course course)
    {
        person.SetCourse(course);

        if(person is Instructor)
        {
            course.SetInstructor((Instructor)person);
        } else if (person is Student)
        {
            if(course.GetStudentCount() < course.Capacity)
            {
                course.AddStudentToCourse((Student)person);
            }
            else
            {
                throw new InvalidOperationException("Student count cannot exceed course capacity.");
            }
        }
    }

    public static Student? GetStudent(string studentName)
    {
        Student foundStudent = _students.First(s => s.FullName == studentName);
        return foundStudent;
    }
    public static Course? GetCourse(string courseName)
    {
        Course foundCourse = _courses.First(c => c.Title == courseName);
        return foundCourse;
    }
    public static Instructor? GetInstructor(int? id)
    {
        if (id == null)
        {
            throw new ArgumentNullException("This method requires an Id value that is not null");
        }
        Instructor foundInstructor = _instructors.First(i => i.RegistrationNumber == id);
        return foundInstructor;
    }

    static School()
    {

    }
}

class Course
{
    private string _title;
    public string Title { get { return _title; } }

    private int _capacity;
    public int Capacity { get { return _capacity; } }


    private Instructor _instructor;
    private HashSet<Student> _students = new HashSet<Student>();
    public double GetAverageGrade()
    {
        double totalGrade = 0;

        foreach(Student s in _students)
        {
            totalGrade += s.Grade;
        }

        double average = totalGrade / _students.Count;
        return average;
    }
    public void AddStudentToCourse(Student student)
    {
        _students.Add(student);
    }
    public void SetInstructor(Instructor instructor)
    {
        _instructor = instructor;
    }
    public void SetCapacity(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException("Capacity must be greater than 0");
        } else
        {
            _capacity = capacity;
        }
    }
    public int GetStudentCount()
    {
        return _students.Count;
    }
    public Course(string title, int capacity)
    {
        if (!String.IsNullOrEmpty(title))
        {
            _title = title;
            SetCapacity(capacity);
        } else
        {
            throw new ArgumentNullException("Course title must not be empty");
        }

    }
}


abstract class Person 
{
    private int _registrationNumber;
    public int RegistrationNumber { get { return _registrationNumber; } }

    private string _fullName;
    public string FullName { get { return _fullName;} }

    private string _countryOfOrigin;
    public string CountryOfOrigin { get { return _countryOfOrigin;} }

    private string _contactEmail;
    public string ContactEmail { get { return _contactEmail;} }
    public abstract void SetCourse(Course course);

    public Person(int regNumber, string name, string country, string email)
    {
        // todo: validate all arguments
        _registrationNumber = regNumber;
        _fullName = name;
        _countryOfOrigin = country;
        _contactEmail = email;
    }

}

// Student and Instructor
class Student: Person
{
    private int _grade;
    public int Grade
    {
        get { return _grade; }
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException("Grades must fall within range of 0 to 100");
            } else
            {
                _grade = value;
            }
        }
    }

    private Course _enrolledCourse;

    // abstract methods MUST be overridden (given their own body) in a child class
    public override void SetCourse(Course course)
    {
        _enrolledCourse = course;
    }
    // Student s = new Student(1, "stude", "here", "stude@school.ca");
    public Student(int regNumber, string name, string country, string email): base(regNumber, name, country, email)
    {

    }

    public Student(int regNumber, string name, string country, string email, Course course) : base(regNumber, name, country, email)
    {
        SetCourse(course);
    }
}
class Instructor: Person
{
    private Course _instructedCourse;
    private int _salary;
    public override void SetCourse(Course course)
    {
        _instructedCourse = course;
    }

    public Instructor(int regNumber, string name, string country, string email, int salary) : base(regNumber, name, country, email)
    {
        _salary = salary;
    }

    public Instructor(int regNumber, string name, string country, string email, int salary, Course course) : base(regNumber, name, country, email)
    {
        _salary = salary;
        SetCourse(course);
    }
}
