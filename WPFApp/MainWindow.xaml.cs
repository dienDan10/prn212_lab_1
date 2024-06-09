using BusinessObjects;
using Service;
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

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IProductService productService;
        private ICategoryService categoryService;

        public MainWindow()
        {
            InitializeComponent();
            productService = new ProductService();
            categoryService = new CategoryService();
        }

        private void LoadCategoryList()
        {
            try
            {

                var catList = categoryService.GetCategories();
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
                cboCategory.SelectedIndex = 0;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of category");
            }
        }

        private void LoadProductList()
        {
            try
            {

                var productList = productService.GetProducts();
                dgData.ItemsSource = null;
                dgData.ItemsSource = productList;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of product");
            } finally
            {
                ResetInput();
            }
        }

        private void ResetInput()
        {
            txtPrice.Text = string.Empty;
            txtProductID.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtUnitsInStock.Text = string.Empty;
            txtPrice.Text = string.Empty;
            cboCategory.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void btnCreate_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.UnitPrice = int.Parse(txtPrice.Text);
                product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                product.CategoryId = int.Parse(cboCategory.SelectedValue.ToString());
                productService.SaveProduct(product);

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail to create product");
            } finally
            {
                LoadProductList();
            }
        }

        private void btnUpdate_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtProductID.Text != string.Empty)
                {
                    Product product = new Product();
                    product.ProductId = int.Parse(txtProductID.Text);
                    product.ProductName = txtProductName.Text.Trim();
                    product.UnitPrice = int.Parse(txtPrice.Text.Trim());
                    product.UnitsInStock = short.Parse(txtUnitsInStock.Text.Trim());
                    product.CategoryId = int.Parse(cboCategory.SelectedValue.ToString());
                    productService.UpdateProduct(product);
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Update product fail");
            }finally
            {
                LoadProductList();
            }

        }

        private void btnDelete_Clicked(object sender, RoutedEventArgs e)
        {

            MessageBoxResult res = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                // get product id
                int id = int.Parse(txtProductID.Text.Trim());
                Product product = new Product();
                product.ProductId = id;
                productService.DeleteProduct(product);

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete product fail");
            } finally
            {
                LoadProductList() ;
            }
        }

        private void btnClose_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.ItemsSource == null)
            {
                return;
            }
            // get dataGrid
            DataGrid dataGrid = sender as DataGrid;
            // get selected row
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgData.SelectedIndex);
            // get the cell contain the index
            DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            string id = ((TextBlock)cell.Content).Text.ToString();
            Product product = productService.GetProductById(int.Parse(id));
            txtProductID.Text = product.ProductId.ToString();
            txtProductName.Text = product.ProductName;
            txtPrice.Text = product.UnitPrice.ToString();
            txtUnitsInStock.Text = product.UnitsInStock.ToString();
            cboCategory.SelectedValue = product.CategoryId;

        }
    }
}