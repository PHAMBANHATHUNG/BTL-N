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

namespace BTLỚN
{
    /// <summary>
    /// Interaction logic for Sign_in.xaml
    /// </summary>
    public partial class Sign_in : Window
    {
        public Sign_in()
        {
            InitializeComponent();
        }

        private void btDN_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenMay.Text.Length == 0 || txtCSDL.Text.Length == 0 || txtName.Text.Length == 0 || txtPW.Text.Length == 0)
            {
                MessageBox.Show("Hay nhap day du");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Dang nhap vao Danh sach DT?", "Danh sach DT", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Vao danh sach");
                    MainWindow signin = new MainWindow();
                    signin.Show();
                    this.Close();
                }
            }
        }

        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Thoát",MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
