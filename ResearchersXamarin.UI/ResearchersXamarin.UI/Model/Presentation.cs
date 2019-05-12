using System;

namespace ResearchersXamarin.UI.Model
{
    public class Presentation
    {
        
        public int Id { get; set; }

        // Название доклада
        
        public string Name { get; set; }

        // Название конференции
        
        public string ConferenceName { get; set; }

        // Дата выступления
        
        public DateTime PresentationDate { get; set; }

        public Presentation() { }

        public Presentation(int id, string name, string conferenceName, DateTime presentationDate)
        {
            Id = id;
            Name = name;
            ConferenceName = conferenceName;
            PresentationDate = presentationDate;
        }
    }
}