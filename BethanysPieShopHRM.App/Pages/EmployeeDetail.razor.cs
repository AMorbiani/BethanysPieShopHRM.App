using BethanysPieShopHRM.App.Services.Interface;
using BethanysPieShopHRM.Shared.Domain;
using BethanysPieShopHRM.Shared.Model;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Tracing;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeDetail
    {
        [Inject]
        public IEmployeeDataService? EmployeeDataService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; } = default!;
        public Employee? Employee { get; set; } = new Employee();
        public List<Marker> MapMarkers { get; set; } = new List<Marker>();

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeDataService.GetEmployeeById(int.Parse(EmployeeId));

            if (Employee.Latitude.HasValue && Employee.Longitude.HasValue)
            {
                MapMarkers = new List<Marker>()
                {
                    new Marker() { Y = Employee.Latitude.Value, X = Employee.Longitude.Value, Description = $"{Employee.FirstName} {Employee.LastName}", ShowPopup = false}
                };
            }
        }
    }
}
