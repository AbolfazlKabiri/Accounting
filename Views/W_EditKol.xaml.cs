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
    /// Interaction logic for W_EditKol.xaml
    /// </summary>
    public partial class W_EditKol : Window
    {
        private Models.Kol _model;
        public W_EditKol(Models.Kol model)
        {
            InitializeComponent();
            FillNatureComboBox();
            _model = model;
            if (model != null)
            {
                txtCode.Text = model.Code;
                txtGroupCode.Text =model.GroupCode;
                txtTitle.Text = model.Title;
                chkIsDefault.IsChecked = model.IsDefault;
                cmbNature.SelectedValue = model.NatureId;
                selector.lblValue.Content = model.GroupTitle;
                selector.lblValue.Tag = new Tuple<long,string>(model.GroupId,model.GroupCode);
            }
        }
        private void FillNatureComboBox()
        {
            try
            {
                using (var controller = new Controllers.NatureController())
                {
                    cmbNature.ItemsSource = controller.GetNatures();
                    cmbNature.DisplayMemberPath = "Title";
                    cmbNature.SelectedValuePath = "Id";
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
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
                _model.Code = txtCode.Text;
                _model.Title = txtTitle.Text;
                _model.NatureId = cmbNature.SelectedValue == null ? default(short) : (short) cmbNature.SelectedValue;
                _model.IsDefault = chkIsDefault.IsChecked.Value;
                _model.GroupId = selector.lblValue.Tag != null
                    ? (int) ((Tuple<long, string>) selector.lblValue.Tag).Item1
                    : 0;
                var errors = _model.Validate(null);
                if (errors.Count() > 0)
                {
                    foreach (var item in errors)
                    {
                        MessageBox.Show(item.ErrorMessage);
                    }
                }
                else
                    using (var controller = new Controllers.KolController())
                    {
                        result = controller.UpdateKolAccount(_model);
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

        private void Selector_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selector.lblValue.Tag != null)
            {
                txtGroupCode.Text = ((Tuple<long, string>) selector.lblValue.Tag).Item2;
            }
        }
    }
}
