using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.App.Services.Interface
{
    public interface IJobCategoryDataService
    {
        Task<IEnumerable<JobCategory>> GetAllJobCategories();
        Task<JobCategory> GetJobCategoryById(int jobCategoryId);
    }
}
