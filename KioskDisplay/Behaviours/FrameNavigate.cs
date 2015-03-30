using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Interactivity;

namespace KioskDisplay.Behaviours
{
    public class FrameNavigate : Behavior<FrameworkElement>
    {
        Panel panel;
        Frame frame;
        Frame newFrame;
        Point p = new Point(0, 0);

        [Description("Select a Page to Navigate to."), Category("Behavior Settings")]
        public Uri NavigateTo { get { return (Uri)GetValue(NavigateToProperty); } set { SetValue(NavigateToProperty, value); } }
        public static readonly DependencyProperty NavigateToProperty = DependencyProperty.Register("NavigateTo", typeof(Uri), typeof(FrameNavigate), null);

        public enum Transitions { Left = 0, Right = 1, Up = 2, Down = 3, Fade = 4, LeftWithFade = 5, RightWithFade = 6 }
        [Description("Select the direction you would like the slide transition to go in."), Category("Behavior Settings")]
        public Transitions Transition { get { return (Transitions)GetValue(TransitionProperty); } set { SetValue(TransitionProperty, value); } }
        public static readonly DependencyProperty TransitionProperty = DependencyProperty.Register("Transition", typeof(Transitions), typeof(FrameNavigate), null);

        [Description("Seconds for the transition.  Example: 0.5 for half a second, 1 for a second, etc..."), Category("Behavior Settings")]
        public double SlideTime { get { return (double)GetValue(SlideTimeProperty); } set { SetValue(SlideTimeProperty, value); } }
        public static readonly DependencyProperty SlideTimeProperty = DependencyProperty.Register("SlideTime", typeof(double), typeof(FrameNavigate), null);

        protected override void OnAttached()
        {
            base.OnAttached();
            Attached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        private void Attached()
        {
            FrameworkElement fe = this.AssociatedObject as FrameworkElement;
            if (fe.GetType().Name.ToString() == "Button")
            {
                Button btn = fe as Button;
                btn.Click += onClick;
            }
            else
                fe.MouseLeftButtonDown += onMouseDown;
        }

        private void onClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DependencyObject parent = VisualTreeHelper.GetParent(btn);
            while (!(parent is Frame))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            frame = parent as Frame;
            panel = frame.Parent as Panel;
            p = frame.TranslatePoint(new Point(0, 0), panel);
            navigateFrame();
        }

        private void onMouseDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject obj = sender as DependencyObject;
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (!(parent is Frame))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            frame = parent as Frame;
            panel = frame.Parent as Panel;
            p = frame.TranslatePoint(new Point(0, 0), panel);
            navigateFrame();
        }

