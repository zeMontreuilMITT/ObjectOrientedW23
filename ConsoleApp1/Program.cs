string sName = "Bob";
string cName = "Course";

School.RegisterStudent(sName);
School.RegisterStudent("Other Person");
School.RegisterStudent("Name Namerson");

School.RegisterCourse(cName);
School.RegisterCourse("Advanced Courses");

Student studentToAdd = School.GetStudent(sName);
Course courseToAdd = School.GetCourse(cName);

School.RegisterStudentForCourse(studentToAdd, courseToAdd);

static class School
{
    public static string Name { get; } = "MITT";
    private static HashSet<Course> _courses = new HashSet<Course>();
    private static HashSet<Student> _students = new HashSet<Student>();

    public static void RegisterStudent(string newStudentName)
    {
        Student newStudent = new Student(newStudentName);
        _students.Add(newStudent);
    }

    public static void RegisterCourse(string courseTitle)
    {
        Course newCourse = new Course(courseTitle);
        _courses.Add(newCourse);
        Console.WriteLine($"Total of {_courses.Count} courses.");
    }

    public static void RegisterStudentForCourse(Student student, Course course)
    {
        student.SetCourse(course);
        course.AddStudentToCourse(student);
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


    static School()
    {

    }
}

class Student
{
    private string _fullName;
    public string FullName { get {  return _fullName; } }

    private Course _course;
    public void SetCourse(Course course)
    {
        _course = course;
    }

    public Student(string fullName)
    {
        if (!String.IsNullOrEmpty(fullName))
        {
            _fullName = fullName;
        }
    }
}

class Course
{
    private string _title;
    public string Title { get { return _title; } }

    private HashSet<Student> _students = new HashSet<Student>();
    public void AddStudentToCourse(Student student)
    {
        _students.Add(student);
    }

    public Course(string title)
    {
        if (!String.IsNullOrEmpty(title))
        {
            _title = title;
        }
    }
}
// Students can Enroll in one course
// Courses can have many students in them
// These belong to a static School class
// School has an AddStudentToCourse(Student s, Course c) method defined on it