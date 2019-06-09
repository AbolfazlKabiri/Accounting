using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_TafsiliGroupAccount.xaml
    /// </summary>
    public partial class W_TafsiliGroupAccount : Window
    {
        public W_TafsiliGroupAccount()
        {
            InitializeComponent();
            GetTafsiliGroupAccounts();
        }
        private void GetTafsiliGroupAccounts()
        {
            try
            {
                using (var controller = new Controllers.TafsiliGroupController())
                {
                    var result = controller.GetTafsiliGroupAccounts(search:null);
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
                Models.TafsiliGroup newTafsiliGroupAccountObject = new Models.TafsiliGroup
                {
                    Code = txtCode.Text,
                    Title = txtTitle.Text,
                    Editable = chkEditable.IsChecked.Value
                };
                var errors = newTafsiliGroupAccountObject.Validate(null);
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
                       result = controller.InsertTafsiliGroupAccount(newTafsiliGroupAccountObject);
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
                        GetTafsiliGroupAccounts();
                }
            }
        }

        private void removeButton_Click(object sender, MouseButtonEventArgs e)
        {
            var  updateObject =(Models.TafsiliGroup) dtGroupAccount.SelectedItem;
            Models.ActionResultModelBinding result = null;
            try
            {
                var question = MessageBox.Show(Application.Current.FindResource("removeAccountWarning") as string, Application.Current.FindResource("warningTitle") as string, MessageBoxButton.YesNo);
                if(question == MessageBoxResult.Yes)
                {
                    if(updateObject != null)
                    {
                        using (var controller = new Controllers.TafsiliGroupController())
                        {
                            result = controller.RemoveTafsiliGroupAccount(updateObject,true);
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
                        GetTafsiliGroupAccounts();
                      
                    }
                }
            }
        }

        private void updateButton_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
             var  updateObject =(Models.TafsiliGroup) dtGroupAccount.SelectedItem;
             W_EditTafsiliGroup editTafsilGroupInstance = new W_EditTafsiliGroup(updateObject);
             editTafsilGroupInstance.ShowDialog();
                GetTafsiliGroupAccounts();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SearchTextChange(object sender, TextChangedEventArgs e)
        {

        }

        private void FASearchTx_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
