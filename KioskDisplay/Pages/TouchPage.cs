using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace KioskDisplay.Pages
{
    public abstract class TouchPage : PageBase
    {
        protected TouchPoint TouchStart;
        protected bool AlreadySwiped = false;

        public event EventHandler TouchSwipeLeft = delegate { };
        public event EventHandler TouchSwipeRight = delegate { };

        public TouchPage()
        {
            TouchDown += TouchPage_TouchDown;
            TouchMove += TouchPage_TouchMove;
        }

        void TouchPage_TouchMove(object sender, TouchEventArgs e)
        {
            if(AlreadySwiped || TouchStart == null)
            {
                return;
            }

            var finalTouch = e.GetTouchPoint(this);
            if(finalTouch == null)
            {
                return;
            }

            // Swipe to right
            if(finalTouch.Position.X > TouchStart.Position.X + 200)
            {
                AlreadySwiped = true;
                try
                {
                    TouchSwipeLeft(this, new EventArgs());
                }
                finally
                {
                    AlreadySwiped = false;
                    TouchStart = null;
                }
                return;
            }

            // Swipe to left
            if(finalTouch.Position.X < TouchStart.Position.X - 200)
            {
                AlreadySwiped = true;
                try
                {
                    TouchSwipeRight(this, new EventArgs());
                }
                finally
                {
                    AlreadySwiped = false;
                    TouchStart = null;
                }
                return;
            }
        }

        void TouchPage_TouchDown(object sender, TouchEventArgs e)
        {
            TouchStart = e.GetTouchPoint(this);
        }
    }
}
