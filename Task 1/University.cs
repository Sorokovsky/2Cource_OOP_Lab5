namespace Task_1
{
    public class University : INamable, IGroupsable
    {
        public string Name { get; set; } = string.Empty;
        public LinkedList<Group> Groups { get; set; } = new();
        public University(string name)
        {
            Name = name;
        }

        public University() : this("")
        {
            
        }

        public static University Enter()
        {
            Console.Write("Введіть ім'я університету: "); 
            University university = new(Console.ReadLine());
            Console.Write("Введіть кількість груп: "); int groups = Convert.ToInt32(Console.ReadLine());
            if (groups < 0) groups = 0;
            for (int i = 0; i < groups; i++)
            {
                university.Groups.AddLast(Group.Enter());
            }
            return university;
        }

        public override string ToString()
        {
            string result = string.Empty;
            result += $"Ім'я університету: {Name}. \n";
            result += "Групи: \n";
            foreach(Group group in Groups)
            {
                result += group.ToString();
            }
            return result;
        }
    }
}
