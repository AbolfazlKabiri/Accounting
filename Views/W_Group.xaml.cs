using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Shapes;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_Group.xaml
    /// </summary>
    public partial class W_Group : Window
    {
        #region Public Variables
        Models.Group updateObject;
        #endregion
        public W_Group()
        {
            InitializeComponent();
            updateObject = null;
            FillNatureComboBox();
            GetGroupAccounts();
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
        private void GetGroupAccounts()
        {
            try
            {
                using (var controller = new Controllers.GroupController())
                {
                    var result = controller.GetGroupAccounts();
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
                Models.Group newGroupAccountObject = new Models.Group
                {
                    Code = txtCode.Text,
                    Title = txtTitle.Text,
                    NatureId = cmbNature.SelectedValue == null ? default(short) : (short)cmbNature.SelectedValue,
                    IsDefault = chkIsDefault.IsChecked.Value
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
                    using (var controller = new Controllers.GroupController())
                    {
                       result = controller.InsertGroup(newGroupAccountObject);
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
                        GetGroupAccounts();
                }
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }
       
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            updateObject =(Models.Group) dtGroupAccount.SelectedItem;
            Models.ActionResultModelBinding result = null;
            try
            {
                var question = MessageBox.Show(Application.Current.FindResource("removeAccountWarning") as string, Application.Current.FindResource("warningTitle") as string, MessageBoxButton.YesNo);
                if(question == MessageBoxResult.Yes)
                {
                    if(updateObject != null)
                    {
                        using (var controller = new Controllers.GroupController())
                        {
                            result = controller.RemoveGroup(updateObject,true);
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
                        GetGroupAccounts();
                        updateObject = null;
                    }
                }
            }
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            updateObject = (Models.Group) dtGroupAccount.SelectedItem;
            W_EditGroup edit = new W_EditGroup(updateObject);
            edit.ShowDialog();
            GetGroupAccounts();
        }
       

        private void SearchTextChange(object sender, TextChangedEventArgs e)
        {
           
        }

        private void FASearchTx_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
