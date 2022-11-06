using BethanysPieShopHRM.App.Services.Interface;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeOverview
    {
        [Inject]
        public IEmployeeDataService? EmployeeDataService { get; set; }
        public List<Employee>? Employees { get; set; } = default!;

        private string Title = "EmployeeOverview";

        private Employee? _selectedEmployee;

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }

        public void ShowQuickViewPopup(Employee selectedEmployee)
        {
            if (selectedEmployee != null)
            {
                _selectedEmployee = selectedEmployee;
            }
        }
    }
}