        private void navigateFrame()
        {
            newFrame = new Frame();
            newFrame.Content = frame.Content;
            newFrame.Width = frame.ActualWidth;
            newFrame.Height = frame.ActualHeight;
            newFrame.HorizontalAlignment = HorizontalAlignment.Left;
            newFrame.VerticalAlignment = VerticalAlignment.Top;
            newFrame.Margin = new Thickness(p.X, p.Y, 0, 0);
            newFrame.Background = frame.Background;
            panel.Children.Add(newFrame);

            frame.Source = NavigateTo;
            newFrame.IsHitTestVisible = false;
            frame.IsHitTestVisible = false;

            double a = frame.ActualWidth;
            double b = frame.ActualHeight;

            TranslateTransform tt = new TranslateTransform();
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(daComplete);
            da.Duration = new Duration(TimeSpan.FromSeconds(SlideTime));

            TranslateTransform tt2 = new TranslateTransform();
            DoubleAnimation da2 = new DoubleAnimation();
            da2.Completed += new EventHandler(daComplete);
            da2.Duration = new Duration(TimeSpan.FromSeconds(SlideTime));

            DoubleAnimation daOpacity = new DoubleAnimation();
            daOpacity.Duration = da.Duration;

            DoubleAnimation daOpacity2 = new DoubleAnimation();
            daOpacity2.Duration = da.Duration;

            if (Transition == Transitions.Down)
            {
                da.From = p.Y;
                da.To = p.Y + b;
                da2.From = p.Y - b;
                da2.To = p.Y;
                newFrame.RenderTransform = tt;
                frame.RenderTransform = tt2;
                tt.BeginAnimation(TranslateTransform.YProperty, da);
                tt2.BeginAnimation(TranslateTransform.YProperty, da2);
            }
            if (Transition == Transitions.Left)
            {
                da.From = p.X;
                da.To = p.X - a;
                da2.From = p.X + a;
                da2.To = p.X;
                newFrame.RenderTransform = tt;
                frame.RenderTransform = tt2;
                tt.BeginAnimation(TranslateTransform.XProperty, da);
                tt2.BeginAnimation(TranslateTransform.XProperty, da2);
            }
            if (Transition == Transitions.Right)
            {
                da.From = p.X;
                da.To = p.X + a;
                da2.From = p.X - a;
                da2.To = p.X;
                newFrame.RenderTransform = tt;
                frame.RenderTransform = tt2;
                tt.BeginAnimation(TranslateTransform.XProperty, da);
                tt2.BeginAnimation(TranslateTransform.XProperty, da2);
            }
            if (Transition == Transitions.Up)
            {
                da.From = p.Y;
                da.To = p.Y - b;
                da2.From = p.Y + b;
                da2.To = p.Y;
                newFrame.RenderTransform = tt;
                frame.RenderTransform = tt2;
                tt.BeginAnimation(TranslateTransform.YProperty, da);
                tt2.BeginAnimation(TranslateTransform.YProperty, da2);
            }

            if (Transition == Transitions.Fade)
            {
                da.Completed -= new EventHandler(daComplete);
                da.Completed += new EventHandler(daFade);
                da.Duration = new Duration(TimeSpan.FromSeconds(SlideTime / 2));
                da2.Completed -= new EventHandler(daComplete);
                da2.Duration = da.Duration;
                da.From = 1;
                da.To = 0;
                da2.From = 0;
                da2.To = 0;
                newFrame.BeginAnimation(Frame.OpacityProperty, da);
                frame.BeginAnimation(Frame.OpacityProperty, da2);
            }
            if (Transition == Transitions.LeftWithFade)
            {
                da.From = p.X;
                da.To = p.X - a;
                da2.From = p.X + a;
                da2.To = p.X;

                daOpacity.From = 1;
                daOpacity.To = 0;
                daOpacity2.From = 0;
                daOpacity2.To = 1;

                newFrame.RenderTransform = tt;
                frame.RenderTransform = tt2;

                newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
                tt.BeginAnimation(TranslateTransform.XProperty, da);

                frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
                tt2.BeginAnimation(TranslateTransform.XProperty, da2);
            }
            if (Transition == Transitions.RightWithFade)
            {
                da.From = p.X;
                da.To = p.X + a;
                da2.From = p.X - a;
                da2.To = p.X;

                daOpacity.From = 1;
                daOpacity.To = 0;
                daOpacity2.From = 0;
                daOpacity2.To = 1;

                newFrame.RenderTransform = tt;
                frame.RenderTransform = tt2;

                newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
                tt.BeginAnimation(TranslateTransform.XProperty, da);

                frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
                tt2.BeginAnimation(TranslateTransform.XProperty, da2);
            }
        }

        private void daComplete(object sender, System.EventArgs e)
        {
            panel.Children.Remove(newFrame);
            frame.IsHitTestVisible = true;
        }

        private void daFade(object sender, System.EventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(daComplete);
            da.Duration = new Duration(TimeSpan.FromSeconds(SlideTime / 2));
            da.From = 0;
            da.To = 1;
            frame.BeginAnimation(Frame.OpacityProperty, da);
        }
    }
}