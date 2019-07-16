using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_EditTafsilAccountTemplate.xaml
    /// </summary>
    public partial class W_EditTafsilAccountTemplate : Window
    {
        Models.TafsilAccountTemplateModelBinding tafsilAccountTemplate;
        public W_EditTafsilAccountTemplate(Models.TafsilAccountTemplateModelBinding _tafsilAccountTemplate)
        {
            InitializeComponent();
            tafsilAccountTemplate = _tafsilAccountTemplate;
            cmbEntityType.SelectedValue = tafsilAccountTemplate.EntityId;
            tafsiliGroupSelector.Tag = new List<long> { tafsilAccountTemplate.TafsiliGroupId};
            tafsiliGroupSelector.lblValue.Content = tafsilAccountTemplate.TafsiliGroupTitle;
            FillEntityComboBoxes();
            tafsiliGroupSelector.MultiSelect = false;
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

         private void Button_Click(object sender, RoutedEventArgs e)
        {
             Models.ActionResultModelBinding result = null;
            try
            {
                Models.TafsilAccountTemplateModelBinding newAccountObject = new Models.TafsilAccountTemplateModelBinding
                {
                    Id = tafsilAccountTemplate.Id,
                    EntityId = cmbEntityType.SelectedValue == null ? default(int) : (int)cmbEntityType.SelectedValue,
                    TafsiliGroupId = tafsiliGroupSelector.lblValue.Tag != null ? ((List<long>)tafsiliGroupSelector.lblValue.Tag).FirstOrDefault() : 0
                    
                };
               
                var errors = newAccountObject.Validate(null);
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
                        result = controller.UpdateTafsilAccountTemplate(newAccountObject);
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
                        Close();
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            W_TafsiliGroupAccount newTafsiliGroupAcc = new W_TafsiliGroupAccount();
            newTafsiliGroupAcc.ShowDialog();
        }
        
    }
}
