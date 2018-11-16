using System;
using UltraMusic.Portable.ViewModels;

namespace UltraMusic.Helpers
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {

        }

        private static MainViewModel mainViewModel;
        public static MainViewModel MainViewModel
        {
            get
            {
                return mainViewModel = mainViewModel ?? new MainViewModel();
            }
        }
    }
}
