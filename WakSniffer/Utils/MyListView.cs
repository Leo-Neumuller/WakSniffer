using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WakSniffer.Utils
{
    public class MyListView : ListView
    {
        public event ScrollEventHandler Scroll;
        public event EventHandler AddEvent;

        protected virtual void OnScroll(ScrollEventArgs e)
        {
            ScrollEventHandler handler = this.Scroll;
            if (handler != null) handler(this, e);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x1007:
                    AddValueEvent(EventArgs.Empty);
                    break;
                case 0x104D:
                    AddValueEvent(EventArgs.Empty);
                    break;
                case 0x115:
                    OnScroll(new ScrollEventArgs((ScrollEventType)(m.WParam.ToInt32() & 0xffff), 0));
                    break;
            }
        }

        protected virtual void AddValueEvent(EventArgs e)
        {
            AddEvent?.Invoke(this, e);
        }
    }
}