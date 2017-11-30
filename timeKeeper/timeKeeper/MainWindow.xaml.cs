using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace timeKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //TfsConnection tfsConnection = new TfsConnection();
            //teamList.ItemsSource = tfsConnection.ProjectTeamDictionary.Keys.ToList();

            ObservableCollection<Task> taskList = new ObservableCollection<Task>();

            taskList.Add(new Task("Task 1", 8));
            taskList.Add(new Task("Task 2", 4));

            teamList.ItemsSource = taskList;
        }
    }
}
