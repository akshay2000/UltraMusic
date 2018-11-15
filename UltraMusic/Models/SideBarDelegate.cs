using System;
using AppKit;
using Foundation;

namespace UltraMusic.Models
{
    public class SideBarDelegate : NSOutlineViewDelegate
    {
        private SourceListView controller;

        public SideBarDelegate(SourceListView controller)
        {
            this.controller = controller;
        }

        public override bool ShouldEditTableColumn(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            return false;
        }

        public override NSCell GetCell(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            nint row = outlineView.RowForItem(item);
            return tableColumn.DataCellForRow(row);
        }

        public override bool IsGroupItem(NSOutlineView outlineView, NSObject item)
        {
            return ((SideBarItem)item).HasChildren;
        }

        public override NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            NSTableCellView view = null;

            // Is this a group item?
            if (((SideBarItem)item).HasChildren)
            {
                view = (NSTableCellView)outlineView.MakeView("HeaderCell", this);
            }
            else
            {
                view = (NSTableCellView)outlineView.MakeView("DataCell", this);
                view.ImageView.Image = ((SideBarItem)item).Icon;
            }

            // Initialize view
            view.TextField.StringValue = ((SideBarItem)item).Title;

            // Return new view
            return view;
        }

        public override bool ShouldSelectItem(NSOutlineView outlineView, Foundation.NSObject item)
        {
            return (outlineView.GetParent(item) != null);
        }

        public override void SelectionDidChange(NSNotification notification)
        {
            NSIndexSet selectedIndexes = controller.SelectedRows;

            // More than one item selected?
            if (selectedIndexes.Count > 1)
            {
                // Not handling this case
            }
            else
            {
                // Grab the item
                var item = controller.Data.ItemForRow((int)selectedIndexes.FirstIndex);

                // Was an item found?
                if (item != null)
                {
                    // Fire the clicked event for the item
                    item.RaiseClickedEvent();

                    // Inform caller of selection
                    controller.RaiseItemSelected(item);
                }
            }
        }
    }
}