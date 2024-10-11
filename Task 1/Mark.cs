namespace Task_1
{
    public class Mark : IMark
    {
        private int _number;

        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                if(value < 0)
                {
                    _number = 0;
                }
                else if(value > 5)
                {
                    _number = 5;
                }
                else 
                {
                    _number = value;
                }
            }
        }

        public Mark(Student student, Subject subject, int number)
        {
            Student = student;
            Subject = subject;
            Number = number;
        }

        public static Mark Enter(LinkedList<Student> students, LinkedList<Subject> subjects)
        {
            Console.WriteLine("Виберіть предмет з списку");
            for(int i = 0; i < subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}-{subjects.ElementAt(i).Name}.");
            }
            Console.Write(">> "); int subject = Convert.ToInt32(Console.ReadLine());
            if(subject < 0)
            {
                subject = 0;
            }
            if(subject >= subjects.Count)
            {
                subject = subjects.Count - 1;
            }
            Console.WriteLine($"Ви вибрали: {subject + 1}-{subjects.ElementAt(subject).Name}.");

            Console.WriteLine("Виберіть студента з списку");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}-{students.ElementAt(i).Surname}.");
            }
            Console.Write(">> "); int student = Convert.ToInt32(Console.ReadLine());
            if (student < 0)
            {
                student = 0;
            }
            if (student >= students.Count)
            {
                student = students.Count - 1;
            }
            Console.WriteLine($"Ви вибрали: {student + 1}-{subjects.ElementAt(student).Name}.");
            Console.Write("Введіть оцінку: "); int number = Convert.ToInt32(Console.ReadLine());
            return new Mark(students.ElementAt(student), subjects.ElementAt(subject), number);
        }


        public override string ToString()
        {
            string result = string.Empty;
            result += $"Предмет: {Subject.Name}.\n";
            result += $"Студент: {Student.Surname}.\n";
            result += $"Оцінка: {Number}.\n";
            return result;
        }
    }
}
