using Kolokwium.DAL;
using Kolokwium.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kolokwium
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<EMP> EmployeeList;
        private ObservableCollection<DEPT> DeptList;


        public MainWindow()
        {
            InitializeComponent();
            LoadIntoGridBoxDataFromDB();
            LoadIntoDnameComboBoxFromDB();
            SearchButton.Click += SearchButton_Click;
            ShowAllButton.Click += ShowAllButton_Click;
            AddEmployeeButton.Click += AddEmployeeButton_Click;
        }
        

        private void LoadIntoDnameComboBoxFromDB()
        {
            try
            {
                DeptList = EfServiceDb.LoadDEPTsDataFromDb();
            }
            catch (Exception e)
            {
                MessageBox.Show("Błąd w komunikacji z bazą danych!");
                MessageBox.Show(e.StackTrace);
            }

            DnameComboBox.ItemsSource = DeptList;
        }

        private void LoadIntoGridBoxDataFromDB()
        {
            try
            {
                 EmployeeList = EfServiceDb.LoadEMPsDataFromDb();
            }
            catch (Exception e)
            {
                MessageBox.Show("Błąd w komunikacji z bazą danych!");
                MessageBox.Show(e.StackTrace);
            }

            EmployeeDataGrid.ItemsSource = EmployeeList;
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var SearchText = SearchTextBox.Text;

            try
            {
                  EmployeeList = EfServiceDb.LoadEMPsWithStringInNameDataFromDb(SearchText);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Błąd w komunikacji z bazą danych!");
                MessageBox.Show(exc.StackTrace);
            }

            EmployeeDataGrid.ItemsSource = EmployeeList;
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            LoadIntoGridBoxDataFromDB();
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            var Ename = EnameTextBox.Text;
            var Job = JobTextBox.Text;
            var Dept = DnameComboBox.SelectedValue;
            
            if (Ename == null || Job == null || Ename == "" || Job == "" || Dept == null)
            {
                MessageBox.Show("Nie wszystkie pola zostały wypełnione!");
                return;
            }

            var NewEmp = new EMP
            {
                ENAME = Ename,
                JOB = Job,
                DEPT = (DEPT)Dept
            };

            var isNewEmpAdded = EfServiceDb.addEmp(NewEmp);

            if (isNewEmpAdded)
            {
                EmployeeList.Add(NewEmp);
            }
            else
            {
                MessageBox.Show("Nie udało się dodać nowego studenta.");
            }
        }
    }
}
