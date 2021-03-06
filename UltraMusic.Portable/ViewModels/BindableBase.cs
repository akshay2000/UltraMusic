﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UltraMusic.Portable.ViewModels
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual bool Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        public virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            if (Equals(handler, null))
            {
                return;
            }
            var args = new PropertyChangedEventArgs(propertyName);
            handler.Invoke(this, args);
        }
    }
}
