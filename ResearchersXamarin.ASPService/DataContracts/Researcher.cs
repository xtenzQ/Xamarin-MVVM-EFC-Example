namespace ResearchersXamarin.ASPService.DataContracts
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

        internal Researcher(Business.Logic.Researcher researcher)
        {
            ResearcherId = researcher.ResearcherId;
            LastName = researcher.LastName;
            FirstName = researcher.FirstName;
            MiddleName = researcher.MiddleName;
            DepartmentNumber = researcher.DepartmentNumber;
            Age = researcher.Age;
            AcademicDegree = researcher.AcademicDegree;
            Position = researcher.Position;
        }

    }
}