namespace ResearchersXamarin.ASPService.DataContracts
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

        internal Monograph(Business.Logic.Monograph monograph)
        {
            Id = monograph.Id;
            Name = monograph.Name;
            CoauthorLastName = monograph.CoauthorLastName;
            CoauthorFirstName = monograph.CoauthorFirstName;
            CoauthorMiddleName = monograph.CoauthorMiddleName;
            ReleaseDate = monograph.ReleaseDate;
            PageCount = monograph.PageCount;
        }
    }
}