using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMusic.UWP.ViewModels;

namespace UltraMusic.UWP.Helpers
{
    internal class ViewModelLocator
    {
        private static MainViewModel mainViewModel;
        public static MainViewModel MainViewModel
        {
            get
            {
                mainViewModel = mainViewModel ?? new MainViewModel();
                return mainViewModel;
            }
        }
    }
}
