using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfExamDemoZZZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public User User { get; set; } = new User();
        public List<User> Users { get; set; } = new List<User>();
        public List<Role> Roles { get; set; } = new List<Role>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            client.BaseAddress = new Uri("http://localhost:5185/api/");
        }

        private async void SignInClick(object sender, RoutedEventArgs e)
        {
            User.Password = password.Password;

            var responce = await client.PostAsync("User/GetAllUser", new StringContent("{}", Encoding.UTF8, "application/json"));
            var responce2 = await client.PostAsync("User/GetAllRole", new StringContent("{}", Encoding.UTF8, "application/json"));
            Users = await responce.Content.ReadFromJsonAsync<List<User>>();
            Roles = await responce2.Content.ReadFromJsonAsync<List<Role>>();
            var user = Users.FirstOrDefault(s => s.Login == User.Login && s.Password == User.Password);

            if (user != null)
            {
                if (Roles.FirstOrDefault(s => s.Id == user.RoleId).RoleName == "Пользователь")
                {
                    if (user.FirstSign == false)
                    {
                        WindowNew1 window = new WindowNew1();
                        MessageBox.Show("Вы успешно авторизовались");
                        Hide();
                        window.Show();
                    }
                    else
                    {
                        WindowNew1 window = new WindowNew1(user.FirstSign);
                        MessageBox.Show("Вы успешно авторизовались");
                        Hide();
                        window.Show();
                    }
                }
                else
                {
                    if (user.FirstSign == false)
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        Hide();
                        adminWindow.Show();
                    }
                    else
                    {
                        AdminWindow adminWindow = new AdminWindow(user.FirstSign);
                        Hide();
                        adminWindow.Show();
                    }
                }
            }
            else MessageBox.Show("Вы вввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
        }
    }
}