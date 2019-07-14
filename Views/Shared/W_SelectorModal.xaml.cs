using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Accounting.Views.Shared
{
    /// <summary>
    /// Interaction logic for UC_SelectorModal.xaml
    /// </summary>
    public partial class W_SelectorModal : Window
    {
        Models.Common.SelectorType selectorType;
        List<Models.PublicResultModel> selectedList;
        bool multiSelect;
        public W_SelectorModal(Models.Common.SelectorType _selectorType,List<Models.PublicResultModel> _selectedList,bool _multiSelect = false)
        {
            InitializeComponent();
            multiSelect = _multiSelect;
            selectorType = _selectorType;
            selectedList = _selectedList;
            LoadInfo();
            if(!_multiSelect & selectorType != Models.Common.SelectorType.TafsiliGroup)
            {
                selectionButtonArea.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadInfo(string search = null)
        {
            try
            {
                switch (selectorType)
                {
                    case Models.Common.SelectorType.GroupAccounts:
                        using (var controller = new Controllers.GroupController())
                        {
                            var result = controller.GetGroupAccounts(search:search);
                            dtSource.ItemsSource = result.Body;
                        }
                        break;
                    case Models.Common.SelectorType.KolAccounts:
                        using (var controller = new Controllers.KolController())
                        {
                            var result = controller.GetKolAccounts(search:search);
                            dtSource.ItemsSource = result.Body;
                        }
                        break;
                    case Models.Common.SelectorType.TafsiliGroup:
                        using (var controller = new Controllers.TafsiliGroupController())
                        {
                            var result = controller.GetTafsiliGroupAccounts(search:search);
                            dtSource.ItemsSource = result.Body;
                        }
                        break;
                    case Models.Common.SelectorType.PeopleGroup:
                         using (var controller = new Controllers.PeopleGroupController())
                        {
                            var result = controller.GetPeopleGroup();
                            dtSource.ItemsSource = result.Body;
                        }
                        break;
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
               
                    //if (txtSearch.Text.Length > 2)
                    //    LoadInfo(txtSearch.Text);
                    //else
                    //    LoadInfo(txtSearch.Text);
                
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }

        private void dtSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (selectorType)
                {
                    case Models.Common.SelectorType.GroupAccounts:
                        var group = (Models.Group)dtSource.SelectedItem;
                        selectedList.Add(new Models.PublicResultModel { Id = group.Id, Title = group.Title,Code = group.Code});
                        Close();
                        break;
                    case Models.Common.SelectorType.KolAccounts:
                         var kol = (Models.Kol)dtSource.SelectedItem;
                        selectedList.Add(new Models.PublicResultModel { Id = kol.Id, Title = kol.Title,Code =$"{kol.GroupCode}{kol.Code}"});
                        Close();
                        break;
                    
                    default:
                        break;
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if(selectorType == Models.Common.SelectorType.TafsiliGroup)
            {
            var list =  (List<Models.TafsiliGroup>)dtSource.ItemsSource;
            selectedList.AddRange(
                    list.Where(c=> c.Selected == true).Select(c=> new Models.PublicResultModel {

                        Id = c.Id,
                        Title = c.Title,
                        Code = c.Code
                    })
                );
                if(selectedList.Count > 1)
                {
                    MessageBox.Show("فقط مجاز به انتخاب 1 گروه تفصیل هستید");
                    selectedList.Clear();
                    list.ForEach(i=> i.Selected = false);
                    return;
                }
            }
            else if(selectorType == Models.Common.SelectorType.PeopleGroup)
            {
                var list =  (List<Models.PeopleGroup>)dtSource.ItemsSource;
                 selectedList.AddRange(
                 list.Where(c=> c.Selected == true).Select(c=> new Models.PublicResultModel {

                        Id = c.Id,
                        Title = c.Title,
                        Code = c.Code
                    })
                );
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
