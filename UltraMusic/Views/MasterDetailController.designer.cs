// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace UltraMusic.Views
{
	[Register ("MasterDetailController")]
	partial class MasterDetailController
	{
		[Outlet]
		AppKit.NSSplitViewItem SideBarItem { get; set; }

		[Outlet]
        AppKit.NSSplitViewItem WebRendererItem { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SideBarItem != null) {
				SideBarItem.Dispose ();
				SideBarItem = null;
			}

			if (WebRendererItem != null) {
				WebRendererItem.Dispose ();
				WebRendererItem = null;
			}
		}
	}
}
