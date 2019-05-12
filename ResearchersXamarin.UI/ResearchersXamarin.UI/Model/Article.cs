using System;

namespace ResearchersXamarin.UI.Model
{
    public class Article
    {
        public int Id { get; set; }

        // Название статьи
        public string Name { get; set; }

        // Название журнала
        public string MagazineName { get; set; }

        // Год и месяц издания
        public DateTime ReleaseDate { get; set; }

        public Article() { }

        public Article(int id, string name, string magazineName, DateTime releaseDate)
        {
            Id = id;
            Name = name;
            MagazineName = magazineName;
            ReleaseDate = releaseDate;
        }
    }
}