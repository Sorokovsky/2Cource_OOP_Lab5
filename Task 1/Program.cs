//Variant 12
namespace Task_1;
public static class Program
{
    private delegate void Operation();
    private static LinkedList<University> UNIVERSITIES = new();
    private static string FILE_PATH = "database.bin";

    public static void Main()
    {
        Operation doing;
        while(true)
        {
            try
            {
                int operation = ChooseOperation();
                switch (operation)
                {
                    case 0:
                        {
                            return;
                        }
                    case 1:
                        {
                            doing = EnterAll;
                            break;
                        }
                    case 2:
                        {
                           doing = ShowAll;
                            break;
                        }
                    case 3:
                        {
                            doing = Finding;   
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Не відома операція. Спробуйте ще раз.");
                        }
                }
                doing.Invoke();
            } 
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                continue;
            }
        }
    }

    private static void Finding()
    {
        ReadFromFile();
        foreach(var univer in UNIVERSITIES)
        {
            foreach(var group in univer.Groups)
            {
                foreach (var student in group.Students)
                {
                    var studentMarks = group.Marks.Where(mark => mark.Student.Surname.Equals(student.Surname));
                    double avarage = studentMarks.Sum(x => x.Number) / studentMarks.Count();
                    if (avarage == 4.5)
                    {
                        Console.WriteLine($"Студент: {student.Surname}, Група: {group.Name}, Університет: {univer.Name}");
                    }
                }
            }
        }
    }

    public static void EnterAll()
    {
        Console.Write("Введіть кількість університетів: "); 
        int count = Convert.ToInt32(Console.ReadLine());
        if(count < 0) count = 0;
        UNIVERSITIES.Clear();
        for(int i = 0; i < count; i++)
        {
            Console.WriteLine($"Введення {i + 1} університету.");
            UNIVERSITIES.AddLast(University.Enter());
        }
        WriteToFile();
    }

    private static void ShowAll()
    {
         ReadFromFile();
        for(int i = 0; i < UNIVERSITIES.Count; i++)
        {
            Console.WriteLine($"#{i + 1}.");
            Console.WriteLine(UNIVERSITIES.ElementAt(i));
        }
    }

    private static int ChooseOperation()
    {
        Console.WriteLine("Виберіть опцію.");
        Console.WriteLine("0-Вийти.");
        Console.WriteLine("1-Ввести данні.");
        Console.WriteLine("2-Вивести все.");
        Console.WriteLine("3-Показати прізвища студентів, групу та інститут, де середній бал становить 4,5.");
        Console.Write(">> ");
        int operation = Convert.ToInt32(Console.ReadLine());
        return operation;
    }

    private static void ReadFromFile()
    {
        if(File.Exists(FILE_PATH))
        {
            using BinaryReader reader = new BinaryReader(File.OpenRead(FILE_PATH));
            try
            {
                UNIVERSITIES = ReadUniversities(reader);
            } 
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        } 
        else
        {
            throw new IOException("Файлу не існує.");
        }
    }

    private static LinkedList<University> ReadUniversities(BinaryReader reader)
    {
        int count = reader.ReadInt32();
        LinkedList<University> universities = new();
        for(int i = 0; i < count; i++)
        {
            var university = new University(reader.ReadString());
            LinkedList<Group> groups = university.Groups;
            int groupsSize = reader.ReadInt32();
            for(int j = 0; j < groupsSize; j++)
            {
                string name = reader.ReadString();
                var group = new Group();
                group.Name = name;
                int students = reader.ReadInt32();
                for(int k = 0; k < students; k++)
                {
                    group.Students.AddLast(new Student(reader.ReadString()));
                }
                int subjects = reader.ReadInt32();
                for (int k = 0; k < subjects; k++)
                {
                    group.Subjects.AddLast(new Subject(reader.ReadString()));
                }
                int marks = reader.ReadInt32();
                for(int k = 0; k < marks; k++)
                {
                    Mark mark = new(new(reader.ReadString()), new(reader.ReadString()), reader.ReadInt32());
                    group.Marks.AddLast(mark);
                }
                university.Groups.AddLast(group);
            }
            universities.AddLast(university);
        }
        return universities;
    }

    private static void WriteUniversity(BinaryWriter writer)
    {
        writer.Write(UNIVERSITIES.Count);
        foreach (var university in UNIVERSITIES)
        {
            writer.Write(university.Name);
            LinkedList<Group> groups = university.Groups;
            writer.Write(groups.Count);
            foreach (var group in groups)
            {
                writer.Write(group.Name);
                LinkedList<Student> students = group.Students;
                LinkedList<Subject> subjects = group.Subjects;
                LinkedList<Mark> marks = group.Marks;
                writer.Write(students.Count);
                foreach (var student in students)
                {
                    writer.Write(student.Surname);
                }
                writer.Write(subjects.Count);
                foreach (var subject in subjects)
                {
                    writer.Write(subject.Name);
                }
                writer.Write(marks.Count);
                foreach(var mark in marks)
                {
                    writer.Write(mark.Student.Surname);
                    writer.Write(mark.Subject.Name);
                    writer.Write(mark.Number);
                }
            }
        }
    }

    private static void WriteToFile()
    {
        using BinaryWriter writer = new(File.Open(FILE_PATH, FileMode.Create));
        WriteUniversity(writer);
    }
}