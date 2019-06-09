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
    /// Interaction logic for W_EditTafsiliGroup.xaml
    /// </summary>
    public partial class W_EditTafsiliGroup : Window
    {
        private Models.TafsiliGroup _model;
        public W_EditTafsiliGroup(Models.TafsiliGroup model)
        {
            InitializeComponent();
            
            _model = model;
            if (model != null)
            {
                txtCode.Text = model.Code;
                txtTitle.Text = model.Title;
                chkIsDefault.IsChecked = model.Editable;
                
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
                  
                    _model.Editable = chkIsDefault.IsChecked.Value;

                    var errors = _model.Validate(null);
                    if (errors.Count() > 0)
                    {
                        foreach (var item in errors)
                        {
                            MessageBox.Show(item.ErrorMessage);
                        }
                    }
                    else
                        using (var controller = new Controllers.TafsiliGroupController())
                        {
                            result = controller.UpdateTafsiliGroupAccount(_model);
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
