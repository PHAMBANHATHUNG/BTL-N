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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace BTLỚN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
public partial class MainWindow : Window
    {
        int cbg = 0;
        public MainWindow()
        {
            InitializeComponent();
            combo();
        }


        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            if (cbg == 1)
            {
                MessageBoxResult result = MessageBox.Show("Cap Nhat DT va SL", "Cap nhat kho", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    if (txtGia.Text.Length == 0 || txtMaHang.Text.Length == 0 || txtSL.Text.Length == 0)
                    {
                        MessageBox.Show("Nhap day du de cap nhat");
                    }
                    else
                    {
                        int Gia = int.Parse(txtGia.Text);
                        int SL = int.Parse(txtSL.Text);
                        if (Gia < 0 || SL < 0)
                        {
                            MessageBox.Show("Lam gi co gia do, nhap lai");
                        }
                        else
                        {
                            if (Gia % 1000 != 0)
                            {
                                MessageBox.Show("Yeu cau de bai, nhap lai");
                            }
                            else
                            {
                                DienThoai dd = new DienThoai();
                                dd.MaHang = txtMaHang.Text;
                                dd.DonGia = txtGia.Text;
                                dd.TonKho = txtSL.Text;
                                KT_DT(dd);
                                MessageBox.Show("Cap nhat thanh cong");
                            }

                        }

                    }
                }
                }
            else
            {
                MessageBox.Show("Click vao checkbox Cap nhat gia va tk");

            }

            }
         
        
        private int Cap_nhat_DT(DienThoai dd)
        {
            try
            {
                DataTable ds = new DataTable();
                using (SqlConnection connection = new SqlConnection(@"Server=DESKTOP-30D4PD3;Database=QuanlyDT;Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("SELECT * FROM DIENTHOAI", connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                {
                    adapter.FillSchema(ds, SchemaType.Source);
                    adapter.Fill(ds);
                    DataRow[] dt = ds.Select(
                    "MaHang = '" + dd.MaHang + "'");
                    dt[0]["DonGia"] = dd.DonGia;
                    dt[0]["TonKho"] = dd.TonKho;
                    adapter.Update(ds);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi:" + ex.Message);
                
            }
            return -1;
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Ban phai nhap Ma Hang");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn chac chan muon xoa?","Xoa DT",MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    DienThoai dd = new DienThoai();
                    dd.MaHang = txtMaHang.Text;
                    dd.TonKho = txtSL.Text;
                    Xoa_Dien_Thoai(dd);
                }
            }
        }
        private void Xoa_Dien_Thoai(DienThoai dd)
        {
            try
            {
                DataTable danhsach = new DataTable();
                using (SqlConnection connection = new SqlConnection(@"Server=DESKTOP-30D4PD3;Database=QuanlyDT;Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("SELECT * FROM DIENTHOAI", connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                {
                    adapter.FillSchema(danhsach, SchemaType.Source);
                    adapter.Fill(danhsach);
                    DataRow[] dt = danhsach.Select(
                    "MaHang = '" + dd.MaHang + "'");
                    if ((int) dt[0]["Tonkho"] == 0)
                    {
                        MessageBoxResult result = MessageBox.Show("Hang DT se bi xoa","Xoa DT",MessageBoxButton.YesNo, MessageBoxImage.Question,MessageBoxResult.No);

                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                        else
                        {
                            danhsach.Select("MaHang = '" + dd.MaHang + "'");
                            dt[0].Delete();
                            adapter.Update(dt);
                            MessageBox.Show("Da Het Hang");
                            dth.ItemsSource = danhsach.DefaultView;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ton kho, khong xoa");
                    }


                }
                

            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Loi khi mo  ket noi: " + ex.Message); 
            }
           
        }

        private void combo()
        {
            try
            {
                DataTable danhsach = new DataTable(); 
                using (SqlConnection connection = new SqlConnection(@"Server=DESKTOP-30D4PD3;Database=QuanlyDT;Integrated Security=SSPI")) 
                using (SqlCommand command = new SqlCommand("SELECT * FROM DIENTHOAI", connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    { 
                        adapter.Fill(danhsach);
                        cboMaHang.ItemsSource = danhsach.DefaultView;
                        cboMaHang.DisplayMemberPath = "Ten";
                        cboMaHang.SelectedValuePath = "MaHang";
                        command.Dispose();
                        connection.Close();
                    }
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi: " + ex.Message);
            }
        }

       

        private void Btn_hang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable danhsach = new DataTable();
                using (SqlConnection connection = new SqlConnection(@"Server=DESKTOP-30D4PD3;Database=QuanlyDT;Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("SELECT * FROM DIENTHOAI WHERE Ten =" + "'" + cboMaHang.Text + "';", connection))
                {
                    connection.Open();
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        { adapter.Fill(danhsach); }
                    }
                }
                MessageBox.Show("Hay tham khao");
                dth.ItemsSource = danhsach.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi: " + ex.Message);
            }
        }


        private void CbGia_Click(object sender, RoutedEventArgs e)
        {
            if(cbGia.IsChecked == true )
            {
                cbg = 1;
            }
            else
            {
                cbg = 0;
            }
        }
        private void GiamGia(DienThoai dd)
        {
            MessageBoxResult result = MessageBox.Show("Khuyen mai giam gia", "Khuyen mai",MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
                Cap_nhat_DT(dd);
            else
                return;
        }
        private void TangGia(DienThoai dd)
        {
            MessageBoxResult result = MessageBox.Show("Khuyen mai tang gia", "Khuyen mai",MessageBoxButton.YesNo, MessageBoxImage.Question,MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
                Cap_nhat_DT(dd);
            else
                return;
        }
        
        private void KT_DT(DienThoai dd)
        {
            int GiaDT;
            try
            {
                DataTable danhsach = new DataTable();
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-30D4PD3; Database=QuanlyDT; Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("SELECT * FROM DIENTHOAI", connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                {
                    adapter.FillSchema(danhsach, SchemaType.Source);
                    adapter.Fill(danhsach);
                    DataRow[] dt = danhsach.Select("MaHang = '" + dd.MaHang + "'");
                    GiaDT = (int)dt[0]["DonGia"];
                }

                if (GiaDT > Int32.Parse(txtGia.Text))
                    GiamGia(dd);
                else
                    if (GiaDT < Int32.Parse(txtGia.Text))
                    TangGia(dd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi: " + ex.Message);
            }
        }

        
    }
    }
