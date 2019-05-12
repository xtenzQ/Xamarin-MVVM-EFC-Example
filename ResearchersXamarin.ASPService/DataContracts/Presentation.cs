using System;
using System.Runtime.Serialization;

namespace ResearchersXamarin.Service.DataContracts
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

        internal Presentation(Business.Logic.Presentation presentation)
        {
            Id = presentation.Id;
            Name = presentation.Name;
            ConferenceName = presentation.ConferenceName;
            PresentationDate = presentation.PresentationDate;
        }
    }
}