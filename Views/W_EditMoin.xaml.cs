using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_EditMoin.xaml
    /// </summary>
    public partial class W_EditMoin : Window
    {
        Models.Moin model;
        public W_EditMoin(Models.Moin _model)
        {
            InitializeComponent();
            FillNatureComboBox();
            kolSelector.Type = Models.Common.SelectorType.KolAccounts;
            tafsiliGroupSelector.Type = Models.Common.SelectorType.TafsiliGroup;
            model = _model;
            kolSelector.lblValue.Content = _model.KolTitle;
            kolSelector.lblValue.Tag = new Tuple<long,string>(model.KolId,model.KolCode);
            tafsiliGroupSelector.lblValue.Content = model.TafsiliGroupBindingString;
            tafsiliGroupSelector.lblValue.Tag = model.TafsiliGroupBinding;
            txtParentCode.Text = model.KolCode;
            txtCode.Text = model.Code;
            txtTitle.Text = model.Title;
            cmbNature.SelectedValue = model.NatureId;
            chkIsDefault.IsChecked = model.IsDefault; 
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
       
        private void TxtCode_OnGotFocus(object sender, RoutedEventArgs e)
        {
             if (kolSelector.lblValue.Tag != null)
            {
                txtParentCode.Text = ((Tuple<long, string>) kolSelector.lblValue.Tag).Item2;
            }
        }

    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             Models.ActionResultModelBinding result = null;
            try
            {
                Models.Moin newMoinAccountObject = new Models.Moin
                {
                    Id = model.Id,
                    Code = txtCode.Text,
                    Title = txtTitle.Text,
                    NatureId = cmbNature.SelectedValue == null ? default(short) : (short)cmbNature.SelectedValue,
                    IsDefault = chkIsDefault.IsChecked.Value,
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
                        result = controller.UpdateMoinAccount(newMoinAccountObject);
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
