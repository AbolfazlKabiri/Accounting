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
    /// Interaction logic for W_PeopleGroup.xaml
    /// </summary>
    public partial class W_PeopleGroup : Window
    {
     
        #region Public Variables
        Models.PeopleGroup updateObject;
        #endregion
        public W_PeopleGroup()
        {
            InitializeComponent();
            updateObject = null;
            GetPeopleGroupList();
        }
       
        private void GetPeopleGroupList()
        {
            try
            {
                using (var controller = new Controllers.PeopleGroupController())
                {
                    var result = controller.GetPeopleGroup();
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
                Models.PeopleGroup newPeopleGroupObject = new Models.PeopleGroup
                {
                    Code = txtCode.Text,
                    Title = txtTitle.Text,
                   
                };
                var errors = newPeopleGroupObject.Validate(null);
                if (errors.Count() > 0)
                {
                    foreach (var item in errors)
                    {
                        MessageBox.Show(item.ErrorMessage);
                    }
                }
                else
                    using (var controller = new Controllers.PeopleGroupController())
                    {
                       result = controller.InsertPeopleGroup(newPeopleGroupObject);
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
                        GetPeopleGroupList();
                }
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }
       
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            updateObject =(Models.PeopleGroup) dtGroupAccount.SelectedItem;
            Models.ActionResultModelBinding result = null;
            try
            {
                var question = MessageBox.Show(Application.Current.FindResource("removeAccountWarning") as string, Application.Current.FindResource("warningTitle") as string, MessageBoxButton.YesNo);
                if(question == MessageBoxResult.Yes)
                {
                    if(updateObject != null)
                    {
                        using (var controller = new Controllers.PeopleGroupController())
                        {
                            result = controller.RemovePeopleGroup(updateObject,true);
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
                        GetPeopleGroupList();
                        updateObject = null;
                    }
                }
            }
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            updateObject = (Models.PeopleGroup) dtGroupAccount.SelectedItem;
            W_EditPeopleGroup edit = new W_EditPeopleGroup(updateObject);
            edit.ShowDialog();
            GetPeopleGroupList();
        }
       

        private void SearchTextChange(object sender, TextChangedEventArgs e)
        {
           
        }

        private void FASearchTx_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
