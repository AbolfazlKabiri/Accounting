using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_TafsilAccountTemplate.xaml
    /// </summary>
    public partial class W_TafsilAccountTemplate : Window
    {
        public W_TafsilAccountTemplate()
        {
            InitializeComponent();
            FillEntityComboBoxes();
            GetTemplates();
            tafsiliGroupSelector.MultiSelect = false;
            tafsiliGroupSelector.Type = Models.Common.SelectorType.TafsiliGroup;
        }
           private void GetTemplates()
        {
            try
            {
                using (var controller = new Controllers.TafsilAccountTemplateController())
                {
                    var result = controller.GetTafsilAccountTemplateList();
                    dtGroupAccount.ItemsSource = result.Body;
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
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
        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
             Models.ActionResultModelBinding result = null;
            try
            {
                Models.TafsilAccountTemplateModelBinding newTafsilAccountTemplateObject = new Models.TafsilAccountTemplateModelBinding
                {
                    EntityId = cmbEntityType.SelectedValue == null ? default(int) : (int)cmbEntityType.SelectedValue,
                    TafsiliGroupId = tafsiliGroupSelector.lblValue.Tag != null ? ((List<long>)tafsiliGroupSelector.lblValue.Tag).FirstOrDefault() : 0
                };
               
                var errors = newTafsilAccountTemplateObject.Validate(null);
                if (errors.Count() > 0)
                {
                    foreach (var item in errors)
                    {
                        MessageBox.Show(item.ErrorMessage);
                    }
                }
                else
                    using (var controller = new Controllers.TafsilAccountTemplateController())
                    {
                        result = controller.InsertTafsilAccountTemplate(newTafsilAccountTemplateObject);
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
                       GetTemplates();
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CodeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void edit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void delete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
