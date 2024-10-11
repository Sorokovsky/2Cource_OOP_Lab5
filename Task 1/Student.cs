namespace Task_1
{
    public class Student : ISurnamable
    {
        public string Surname { get; set; } = string.Empty;

        public Student() : this("")
        {
            
        }

        public Student(string surname)
        {
            Surname = surname;
        }

        public static Student Enter()
        {
            Console.Write("Введіть прізвище студента: "); 
            return new Student(Console.ReadLine());
        }

        public override string ToString()
        {
            string result = string.Empty;
            result += $"Прізвище: {Surname}.\n";
            return result;
        }
    }
}