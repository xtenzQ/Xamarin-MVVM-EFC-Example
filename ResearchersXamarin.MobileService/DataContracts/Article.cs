using System;

namespace ResearchersXamarin.MobileService.DataContracts
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
        internal Article(Business.Logic.Article article)
        {
            Id = article.Id;
            Name = article.Name;
            MagazineName = article.MagazineName;
            ReleaseDate = article.ReleaseDate;
        }
    }
}