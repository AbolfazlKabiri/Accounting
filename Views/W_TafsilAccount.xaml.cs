using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_TafsilAccount.xaml
    /// </summary>
    public partial class W_TafsilAccount : Window
    {
        public W_TafsilAccount()
        {
            InitializeComponent();
            FillEntityComboBoxes();
            peopleGroupSelector.Type = Models.Common.SelectorType.PeopleGroup;
            tafsiliGroupSelector.Type = Models.Common.SelectorType.TafsiliGroup;
        }
        private void FillEntityComboBoxes()
        {
            try
            {
                using (var controller = new Controllers.TafsilController())
                {
                    cmbEntityType.ItemsSource = controller.GetEntities().Body.ToList();
                    cmbEntityType.DisplayMemberPath = "Title";
                    cmbEntityType.SelectedValuePath = "Id";
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

        private void removeButton_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void updateButton_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            Models.ActionResultModelBinding result = null;
            try
            {
                Models.Tafsil newTafsilAccountObject = new Models.Tafsil
                {
                    Code = txtCode.Text,
                    Title = txtTitle.Text,
                    EntityTypeId = cmbEntityType.SelectedValue == null ? default(int) : (int)cmbEntityType.SelectedValue,
                    IsActive = chkInActive.IsChecked.Value ? false : true,
                    IsCostCenter = isCistCenter.IsChecked.Value ? true:false,
                    AccountTypeId = radioLegal.IsChecked.Value ? (short)1 : (short)2,
                    Address = txtAddress.Text,
                    EconomyCode = txtEconomyCode.Text,
                    MaxCredit = decimal.Parse(string.IsNullOrEmpty(txtMaxCreidit.Text) ? "0" : txtMaxCreidit.Text),
                    NationalCode = txtNationCode.Text,
                    Mobile = txtMobile.Text,
                    Tel = txtTel.Text,
                    TitleInAutomateDocument = txtTitleInDocument.Text,
                    
                };
                newTafsilAccountObject.TafsiliGroupBinding = Models.Common.SetDataTable(tafsiliGroupSelector.lblValue.Tag != null ? (List<long>)tafsiliGroupSelector.lblValue.Tag : new List<long>());
                newTafsilAccountObject.PeopleGroupBinding = Models.Common.SetDataTable(peopleGroupSelector.lblValue.Tag != null ? (List<long>)tafsiliGroupSelector.lblValue.Tag : new List<long>());
                var errors = newTafsilAccountObject.Validate(null);
                if (errors.Count() > 0)
                {
                    foreach (var item in errors)
                    {
                        MessageBox.Show(item.ErrorMessage);
                    }
                }
                else
                    using (var controller = new Controllers.TafsilController())
                    {
                        result = controller.InsertTafsil(newTafsilAccountObject);
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
                    if (result.Status == Models.ActionResult.Success) { }
                        // get Tafsil
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
