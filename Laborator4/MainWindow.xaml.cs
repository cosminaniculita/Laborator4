using AutoLotModel;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Laborator4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        private const ActionState nothing = ActionState.Nothing;
        private ActionState action = nothing;
        CollectionViewSource customerVSource;
        private object ordersDataGrid;
        private readonly object ctx;
        private readonly CollectionViewSource customerOrdersVSource;

        public MainWindow(CollectionViewSource customerOrdersVSource)
        {
            this.customerOrdersVSource = customerOrdersVSource;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            customerVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            //using System.Data.Entity;
            customerVSource =
           ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            ///customerVSource.Source;

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToNext();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToPrevious();
        }

        private void SaveCustomers()
        {
            customerVSource customer = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    customer = new custIdTextBox()
                    {
                        FirstName = firstNameTextBox.Text.Trim(),
                        LastName = lastNameTextBox.Text.Trim(),
                    };
                    //adaugam entitatea nou creata in context
                    ///ctx.Customers.Add(customer);
                    ///customerVSource.View.Refresh();
                    //salvam modificarile:
                    //ctx.SaveChanges();
                }
                //using System.Data;
                catch (System.Data.DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    ///customer = (Customers)customerDataGrid.SelectedItem;
                    ///customer.FirstName = firstNameTextBox.Text.Trim();
                    //customer.LastName = lastNameTextBox.Text.Trim();
                    ///salvam modificarile
                    ///ctx.SaveChanges();
                }
                catch (System.Data.DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    ///customer =(custIdTextBox)customerDataGrid.text;
                    ///ctx.custIdTextBox.Remove(customer);
                    ///ctx.SaveChanges();
                }
                catch (System.Data.DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ///customerVSource.View.Refresh();
            }

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = (TabItem)tbAutoLotModel.SelectedItem;

            switch (ti.Header)
            {
                case "Customers":
                    ///SaveCustomers();
                    break;
                case "Inventory":
                    ///SaveInventory();
                    break;
                case "Orders":
                    break;
            }
            ///ReInitialize();
        }
        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void SaveOrders()
        {
            Order order = null;
            if (action == ActionState.New)
            {
                try
                {
                    Customer customer = (AutoLotModel.Customer)cmbCustomers.SelectedItem;
                    Inventory inventory = (Inventory)cmbInventory.SelectedItem;
                    //instantiem Order entity 
                    order = new Order()
                    {

                        CustId = customer.CustId,
                        CarId = inventory.CarId
                    };
                    //adaugam entitatea nou creata in context
                    ///ctx.Orders.Add(order);
                    //salvam modificarile
                    ///ctx.SaveChanges();
                    ///BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                dynamic selectedOrder = ordersDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedOrder.OrderId;
                    var editedOrder = ctx.Orders.FirstOrDefault(s => s.OrderId == curr_id);
                    if (editedOrder != null)
                    {
                        editedOrder.CustId = int.Parse(cmbCustomers.SelectedValue.ToString());
                        editedOrder.CarId = Convert.ToInt32(cmbInventory.SelectedValue.ToString());
                        //salvam modificarile
                        ///ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ///BindDataGrid();
                // pozitionarea pe item-ul curent
                customerOrdersVSource.View.MoveCurrentTo(selectedOrder);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedOrder = ordersDataGrid.SelectedItem;
                    dynamic orderId = selectedOrder.OrderId;
                    int curr_id = orderId;
                    var deletedOrder = ctx.Orders.FirstOrDefault(s => s.OrderId == curr_id);
                    if (deletedOrder != null)
                    {
                        ///ctx.Orders.Remove(deletedOrder);
                        ///ctx.SaveChanges();
                        MessageBox.Show("Order Deleted Successfully", "Message");
                        ///BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
    private void BindDataGrid()
    {
        object ctx = null;
        var queryOrder = from ord in ctx.Orders
                         join cust in ctx.Customers on ord.CustId equals
                         cust.CustId
                         join inv in ctx.Inventories on ord.CarId
             equals inv.CarId
                         select new
                         {
                             ord.OrderId,
                             ord.CarId,
                             ord.CustId,
                             cust.FirstName,
                             cust.LastName,
                             inv.Make,
                             inv.Color
                         };
        ///customerOrdersVSource.Source = queryOrder.ToList();
    }
}