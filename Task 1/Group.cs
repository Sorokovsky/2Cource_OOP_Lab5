namespace Task_1
{
    public class Group : INamable, IStudentsable, ISubjectsable
    {
        public string Name { get; set; } = string.Empty;
        public LinkedList<Student> Students {get; set;}
        public LinkedList<Subject> Subjects {get; set;}
        public readonly LinkedList<Mark> Marks;

        public Group()
        {
            Students = new LinkedList<Student>();
            Subjects = new LinkedList<Subject>();
            Marks = new LinkedList<Mark>();
        }

        public Group(IEnumerable<Student> students, IEnumerable<Subject> subjects) : this()
        {
            Students = new(students);
            Subjects = new(subjects);
        }

        public static Group Enter()
        {
            Group group = new();
            Console.Write("Введіть назву групи: "); group.Name = Console.ReadLine();
            Console.Write("Введіть кількість студентів: "); int students = Convert.ToInt32(Console.ReadLine());
            if (students < 0) students = 0;
            for (int i = 0; i < students; i++)
            {
                Console.WriteLine($"Введення {i + 1} студента.");
                group.Students.AddLast(Student.Enter());
            }
            Console.Write("Введіть кількість предметів: "); int subjects = Convert.ToInt32(Console.ReadLine());
            if (subjects < 0) subjects = 0;
            for (int i = 0; i < subjects; i++)
            {
                Console.WriteLine($"Введення {i + 1} предмета.");
                group.Subjects.AddLast(Subject.Enter());
            }

            Console.Write("Введіть кількість оцінок: "); int marks = Convert.ToInt32(Console.ReadLine());
            if (marks < 0) marks = 0;
            for (int i = 0; i < marks; i++)
            {
                Console.WriteLine($"Введення {i + 1} предмета.");
                group.Marks.AddLast(Mark.Enter(group.Students, group.Subjects));
            }
            return group;
        }

        public override string ToString()
        {
            string result = string.Empty;
            result += $"Ім'я групи: {Name}.\n";
            result += $"Студенти: \n";
            foreach (var student in Students)
            {
                result += student.ToString();
            }

            result += $"Предмети: \n";
            foreach (var subject in Subjects)
            {
                result += subject.ToString();
            }

            result += $"Оцінки: \n";
            foreach (var mark in Marks)
            {
                result += mark.ToString();
            }
            return result;
        }
    }
}
