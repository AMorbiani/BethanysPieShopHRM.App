using BethanysPieShopHRM.App.Services.Interface;
using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        public IEmployeeDataService? EmployeeDataService { get; set; }
        [Inject]
        public ICountryDataService? CountryDataService { get; set; }
        [Inject]
        public IJobCategoryDataService? JobCategoryDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public ILocalStorageService? LocalStorageService { get; set; }


        [Parameter]
        public string? EmployeeId { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Countries = (await CountryDataService.GetAllCountries()).ToList();
            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

            int.TryParse(EmployeeId, out var employeeId);

            if (employeeId == 0) // creating new employee
            {
                Employee = new Employee()
                {
                    CountryId = 1,
                    JobCategoryId = 1,
                    BirthDate = DateTime.Now,
                    JoinedDate = DateTime.Now
                };
            }
            else
            {
                Employee = await EmployeeDataService.GetEmployeeById(int.Parse(EmployeeId));
            }
        }

        private IBrowserFile selectedFile;
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
            StateHasChanged();
        }
        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (Employee.EmployeeId == 0) //new
            {
                //image adding
                if (selectedFile != null)
                {
                    var file = selectedFile;
                    Stream stream = file.OpenReadStream();
                    MemoryStream ms = new();
                    await stream.CopyToAsync(ms);
                    stream.Close();

                    Employee.ImageName = file.Name;
                    Employee.ImageContent = ms.ToArray();
                }

                var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;

                    // CLEAR CACHE
                    await LocalStorageService.ClearAsync();
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                //image adding
                if (selectedFile != null)
                {
                    var file = selectedFile;
                    Stream stream = file.OpenReadStream();
                    MemoryStream ms = new();
                    await stream.CopyToAsync(ms);
                    stream.Close();

                    Employee.ImageName = file.Name;
                    Employee.ImageContent = ms.ToArray();
                }
                await EmployeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;

                // CLEAR CACHE
                await LocalStorageService.ClearAsync();
            }
        }

        protected async Task HandleInvalidSubmit()
        {
            StatusClass = "alter-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted succesfully";

            Saved = true;

            // CLEAR CACHE
            await LocalStorageService.ClearAsync();
        }

        public void NavigateToOverview()
        {
            NavigationManager.NavigateTo($"/EmployeeOverview");
        }    
    }
}
