using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfExamDemoZZZ
{
    /// <summary>
    /// Логика взаимодействия для WindowNew1.xaml
    /// </summary>
    public partial class WindowNew1 : Window
    {
        private bool firstSign;

        public WindowNew1()
        {
            InitializeComponent();
        }

        public WindowNew1(bool firstSign)
        {
            this.firstSign = firstSign;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
