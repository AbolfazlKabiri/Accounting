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
    /// Interaction logic for W_EditFiscalYear.xaml
    /// </summary>
    public partial class W_EditFiscalYear : Window
    {
        private Models.FiscalYear _model;
        public W_EditFiscalYear(Models.FiscalYear model)
        {
            InitializeComponent();
           
            _model = model;
            if (model != null)
            {
                txtId.Text = model.Id.ToString();
                txtTitle.Text = model.Title;
                txtStartDate.Text = model.StartDate;
                txtEndDate.Text = model.EndDate;
                txtTaxes.Text = model.Taxes.ToString("N2");
                txtDuties.Text = model.Duties.ToString("N2");
                chkIsInactive.IsChecked = model.IsActive ? false:true;
                chkTaxable.IsChecked = model.Taxable;
            }
        }
        
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Models.ActionResultModelBinding result = null;
            try
            {

                if (_model != null)
                {
                    _model.Title = txtTitle.Text;
                    _model.StartDate = txtStartDate.Text;
                    _model.EndDate = txtEndDate.Text;
                    _model.Taxable = chkTaxable.IsChecked.Value;
                    _model.Taxes = Convert.ToDecimal(string.IsNullOrEmpty(txtTaxes.Text) ? "0":txtTaxes.Text);
                    _model.Duties = Convert.ToDecimal(string.IsNullOrEmpty(txtDuties.Text) ? "0":txtDuties.Text);
                    _model.IsActive = chkIsInactive.IsChecked.Value ? false:true;

                    //var errors = _model.Validate(null);
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
                            result = controller.UpdateFiscalYear(_model);
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

                    Close();
                }
            }
        }
    }
}