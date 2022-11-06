using BethanysPieShopHRM.App.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components.Widgets
{
    public partial class EmployeeCountWidget
    {
        [Inject]
        public IEmployeeDataService? EmployeeDataService { get; set; }
        public int EmployeeCounter { get; set; }

        protected async override Task OnInitializedAsync()
        {
            EmployeeCounter = (await EmployeeDataService.GetAllEmployees()).Count();
        }
    }
}
