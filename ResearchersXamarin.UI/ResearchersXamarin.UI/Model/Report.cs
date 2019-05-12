namespace ResearchersXamarin.UI.Model
{
    public class Report
    {
        
        public int Id { get; set; }

        // Название научного отчёта
        
        public string Name { get; set; }

        // Регистрационный номер
        
        public int RegisterNumber { get; set; }

        // Год выпуска
        
        public int ReleaseYear { get; set; }

        // Число страниц
        
        public int PageCount { get; set; }

        public Report() { }

        public Report(int id, string name, int registerNumber, int releaseYear, int pageCount)
        {
            Id = id;
            Name = name;
            RegisterNumber = registerNumber;
            ReleaseYear = releaseYear;
            PageCount = pageCount;
        }
    }
}