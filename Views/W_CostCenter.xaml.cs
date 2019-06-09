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

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_CostCenter.xaml
    /// </summary>
    public partial class W_CostCenter : Window
    {
          #region Public Variables
        Models.CostCenter updateObject;
        #endregion
        public W_CostCenter()
        {
            InitializeComponent();
            updateObject = null;
            GetCostCenterList();
        }
       
        private void GetCostCenterList()
        {
            try
            {
                using (var controller = new Controllers.CostCenterController())
                {
                    var result = controller.GetCostCenter();
                    dtGroupAccount.ItemsSource = result.Body;
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Models.ActionResultModelBinding result = null;
            try
            {
                Models.CostCenter newCostCenterObject = new Models.CostCenter
                {
                    Code = txtCode.Text,
                    Title = txtTitle.Text,
                   
                };
                var errors = newCostCenterObject.Validate(null);
                if (errors.Count() > 0)
                {
                    foreach (var item in errors)
                    {
                        MessageBox.Show(item.ErrorMessage);
                    }
                }
                else
                    using (var controller = new Controllers.CostCenterController())
                    {
                       result = controller.InsertCostCenter(newCostCenterObject);
                    }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
                return;
            }
            finally
            {
                if (result != null)
                {
                    MessageBox.Show(result.Message);
                    if (result.Status == Models.ActionResult.Success)
                        GetCostCenterList();
                }
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }
       
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            updateObject =(Models.CostCenter) dtGroupAccount.SelectedItem;
            Models.ActionResultModelBinding result = null;
            try
            {
                var question = MessageBox.Show(Application.Current.FindResource("removeAccountWarning") as string, Application.Current.FindResource("warningTitle") as string, MessageBoxButton.YesNo);
                if(question == MessageBoxResult.Yes)
                {
                    if(updateObject != null)
                    {
                        using (var controller = new Controllers.CostCenterController())
                        {
                            result = controller.RemoveCostCenter(updateObject,true);
                        }
                    }
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
                return;
            }
            finally
            {
                if (result != null)
                {
                    MessageBox.Show(result.Message);
                    if (result.Status == Models.ActionResult.Success)
                    {
                        GetCostCenterList();
                        updateObject = null;
                    }
                }
            }
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            updateObject = (Models.CostCenter) dtGroupAccount.SelectedItem;
            W_EditCostCenter edit = new W_EditCostCenter(updateObject);
            edit.ShowDialog();
            GetCostCenterList();
        }
       

        private void SearchTextChange(object sender, TextChangedEventArgs e)
        {
           
        }

        private void FASearchTx_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
