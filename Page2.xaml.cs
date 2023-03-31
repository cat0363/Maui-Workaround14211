using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui_Workaround14211;

public partial class Page2 : ContentPage
{
    /// <summary>
    /// Employee
    /// </summary>
    public class Employee : INotifyPropertyChanged
    {
        /// <summary>
        /// Property Changed Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Property Changed 
        /// </summary>
        /// <param name="name">Property Name</param>
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Employee Id
        /// </summary>
        private int pEmployeeId;
        /// <summary>
        /// Employee Name
        /// </summary>
        private string pEmployeeName;

        /// <summary>
        /// Employee Id
        /// </summary>
        public int EmployeeId
        {
            get
            {
                return pEmployeeId;
            }
            set
            {
                if (pEmployeeId != value)
                {
                    pEmployeeId = value;
                    OnPropertyChanged(nameof(EmployeeId));
                }
            }
        }
        /// <summary>
        /// Employee Name
        /// </summary>
        public string EmployeeName
        {
            get
            {
                return pEmployeeName;
            }
            set
            {
                if (pEmployeeName != value)
                {
                    pEmployeeName = value;
                    OnPropertyChanged(nameof(EmployeeName));
                }
            }
        }
    }

    /// <summary>
    /// MainPage View Model
    /// </summary>
    public class ViewModelPage2 : INotifyPropertyChanged
    {
        /// <summary>
        /// Property Changed Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Property Changed 
        /// </summary>
        /// <param name="name">Property Name</param>
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Employee List
        /// </summary>
        private List<Employee> pEmployeeList;

        /// <summary>
        /// Employee List (Orderd EmployeeId)
        /// </summary>
        public IEnumerable<Employee> Employees
        {
            get
            {
                return pEmployeeList.AsEnumerable()
                    .OrderBy(
                        x => x.EmployeeId
                    );
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModelPage2()
        {
            // Create Employee List
            pEmployeeList = new List<Employee>();
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <param name="employeeName">Employee Name</param>
        public void AddEmployee(int employeeId, string employeeName)
        {
            // Add Employee
            pEmployeeList.Add(new Employee() { EmployeeId = employeeId, EmployeeName = employeeName });
            // Update Employee List
            OnPropertyChanged(nameof(Employees));
        }
    }

    /// <summary>
    /// Page2 ViewModel
    /// </summary>
    public ViewModelPage2 vmPage2 = null;

    /// <summary>
    /// Constructor
    /// </summary>
    public Page2()
    {
        InitializeComponent();

        // Create Page2 View Model
        vmPage2 = new ViewModelPage2();
        // Add Test Employee
        vmPage2.AddEmployee(1, "Taro Yamada");
        vmPage2.AddEmployee(2, "Hanako Yamada");

        // Bind Page2 View Model
        this.BindingContext = vmPage2;
    }

    /// <summary>
    /// Add Employee
    /// </summary>
    /// <param name="sender">Event Source</param>
    /// <param name="e">Event Args</param>
    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        // Clear Focus
        new ServiceFocus().ClearFocus();

        // Set Next Employee Id
        int nextId = vmPage2.Employees.AsEnumerable().Max(x => x.EmployeeId) + 1;
        // Create New Employee
        vmPage2.AddEmployee(nextId, null);

        // Scroll Employee List
        IDispatcherTimer timer;
        timer = Dispatcher.CreateTimer();
        timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        timer.Tick += (s, e) =>
        {
            timer.Stop();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                svEmployeeList.ScrollToAsync(slEmployeeList, ScrollToPosition.End, true);
            });
        };
        timer.Start();
    }
}

