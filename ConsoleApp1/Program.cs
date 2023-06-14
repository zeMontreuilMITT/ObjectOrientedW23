// Students can Enrol in Many courses
// Courses can have many enrolled students
// Enrollments are represented in the middle Enrollment class, which breaks the relationship between Student and Course

HashSet<Student> students = new HashSet<Student>();
HashSet<Course> courses = new HashSet<Course>();
HashSet<Enrollment> enrollments = new HashSet<Enrollment>();

students.Add(new Student(1));
courses.Add(new Course(2));

enrollments.Add(EnrollStudentInCourse(students.First(), courses.First()));

EnrollStudentInCourse(students.First(), courses.First());
SetGrade(92, 1, 2, enrollments);


// FUNCTIONS
Enrollment EnrollStudentInCourse(Student student, Course course)
{
    Enrollment newEnrollment = new Enrollment();

    // create references in enrollment to both sides of the relationship
    newEnrollment.Student = student;
    newEnrollment.Course = course;

    // give both sides a reference to the new enrollment
    course.Enrollments.Add(newEnrollment);
    student.Enrollments.Add(newEnrollment);

    return newEnrollment;
}

void SetGrade(int newGrade, int studentId, int courseId, HashSet<Enrollment> enrollments)
{
    foreach(Enrollment e in enrollments)
    {
        if(e.Student.Id == studentId && e.Course.Id == courseId)
        {
            e.Grade = newGrade;
        }
    }
}

// CLASSES
class Student
{
    public int Id { get; set; }
    public HashSet<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();

    public Student(int id)
    {
        Id = id;
    }
}

class Course
{
    public int Id { get; set; }
    public HashSet<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();

    public Course(int id)
    {
        Id = id;
    }
}

// track the relationship between students and their courses
class Enrollment
{
    public Student Student { get; set; }
    public Course Course { get; set; }
    public int Grade { get; set; }
}

// refactor the previous example to allow many students to enroll in many courses
