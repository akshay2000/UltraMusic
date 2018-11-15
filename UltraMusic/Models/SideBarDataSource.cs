using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace UltraMusic.Models
{
    public class SideBarDataSource : NSOutlineViewDataSource
    {
        private readonly SourceListView controller;

        public List<SideBarItem> Items = new List<SideBarItem>();

        public SideBarDataSource(SourceListView controller)
        {
            this.controller = controller;
        }

        public override nint GetChildrenCount(NSOutlineView outlineView, NSObject item)
        {
            return item == null ? Items.Count : (nint)((SideBarItem)item).Count;
        }

        public override bool ItemExpandable(NSOutlineView outlineView, NSObject item)
        {
            return ((SideBarItem)item).HasChildren;
        }

        public override NSObject GetChild(NSOutlineView outlineView, nint childIndex, NSObject item)
        {
            return item == null ? Items[(int)childIndex] : ((SideBarItem)item)[(int)childIndex];
        }

        public override NSObject GetObjectValue(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            return new NSString(((SideBarItem)item).Title);
        }

        internal SideBarItem ItemForRow(int row)
        {
            int index = 0;

            // Look at each group
            foreach (SideBarItem item in Items)
            {
                // Is the row inside this group?
                if (row >= index && row <= (index + item.Count))
                {
                    return item[row - index - 1];
                }

                // Move index
                index += item.Count + 1;
            }

            // Not found 
            return null;
        }
    }
}

