using ResearchersXamarin.ASPService.DataContracts;
using ResearchersXamarin.Service.DataContracts;

namespace ResearchersXamarin.ASPService.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IResearcherData" в коде и файле конфигурации.
    public interface IResearcherData
    {
        #region Article

        List<Article> GetArticleByResearcher(int researcherId);

        int AddArticle(int researcherId, Article article);

        void UpdateArticle(Article article);

        void DeleteArticle(int articleId);

        Article GetArticle(int articleId);

        #endregion

        #region Monograph

        List<Monograph> GetMonographByResearcher(int researcherId);

        int AddMonograph(int researcherId, Monograph monograph);

        void UpdateMonograph(Monograph monograph);

        void DeleteMonograph(int monographId);

        Monograph GetMonograph(int monographId);

        #endregion

        #region Presentation

        List<Presentation> GetPresentationByResearcher(int researcherId);

        int AddPresentation(int researcherId, Presentation presentation);

        void UpdatePresentation(Presentation presentation);

        void DeletePresentation(int presentationId);

        Presentation GetPresentation(int presentationId);

        #endregion

        #region Report

        List<Report> GetReportByResearcher(int researcherId);

        int AddReport(int researcherId, Report report);

        void UpdateReport(Report report);

        void DeleteReport(int reportId);

        Report GetReport(int reportId);

        #endregion

        #region Request

        int GetPresentationRequest(DateTime dateTime);

        int GetReportRequest(int departmentNumber);

        #endregion

        #region Researcher

        List<Researcher> GetResearchers();

        int AddResearcher(string lastName, string firstName, string middleName, int departmentNumber, int age, string academicDegree, string position);

        void UpdateResearcher(Researcher c);

        void DeleteResearcher(int researcherId);

        #endregion
    }
}
