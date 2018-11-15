using System;
using AppKit;
using Foundation;

namespace UltraMusic.Models
{
    [Register ("SourceListView")]
    public class SourceListView : NSOutlineView
    {
        public SideBarDataSource Data
        {
            get { return (SideBarDataSource)DataSource; }
        }


        #region Constructors

        public SourceListView()
        {

        }

        public SourceListView(IntPtr handle) : base(handle)
        {

        }

        public SourceListView(NSCoder coder) : base(coder)
        {

        }

        public SourceListView(NSObjectFlag t) : base(t)
        {

        }

        #endregion

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        public void Initialize()
        {

            // Initialize this instance
            this.DataSource = new SideBarDataSource(this);
            this.Delegate = new SideBarDelegate(this);

        }

        public void AddItem(SideBarItem item)
        {
            if (Data != null)
            {
                Data.Items.Add(item);
            }
        }


        #region Events

        public delegate void ItemSelectedDelegate(SideBarItem item);
        public event ItemSelectedDelegate ItemSelected;

        internal void RaiseItemSelected(SideBarItem item)
        {
            // Inform caller
            ItemSelected?.Invoke(item);
        }

        #endregion
    }
}