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
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		AppKit.NSButton PlayPauseButton { get; set; }

		[Action ("NextButtonClicked:")]
		partial void NextButtonClicked (Foundation.NSObject sender);

		[Action ("PlayPauseButtonClicked:")]
		partial void PlayPauseButtonClicked (Foundation.NSObject sender);

		[Action ("PreviousButtonClicked:")]
		partial void PreviousButtonClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PlayPauseButton != null) {
				PlayPauseButton.Dispose ();
				PlayPauseButton = null;
			}
		}
	}
}
