using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_MoinAccount.xaml
    /// </summary>
    public partial class W_MoinAccount : Window
    {
        public W_MoinAccount()
        {
            InitializeComponent();
            kolSelector.Type = Models.Common.SelectorType.KolAccounts;
            tafsiliGroupSelector.Type = Models.Common.SelectorType.TafsiliGroup;
            FillNatureComboBox();
            GetMoinAccounts();
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
        private void GetMoinAccounts()
        {
            try
            {
                using (var controller = new Controllers.MoinController())
                {
                    var result = controller.GetMoinAccounts();
                    dtGroupAccount.ItemsSource = result.Body;
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }
        private void TxtCode_OnGotFocus(object sender, RoutedEventArgs e)
        {
             if (kolSelector.lblValue.Tag != null)
            {
                txtParentCode.Text = ((Tuple<long, string>) kolSelector.lblValue.Tag).Item2;
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
                Models.Moin newMoinAccountObject = new Models.Moin
                {
                    Code = txtCode.Text,
                    Title = txtTitle.Text,
                    NatureId = cmbNature.SelectedValue == null ? default(short) : (short)cmbNature.SelectedValue,
                    IsDefault = chkIsDefault.IsChecked.Value,
                    IsPermanent = chkIsTemprory.IsChecked.Value ? false:true,
                    KolId = kolSelector.lblValue.Tag != null ? (int)((Tuple<long,string>) kolSelector.lblValue.Tag).Item1:0,
                    
                };
                newMoinAccountObject.SetDataTable(tafsiliGroupSelector.lblValue.Tag != null ? (List<long>)tafsiliGroupSelector.lblValue.Tag : new List<long>());
                var errors = newMoinAccountObject.Validate(null);
                if (errors.Count() > 0)
                {
                    foreach (var item in errors)
                    {
                        MessageBox.Show(item.ErrorMessage);
                    }
                }
                else
                    using (var controller = new Controllers.MoinController())
                    {
                        result = controller.InsertMoinAccount(newMoinAccountObject);
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
                        GetMoinAccounts();
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void removeButton_Click(object sender, MouseButtonEventArgs e)
        {
            var  updateObject =(Models.Moin) dtGroupAccount.SelectedItem;
            Models.ActionResultModelBinding result = null;
            try
            {
                var question = MessageBox.Show(Application.Current.FindResource("removeAccountWarning") as string, Application.Current.FindResource("warningTitle") as string, MessageBoxButton.YesNo);
                if(question == MessageBoxResult.Yes)
                {
                    if(updateObject != null)
                    {
                        using (var controller = new Controllers.MoinController())
                        {
                            result = controller.RemoveMoinAccount(updateObject,true);
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
                        GetMoinAccounts();
                      
                    }
                }
            }
        }

        private void updateButton_Click(object sender, MouseButtonEventArgs e)
        {
             
            
            try
            {
                 var  updateObject =(Models.Moin) dtGroupAccount.SelectedItem;
                 W_EditMoin moinUpdateInstance = new W_EditMoin(updateObject);
                moinUpdateInstance.ShowDialog();
                GetMoinAccounts();
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
                return;
            }
            
        }

        private void FASearchTx_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            W_TafsiliGroupAccount newTafsiliGroupAcc = new W_TafsiliGroupAccount();
            newTafsiliGroupAcc.ShowDialog();
        }
    }
}
