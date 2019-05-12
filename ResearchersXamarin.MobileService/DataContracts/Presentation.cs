using System;

namespace ResearchersXamarin.MobileService.DataContracts
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
        internal Presentation(Business.Logic.Presentation presentation)
        {
            Id = presentation.Id;
            Name = presentation.Name;
            ConferenceName = presentation.ConferenceName;
            PresentationDate = presentation.PresentationDate;
        }
    }
}