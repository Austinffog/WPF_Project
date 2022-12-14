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

namespace QuestionOne
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ContentBtn_Click(object sender, RoutedEventArgs e)
        {
            Screen2 content = new Screen2();
            content.Owner = this;
            this.Hide();
            content.Show();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
