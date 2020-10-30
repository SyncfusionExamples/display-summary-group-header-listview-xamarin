# How to display a summary in the group header of Xamarin.Forms ListView (SfListView)?

You can display the summary of group items in the [GroupHeader](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.GroupHeaderItem.html) and update on the runtime changes in Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview).

You can also refer our artcile.

https://www.syncfusion.com/kb/12017/how-to-display-a-summary-in-the-group-header-of-xamarin-forms-listview-sflistview

**XAML**

Bind the [GroupResult.Items](https://help.syncfusion.com/cr/xamarin/Syncfusion.DataSource.Extensions.GroupResult.html#Syncfusion_DataSource_Extensions_GroupResult_Items) property to the **GroupHeader** label and define the converter to display the aggregate summary.
``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:listView="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             xmlns:dataSource="clr-namespace:Syncfusion.DataSource;assembly=Syncfusion.DataSource.Portable"
             x:Class="ListViewXamarin.MainPage">
…
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:SummaryConverter x:Key="Converter"/>
        </ResourceDictionary>
    </ContentPage.Resources>
    
    <ContentPage.Content>
        <Grid>
            <listView:SfListView x:Name="listView" ItemSize="70" GroupHeaderSize="60" ItemsSource="{Binding ContactsInfo}" ItemSpacing="0,0,5,0"> 
                <listView:SfListView.GroupHeaderTemplate>
                    <DataTemplate x:Name="GroupHeaderTemplate" >
                        <Grid BackgroundColor="#E4E4E4" Padding="10,0,0,0">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="Auto" />
                                <ColumnDefinition Width="*" />
                                <ColumnDefinition Width="50" />
                            </Grid.ColumnDefinitions>
                            <Label Text="Total: " FontSize="18" FontAttributes="Bold" VerticalOptions="Center"/>
                            <Label Text="{Binding Items, Converter={StaticResource Converter},StringFormat='{0:C2}'}" Grid.Column="1" FontSize="18"  FontAttributes="Bold" VerticalOptions="Center" HorizontalOptions="Start" Margin="0,0,0,0" />
                            <Grid Padding="5,5,5,5" Grid.Column="2">
                                <Label Text="+" FontSize="Large" HorizontalOptions="End" VerticalOptions="CenterAndExpand"/>
                                <Grid.GestureRecognizers>
                                    <TapGestureRecognizer Command="{Binding Path=BindingContext.AddItemCommand, Source={x:Reference listView}}" CommandParameter="{Binding .}"/>
                                </Grid.GestureRecognizers>
                            </Grid>
                        </Grid>
                    </DataTemplate>
                </listView:SfListView.GroupHeaderTemplate>
            </listView:SfListView>
        </Grid>
    </ContentPage.Content>
</ContentPage>
```
**C#**

Returns the total value of the group items based on the model property.
```c#
namespace ListViewXamarin
{
    public class SummaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            var items = value as IEnumerable;
            if(items != null)
            {
                var groupitems = items.ToList<object>().ToList<object>();
 
                if (groupitems != null)
                {
                    for (int i = 0; i < groupitems.Count; i++)
                    {
                        var contact = groupitems[i] as Contacts;
                        result += contact.Salary;
                    }
                }
            }
            return result;
        }
    }
}
```
**C#**

In the [ItemTapped](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.SfListView.html#Syncfusion_ListView_XForms_SfListView_ItemTapped) event, update the model property value of the item. To reflect the changes in the GroupHeader, refresh the GroupHeader by setting the [BindingContext](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.bindableobject.bindingcontext?view=xamarin-forms) as null. You can get the GroupResult of the tapped item from the [ListView.DataSource.DisplayItems](https://help.syncfusion.com/cr/xamarin/Syncfusion.DataSource.DataSource.html#Syncfusion_DataSource_DataSource_DisplayItems) property.
``` c#
namespace ListViewXamarin
{
   public class Behavior : Behavior<ContentPage>
    {
        SfListView ListView;
 
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            ...
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "ContactType",
            });
            ListView.ItemTapped += ListView_ItemTapped;
            base.OnAttachedTo(bindable);
        }
 
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
    }
}
```
You can get the [GroupHeaderItem](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.GroupHeaderItem.html) from the [VisualContainer](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.VisualContainer.html) and set the **GroupHeaderItem.BindingContext** and BindingContext of the element loaded in the GroupHeader to null. You can access the VisualContainer using the [GetVisualContainer](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.Control.Helpers.SfListViewHelper.html#Syncfusion_ListView_XForms_Control_Helpers_SfListViewHelper_GetVisualContainer_Syncfusion_ListView_XForms_SfListView_) method. And refresh the ListView using the RefreshView method to update the changes to the UI.

**Output**

![GroupHeaderSummary](https://github.com/SyncfusionExamples/display-summary-group-header-listview-xamarin/blob/main/ScreenShots/GroupHeaderSummary.gif)
