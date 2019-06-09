using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Accounting.Views.Shared
{
    /// <summary>
    /// Interaction logic for UC_Selector.xaml
    /// </summary>
    public partial class UC_Selector : UserControl
    {
        public Models.Common.SelectorType Type { get; set; }
        public UC_Selector()
        {
            InitializeComponent();
           
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<Models.PublicResultModel> selectedList = new List<Models.PublicResultModel>();
                 W_SelectorModal modal;
                switch (Type)
                {
                    
                    case Models.Common.SelectorType.GroupAccounts:
                         
                          modal = new W_SelectorModal(Models.Common.SelectorType.GroupAccounts, selectedList);
                          modal.ShowDialog();
                         if(selectedList.Count > 0)
                         {
                             lblValue.Content = selectedList[0].Title;
                             lblValue.Tag =new Tuple<long,string>(selectedList[0].Id,selectedList[0].Code);
                        }
                        break;
                    case Models.Common.SelectorType.KolAccounts:
                          
                          modal = new W_SelectorModal(Models.Common.SelectorType.KolAccounts, selectedList);
                          modal.ShowDialog();
                         if(selectedList.Count > 0)
                         {
                             lblValue.Content = selectedList[0].Title;
                             lblValue.Tag =new Tuple<long,string>(selectedList[0].Id,selectedList[0].Code);
                        }
                        break;
                    case Models.Common.SelectorType.TafsiliGroup:
                         modal = new W_SelectorModal(Models.Common.SelectorType.TafsiliGroup, selectedList,true);
                          modal.ShowDialog();
                         if(selectedList.Count > 0)
                         {
                             lblValue.Content =string.Join(",", selectedList.Select(c=> c.Title).ToList());
                             lblValue.Tag = selectedList.Select(c=> c.Id).ToList();
                        }
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
    }
}
