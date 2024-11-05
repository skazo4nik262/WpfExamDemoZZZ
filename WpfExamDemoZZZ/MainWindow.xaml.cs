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

        private async Task SignInClick(object sender, RoutedEventArgs e)
        {
            User.Password = password.Password;

            var responce = await client.PostAsync("User/CheckUserInDB", new StringContent("{}", Encoding.UTF8, "application/json"));
            var responce2 = await client.PostAsync("User/CheckUserRole", new StringContent("{}", Encoding.UTF8, "application/json"));
            var responce3 = await client.PostAsync("User/CheckFirstSign", new StringContent("{}", Encoding.UTF8, "application/json"));
            var responce4 = await client.PostAsync("User/CheckUserIsBlocked", new StringContent($"{User.Login}, {User.Password}", Encoding.UTF8, "application/json"));
            //Users = await responce.Content.ReadFromJsonAsync<List<User>>();
            //Roles = await responce2.Content.ReadFromJsonAsync<List<Role>>();
            //var user = Users.FirstOrDefault(s => s.Login == User.Login && s.Password == User.Password);
            var answerCheckUserInDB = await responce.Content.ReadFromJsonAsync<bool>();
            var answerCheckUserRole = await responce2.Content.ReadFromJsonAsync<bool>();
            var answerCheckFirstSign = await responce3.Content.ReadFromJsonAsync<bool>();
            var answerCheckUserIsBlocked = await responce4.Content.ReadFromJsonAsync<bool>();

            if (answerCheckUserIsBlocked)
            {
                if (answerCheckUserInDB)
                {
                    if (answerCheckUserRole)
                    {
                        if (answerCheckFirstSign == false)
                        {
                            WindowNew1 window = new WindowNew1(); MessageBox.Show("Вы успешно авторизовались"); Hide(); window.Show();
                        }
                        else
                        {
                            WindowNew1 window = new WindowNew1(answerCheckFirstSign); MessageBox.Show("Вы успешно авторизовались"); Hide(); window.Show();
                        }
                    }
                    else
                    {
                        if (answerCheckFirstSign == false)
                        {
                            AdminWindow adminWindow = new AdminWindow(); Hide(); adminWindow.Show();
                        }
                        else
                        {
                            AdminWindow adminWindow = new AdminWindow(answerCheckFirstSign); Hide(); adminWindow.Show();
                        }
                    }
                }
                else MessageBox.Show("Вы вввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
            }
            else MessageBox.Show("Вы заблокированы. Обратитесь к администратору");
        }
    }
}