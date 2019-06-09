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
    /// Interaction logic for W_KolAccount.xaml
    /// </summary>
    public partial class W_KolAccount : Window
    {
        public W_KolAccount()
        {
            InitializeComponent();
            FillNatureComboBox();
            GetKolAccounts();
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
        private void GetKolAccounts()
        {
            try
            {
                using (var controller = new Controllers.KolController())
                {
                    var result = controller.GetKolAccounts();
                    dtGroupAccount.ItemsSource = result.Body;
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }
        private void SearchTextChange(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Models.ActionResultModelBinding result = null;
            try
            {
                Models.Kol newGroupAccountObject = new Models.Kol()
                {
                    Code = txtCode.Text,
                    Title = txtTitle.Text,
                    NatureId = cmbNature.SelectedValue == null ? default(short) : (short)cmbNature.SelectedValue,
                    IsDefault = chkIsDefault.IsChecked.Value,
                    GroupId =selector.lblValue.Tag != null ? (int)((Tuple<long,string>) selector.lblValue.Tag).Item1:0
                };
                var errors = newGroupAccountObject.Validate(null);
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
                        result = controller.InsertKolAccount(newGroupAccountObject);
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
                        GetKolAccounts();
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void FASearchTx_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void removeButton_Click(object sender, MouseButtonEventArgs e)
        {
            var updateObject =(Models.Kol) dtGroupAccount.SelectedItem;
            Models.ActionResultModelBinding result = null;
            try
            {
                var question = MessageBox.Show(Application.Current.FindResource("removeAccountWarning") as string, Application.Current.FindResource("warningTitle") as string, MessageBoxButton.YesNo);
                if(question == MessageBoxResult.Yes)
                {
                    if(updateObject != null)
                    {
                        using (var controller = new Controllers.KolController())
                        {
                            result = controller.RemoveKolAccount(updateObject,true);
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
                        GetKolAccounts();
                        updateObject = null;
                    }
                }
            }
        }

        private void updateButton_Click(object sender, MouseButtonEventArgs e)
        {
            var updateObject = (Models.Kol) dtGroupAccount.SelectedItem;
            W_EditKol edit = new W_EditKol(updateObject);
            edit.ShowDialog();
            GetKolAccounts();
        }

        private void TxtCode_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (selector.lblValue.Tag != null)
            {
                txtGroupCode.Text = ((Tuple<long, string>) selector.lblValue.Tag).Item2;
            }
        }

        private void Selector_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
