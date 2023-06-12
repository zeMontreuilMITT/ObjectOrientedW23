// Object Declaration
Student newStudent;

// Initialize and Assign new object by invoking the constructor method
newStudent = new Student("Bob Burger");

Course softwareCourse = new Course(100, "Introduction to Software Development");

void AddStudentToCourse(Student student, Course course)
{
    student.CurrentCourse = course;
    course.AddStudentToCourse(student);
}

// class definition
class Student
{
    // Members
    // field: serves as variable for class
    private string _name;

    // properties: methods for accessing fields
    public string Name { get { return _name; } }

    // refer to the course the student is taking
    private Course _currentCourse;
    public Course CurrentCourse { get { return _currentCourse; } set { _currentCourse = value; } }

    // constructor method: default
    // adding any constructor removes the default constructor
    public Student(string name)
    {
        Console.WriteLine($"Creating new student with the name of {name}");
        if(name.Length > 0)
        {
            _name = name;
        }
    }
}
class Course
{
    private int _id;
    public int Id { get { return _id;} }

    private string _title;
    public string Title { get { return _title;} }

    // list must be initialized for use
    // assignment of values on fields/properties will occur when the Course is initialized
    // refers to all of the students enrolled in this course
    private List<Student> _students = new List<Student>();
    public void AddStudentToCourse(Student student)
    {
        _students.Add(student);
    }
    public void RemoveStudentFromCourse(Student student)
    {
        _students.Remove(student);
    }


    public Course(int id, string title)
    {
        if (id > 0 && !String.IsNullOrEmpty(title))
        {
            _id = id;
            _title = title;
        }
    }
}