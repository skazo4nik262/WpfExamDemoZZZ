using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfExamDemoZZZ
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window, INotifyPropertyChanged
    {
        private bool firstSign;
        private User user;
        HttpClient client = new HttpClient();
        private List<User> users = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public User User { get; set; }
        public List<User> Users { get => users; set { users = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Users))); } }

        public Visibility FirstSignVisibility { get; set; } = Visibility.Collapsed;


        public AdminWindow(User user)
        {
            DataContext = this;
            User = new User();
            client.BaseAddress = new Uri("http://localhost:5185/api/");
            NewMethod();
            this.user = user;
            if (user.FirstSign == true)
                FirstSignVisibility = Visibility.Visible;
            else FirstSignVisibility = Visibility.Collapsed;
            Method();
            InitializeComponent();
        }

        private async Task NewMethod()
        {
            var responseMessage = await client.PostAsJsonAsync("User/GetAllUser", new StringContent("{}", Encoding.UTF8, "application/json"));
            Users = await responseMessage.Content.ReadFromJsonAsync<List<User>>();
        }

        public async Task Method()
        {
            var a = await client.PostAsJsonAsync("User/LastVisitChange", user);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
