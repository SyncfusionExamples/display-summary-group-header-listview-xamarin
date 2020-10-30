using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
   public class Behavior : Behavior<ContentPage>
    {
        #region Fields

        SfListView ListView;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            ListView.DataSource.SortDescriptors.Add(new SortDescriptor()
            {
                PropertyName = "ContactType",
                Direction = ListSortDirection.Ascending
            });
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "ContactType",
            });
            ListView.ItemTapped += ListView_ItemTapped;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView.ItemTapped -= ListView_ItemTapped;
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Methods
        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemType == ItemType.Record)
            {
                (e.ItemData as Contacts).Salary += 100;

                var groupResult = this.GetGroup(e.ItemData);

                this.RefreshGroupHeader(groupResult);
                ListView.RefreshView();
            }
        }

        public GroupResult GetGroup(object itemData)
        {
            GroupResult itemGroup = null;

            foreach (var item in this.ListView.DataSource.DisplayItems)
            {
                if (item == itemData)
                    break;

                if (item is GroupResult)
                    itemGroup = item as GroupResult;
            }
            return itemGroup;
        }

        private void RefreshGroupHeader(GroupResult group)
        {
            foreach (var item in ListView.GetVisualContainer().Children)
            {
                if (item.BindingContext == group)
                {
                    item.BindingContext = null;
                    (item as GroupHeaderItem).Content.BindingContext = null;
                }
            }
        }
        #endregion
    }
}
