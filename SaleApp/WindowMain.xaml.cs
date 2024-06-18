using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace SaleApp
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        LoginWindow loginView;
        private IMemberRepository memberRepository = new MemberRepository();
        private IProductRepository productRepository = new ProductRepository();
        private IOrderRepository orderRepository = new OrderRepository();
        private IOrderDetailRepository iorderRepository = new OrderDetailRepository();
        private bool isMember;
        private bool isAdmin;
        private string email;



        public WindowMain(LoginWindow loginView, bool memberStatus, bool adminStatus, string email)
        {

            InitializeComponent();
            this.loginView = loginView;
            isMember = memberStatus;
            isAdmin = adminStatus;

            SetTabVisibility();
            this.email = email;
        }


        private void SetTabVisibility()
        {
            if (isAdmin)
            {
                Visibility = Visibility.Visible;
                EditProfile.Visibility = Visibility.Collapsed;
            }
            else
            {
                memberTab.Visibility = Visibility.Collapsed;
            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabItem selectedTab = ((sender as TabControl).SelectedItem as TabItem);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadListMember();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadListMember()
        {
            lvMember.ItemsSource = memberRepository.ReadAll();
        }

        private void LoadListProduct()
        {
            lvProducts.ItemsSource = productRepository.ReadAll();
        }



        private T GetObjectFromTextBoxes<T>()
        {
            T obj = default(T);
            try
            {
                obj = Activator.CreateInstance<T>();


                foreach (var property in typeof(T).GetProperties())
                {
                    var textBox = FindTextBoxByName(property.Name);
                    if (textBox != null)
                    {
                        object value = null;
                        if (!string.IsNullOrWhiteSpace(textBox.Text))
                        {
                            Type propType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            value = Convert.ChangeType(textBox.Text, propType);
                        }
                        property.SetValue(obj, value);
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Invalid format: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting property value: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return obj;
        }

        // Helper method to find TextBox by name
        private TextBox FindTextBoxByName(string name)
        {
            var field = this.GetType().GetField($"txt{name}", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return field?.GetValue(this) as TextBox;
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member rm = GetObjectFromTextBoxes<Member>();
                if (rm != null)
                {
                    memberRepository.Delete(rm);
                    LoadListMember();
                    MessageBox.Show("Delete successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member add = GetObjectFromTextBoxes<Member>();

                if (add != null)
                {
                    memberRepository.Create(add);
                    LoadListMember();
                    MessageBox.Show("Add successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadListMember();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member upp = GetObjectFromTextBoxes<Member>();

                if (upp != null)
                {
                    memberRepository.Update(upp);
                    LoadListMember();
                    MessageBox.Show("Update successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //---------------------------------------


        private void lvProducts_Loaded(object sender, RoutedEventArgs e)
        {
            LoadListProduct();
        }

        private void btnInsertProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product add = GetObjectFromTextBoxes<Product>();

                if (add != null)
                {
                    productRepository.Create(add);
                    LoadListProduct();
                    MessageBox.Show("Add successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product rm = GetObjectFromTextBoxes<Product>();
                if (rm != null)
                {
                    productRepository.Delete(rm);
                    LoadListProduct();
                    MessageBox.Show("Delete successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product upp = GetObjectFromTextBoxes<Product>();

                if (upp != null)
                {
                    productRepository.Update(upp);
                    LoadListProduct();
                    MessageBox.Show("Update successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ;
                Order rm = GetObjectFromTextBoxes<Order>();
                OrderDetail oder = iorderRepository.GetById(rm.OrderId);
                iorderRepository.Delete(oder);
                if (rm != null)
                {
                    
                    orderRepository.Delete(rm);
                    LoadListOrder();
                    MessageBox.Show("Delete successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order upp = GetObjectFromTextBoxes<Order>();

                if (upp != null)
                {
                    orderRepository.Update(upp);
                    LoadListOrder();
                    MessageBox.Show("Update successfully");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

   

        private void LoadListOrder()
        {
            if (isAdmin)
            {
                lvOrders.ItemsSource = orderRepository.List();
            }
            else
            {
                Member mem = memberRepository.GetByString("Email", email);
                lvOrders.ItemsSource = orderRepository.ListByMember(mem.MemberId);
            }
           
        }
        private void lvOrders_Loaded(object sender, RoutedEventArgs e)
        {
            LoadListOrder();
        }

        private void Orderbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = GetObjectFromTextBoxes<Product>();
                Member mem = memberRepository.GetByString("Email",email);
                int quantity = 1;
                int curQuan = product.UnitsInStock??0;
                Order add = new Order
                {

                    MemberId = mem.MemberId,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(7),
                    ShippedDate = DateTime.Now,
                    Freight = 0,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            ProductId = product.ProductId,
                            UnitPrice = product.UnitPrice,
                            Quantity = quantity,
                            Discount = 0
                        }
                    }
                };

                if (add != null && curQuan>0)
                {
                    
                    orderRepository.Add(add);
                    productRepository.SetQuantity(product.ProductId,curQuan - quantity );
                    LoadListProduct();
                    MessageBox.Show("Add successfully");

                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Failed!");
            }
        }

     

        private void ListOrderDetail()
        {
            if (isAdmin)
            {
                lvOrderDetails.ItemsSource = iorderRepository.GetList();
            }
            else
            {
                Member mem = memberRepository.GetByString("Email", email);
                lvOrderDetails.ItemsSource = iorderRepository.GetListByMember(mem.MemberId);
            }
          
        }

        private void lvMember_Loaded(object sender, RoutedEventArgs e)
        {
            ListOrderDetail();
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            Member mem = memberRepository.GetByString("Email", email);
            EmailTextBox.Text = mem.Email;
            CompanyNameTextBox.Text = mem.CompanyName;
            CityTextBox.Text = mem.City;
            CountryTextBox.Text = mem.Country;
            PasswordBox.Password = mem.Password;
            ConfirmPasswordBox.Password = mem.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var member = memberRepository.GetByString("Email",email);
            if (member != null)
            {
                member.CompanyName = CompanyNameTextBox.Text;
                member.City = CityTextBox.Text;
                member.Country = CountryTextBox.Text;
                if (!string.IsNullOrEmpty(PasswordBox.Password))
                {
                    if (PasswordBox.Password == ConfirmPasswordBox.Password)
                    {
                        member.Password = PasswordBox.Password;
                    }
                    else
                    {
                        MessageBox.Show("Passwords do not match.");
                        return;
                    }
                }
                memberRepository.Update(member);
                MessageBox.Show("Profile updated successfully.");
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            ListOrderDetail();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logged out successfully. Returning to login screen.");
            var loginWindow = new LoginWindow();
            isAdmin = false;
            isMember = false;
            loginWindow.Show();
            this.Close();
        }
    }

  
}
