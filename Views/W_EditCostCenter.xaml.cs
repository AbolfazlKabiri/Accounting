using System;
using System.Linq;
using System.Windows;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_EditCostCenter.xaml
    /// </summary>
    public partial class W_EditCostCenter : Window
    {
         private Models.CostCenter _model;
        public W_EditCostCenter(Models.CostCenter model)
        {
            InitializeComponent();
            
            _model = model;
            if (model != null)
            {
                txtCode.Text = model.Code;

                txtTitle.Text = model.Title;
               
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
                    _model.Code = txtCode.Text;
                    _model.Title = txtTitle.Text;
                    

                    var errors = _model.Validate(null);
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
                            result = controller.UpdateCostCenter(_model);
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


