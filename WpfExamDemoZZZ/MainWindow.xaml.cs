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
        private static MainWindow instance;

        public User User { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            client.BaseAddress = new Uri("http://localhost:5185/api/");
            User = new User();
        }

        public static MainWindow Instance { get => instance ??= new(); }

        private async void SignInClick(object sender, RoutedEventArgs e)
        {
            await NewMethodNew();
        }

        private async Task NewMethodNew()
        {
            User.Password = password.Password;

            var hui = JsonSerializer.Serialize(User);
            var d = await client.PostAsJsonAsync("User/CheckUserIsBlocked", User);
            var answerCheckUserIsBlocked = await d.Content.ReadFromJsonAsync<bool>();
            if (answerCheckUserIsBlocked)
            {
                var a = await client.PostAsJsonAsync("User/CheckUserInDB", User);
                var answerCheckUserInDB = await a.Content.ReadFromJsonAsync<bool>();
                if (answerCheckUserInDB)
                {
                    var b = await client.PostAsJsonAsync("User/CheckUserRole", User);
                    var answerCheckUserRole = await b.Content.ReadFromJsonAsync<bool>();
                    if (answerCheckUserRole)
                    {
                        var c = await client.PostAsJsonAsync("User/CheckFirstSign", User);
                        var answerCheckFirstSign = await c.Content.ReadFromJsonAsync<bool>();
                        User.FirstSign = answerCheckFirstSign;
                        WindowNew1 window = new WindowNew1(User); MessageBox.Show("Вы успешно авторизовались"); Hide(); window.Show();
                    }
                    else
                    {
                        var c = await client.PostAsJsonAsync("User/CheckFirstSign", User);
                        var answerCheckFirstSign = await c.Content.ReadFromJsonAsync<bool>();
                        User.FirstSign = answerCheckFirstSign;
                        AdminWindow adminWindow = new AdminWindow(User); Hide(); adminWindow.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Вы вввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные"); User.ErrorCount++;
                }
            }
            else MessageBox.Show("Вы заблокированы. Обратитесь к администратору");
        }
    }
}