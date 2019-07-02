using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_FiscalYear.xaml
    /// </summary>
    public partial class W_FiscalYear : Window
    {
        public W_FiscalYear()
        {
            InitializeComponent();
            GetFiscalYears();
        }
          private void GetFiscalYears()
        {
            try
            {
                using (var controller = new Controllers.FiscalYearController())
                {
                    var result = controller.GetFiscalYears();
                    dtGroupAccount.ItemsSource = result.Body;
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }
        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            Models.ActionResultModelBinding result = null;
            try
            {
                Models.FiscalYear newFiscalYear = new Models.FiscalYear
                {
                    Title = txtTitle.Text,
                    StartDate = txtStartDate.Text,
                    EndDate = txtEndDate.Text,
                    Taxable = chkTaxable.IsChecked.Value,
                    Taxes = Convert.ToDecimal(string.IsNullOrEmpty(txtTaxes.Text) ? "0":txtTaxes.Text),
                    Duties = Convert.ToDecimal(string.IsNullOrEmpty(txtDuties.Text) ? "0":txtDuties.Text),
                    IsActive = chkIsInactive.IsChecked.Value ? false:true,
                    
                };

               /// var errors = newFiscalYear.Validate(null);
                //if (errors.Count() > 0)
                //{
                //    foreach (var item in errors)
                //    {
                //        MessageBox.Show(item.ErrorMessage);
                //    }
                //}
                //else
                    using (var controller = new Controllers.FiscalYearController())
                    {
                        result = controller.InsertFiscalYear(newFiscalYear);
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
                        GetFiscalYears();
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void removeButton_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void updateButton_Click(object sender, MouseButtonEventArgs e)
        {
            var updateObject = (Models.FiscalYear) dtGroupAccount.SelectedItem;
            W_EditFiscalYear edit = new W_EditFiscalYear(updateObject);
            edit.ShowDialog();
            GetFiscalYears();
        }

        private void SearchTextChange(object sender, TextChangedEventArgs e)
        {

        }

        private void FASearchTx_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
