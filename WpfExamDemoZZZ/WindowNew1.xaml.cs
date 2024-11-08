using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WindowNew1.xaml
    /// </summary>
    public partial class WindowNew1 : Window
    {
        private bool firstSign;
        private User user;
        HttpClient client = new HttpClient();

        public Visibility FirstSignVisibility { get; set; } = Visibility.Collapsed;

        

        public WindowNew1(User user)
        {
            DataContext = this;
            this.user = user;
            if (user.FirstSign == true)
                FirstSignVisibility = Visibility.Visible;
            else FirstSignVisibility = Visibility.Collapsed;
            client.BaseAddress = new Uri("http://localhost:5185/api/");
            Method();
            InitializeComponent();
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
