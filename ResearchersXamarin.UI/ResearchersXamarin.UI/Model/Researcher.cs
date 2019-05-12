namespace ResearchersXamarin.UI.Model
{
    public class Researcher
    {
        public int ResearcherId { get; set; }
        
        public string LastName { get; set; }
        
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public int DepartmentNumber { get; set; }

        public int Age { get; set; }
        
        public string AcademicDegree { get; set; }

        public string Position { get; set; }

        public Researcher() { }

        public Researcher(int researcherId, string lastName, string firstName, string middleName, int departmentNumber, int age,
            string academicDegree, string position)
        {
            ResearcherId = researcherId;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            DepartmentNumber = departmentNumber;
            Age = age;
            AcademicDegree = academicDegree;
            Position = position;
        }

    }
}