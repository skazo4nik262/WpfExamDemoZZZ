using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        public List<RoleModel> Roles { get; set; } = new List<RoleModel>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            client.BaseAddress = new Uri("http://localhost:5185/api/");
        }

        private async void SignInClick(object sender, RoutedEventArgs e)
        {
            await NewMethodNew();
        }

        private async Task NewMethodNew()
        {
            User.Password = password.Password;

            //var responce = await client.PostAsync($"User/CheckUserInDB?login={User.Login}&password={User.Password}", new StringContent("{}", Encoding.UTF8, "application/json"));
            var a = await client.PostAsJsonAsync("User/CheckUserInDB", User);
            var b = await client.PostAsJsonAsync("User/CheckUserRole", User);
            var c = await client.PostAsJsonAsync("User/CheckFirstSign", User);
            var d = await client.PostAsJsonAsync("User/CheckUserIsBlocked", User);
            //var responce = await client.PostAsync("User/CheckUserInDB", new StringContent(JsonSerializer.Serialize(User), Encoding.UTF8, "application/json"));//partial не виноват
            //var responce2 = await client.PostAsync("User/CheckUserRole", new StringContent($"{User.Login}, {User.Password}", Encoding.UTF8, "application/json"));
            //var responce3 = await client.PostAsync("User/CheckFirstSign", new StringContent($"{User.Login}, {User.Password}", Encoding.UTF8, "application/json"));
            //var responce4 = await client.PostAsync("User/CheckUserIsBlocked", new StringContent($"{User.Login}, {User.Password}, {User.ErrorCount}", Encoding.UTF8, "application/json"));

            //Users = await responce.Content.ReadFromJsonAsync<List<User>>();
            //Roles = await responce2.Content.ReadFromJsonAsync<List<Role>>();
            //var user = Users.FirstOrDefault(s => s.Login == User.Login && s.Password == User.Password);
            //var shit = await responce.Content.ReadAsStringAsync();

            var answerCheckUserInDB = await a.Content.ReadFromJsonAsync<bool>();
            var answerCheckUserRole = await b.Content.ReadFromJsonAsync<bool>();
            var answerCheckFirstSign = await c.Content.ReadFromJsonAsync<bool>();
            var answerCheckUserIsBlocked = await d.Content.ReadFromJsonAsync<bool>();


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
                else { MessageBox.Show("Вы вввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные"); User.ErrorCount++; }
            }
            else MessageBox.Show("Вы заблокированы. Обратитесь к администратору");
        }
    }
}