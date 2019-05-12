namespace ResearchersXamarin.UI.Model
{
    public class Monograph
    {
        public int Id { get; set; }

        // Название монографии
        public string Name { get; set; }

        // ФИО соавтора
        public string CoauthorLastName { get; set; }

        public string CoauthorFirstName { get; set; }

        public string CoauthorMiddleName { get; set; }

        // Год издания
        public int ReleaseDate { get; set; }

        // Число страниц
        public int PageCount { get; set; }

        public Monograph() { }

        public Monograph(int id, string name, string coauthorLastName, string coauthorFirstName, string coauthorMiddleName, int releaseDate, int pageCount)
        {
            Id = id;
            Name = name;
            CoauthorLastName = coauthorLastName;
            CoauthorFirstName = coauthorFirstName;
            CoauthorMiddleName = coauthorMiddleName;
            ReleaseDate = releaseDate;
            PageCount = pageCount;
        }
    }
}