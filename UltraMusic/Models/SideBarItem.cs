using System;
using System.Collections;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace UltraMusic.Models
{
    public class SideBarItem : NSObject, IEnumerator, IEnumerable
    {
        private List<SideBarItem> items = new List<SideBarItem>();

        public string Title { get; set; }

        public NSImage Icon { get; set; }

        public string Tag { get; set; }

        public SideBarItem this[int index]
        {
            get
            {
                return items[index];
            }

            set
            {
                items[index] = value;
            }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool HasChildren
        {
            get { return (Count > 0); }
        }


        #region Enumerable Routines

        private int position = -1;

        public IEnumerator GetEnumerator()
        {
            position = -1;
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < items.Count);
        }

        public void Reset()
        { position = -1; }

        public object Current
        {
            get
            {
                try
                {
                    return items[position];
                }

                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        #endregion


        public SideBarItem(string title, ClickedDelegate clicked = null, NSImage icon = null, string tag = null)
        {
            Title = title;
            Icon = icon;
            Tag = tag;
            Clicked = clicked;
        }

        #region Public Methods
        public void AddItem(SideBarItem item)
        {
            items.Add(item);
        }

        public void AddItem(string title, ClickedDelegate clicked, NSImage icon = null, string tag = null)
        {
            items.Add(new SideBarItem(title, clicked, icon, tag));
        }

        public void Insert(int n, SideBarItem item)
        {
            items.Insert(n, item);
        }

        public void RemoveItem(SideBarItem item)
        {
            items.Remove(item);
        }

        public void RemoveItem(int n)
        {
            items.RemoveAt(n);
        }

        public void Clear()
        {
            items.Clear();
        }
        #endregion

        #region Events
        public delegate void ClickedDelegate();
        public event ClickedDelegate Clicked;

        internal void RaiseClickedEvent()
        {
            this.Clicked?.Invoke();
        }
        #endregion
    }
}
