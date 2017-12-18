using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Linq;

namespace WpfUIPickerLib
{
    public partial class WpfUIPickerControl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WpfUIPickerControl()
        {
            InitializeComponent();
        }

        public List<object> SelectedValues =>
            Tumblers.Select(tumbler => tumbler.SelectedValue).ToList();

        private bool _open;
        /// <summary>
        /// Flag to indicate whether the user control is "open" (i.e. displaying tumblers).
        /// </summary>
        public bool IsOpen
        {
            get => _open || AlwaysOpen;
            set
            {
                bool newVal = value || AlwaysOpen;
                if (_open == newVal) return;

                _open = newVal;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsOpen"));
            }
        }

        #region Tumblers Dependency Property
        /// <summary>
        /// Dependency property for List of Tumblers
        /// </summary>
        public List<TumblerData> Tumblers
        {
            get => (List<TumblerData>)GetValue(TumblersProperty);
            set => SetValue(TumblersProperty, value);
        }

        // Using a DependencyProperty as the backing store for Tumblers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TumblersProperty =
            DependencyProperty.Register("Tumblers", typeof(List<TumblerData>), typeof(WpfUIPickerControl), new UIPropertyMetadata(null, OnTumblersPropertyChanged));

        private static void OnTumblersPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            WpfUIPickerControl thisCtrl = source as WpfUIPickerControl;
            // TODO: update display
        }
        #endregion


        #region AlwaysOpen Dependency Property
        public bool AlwaysOpen
        {
            get => (bool)GetValue(AlwaysOpenProperty);
            set => SetValue(AlwaysOpenProperty, value);
        }

        // Using a DependencyProperty as the backing store for AlwaysOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlwaysOpenProperty =
            DependencyProperty.Register("AlwaysOpen", typeof(bool), typeof(WpfUIPickerControl), new UIPropertyMetadata(false));
        #endregion


        /// <summary>
        /// Mouse wheel handler for tumblers.  This is not a fired event because we need to mouse capture on the main grid
        /// so we can know when to close the control.  Instead this event gets called via the MouseWheel event on the main grid.
        /// </summary>
        /// <param name="tumbler">The tumbler (grid) that should receive the mouse wheel event</param>
        /// <param name="e">the MouseWheelEventArgs forwarded from the main grid MouseWheel event</param>
        private static void Tumbler_PreviewMouseWheel(FrameworkElement tumbler, MouseWheelEventArgs e)
        {
            // Each click in the mouse will increment or decrement the tumbler value by one
            if (!(tumbler?.Tag is TumblerData td)) return;
            int newIdx = td.SelectedValueIndex + (e.Delta > 0 ? 1 : -1);
            if (newIdx >= 0 && newIdx < td.Values.Count)
            {
                // Set the new index which will cause NotifyPropertyChanged to fire, which in turn will cause
                // the binding/converter to re-evaluate, which will result in the tumbler animating to the correct 
                // canvas offset.
                td.SelectedValueIndex = newIdx;
            }
        }

        private Grid _dragTumbler;
        private Point _dragPt = new Point(0, 0);
        private double _originalDragOffset;

        private void mainGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mainGrid.IsMouseCaptured == false)
            {
                IsOpen = true;
                mainGrid.CaptureMouse();
                StartDrag();
            }            
            else if (mainGrid.ContainsMouse())
                StartDrag();
            else
            { 
                // close ui picker
                mainGrid.ReleaseMouseCapture();
                IsOpen = false;
            }
        }

        private void StartDrag()
        {
            _dragTumbler = GetTargetTumbler();
            _dragPt = Mouse.GetPosition(mainGrid);
            _originalDragOffset = Canvas.GetTop(_dragTumbler);
        }

        private void mainGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // figure out which tumbler should get the mouse wheel
            Grid targetTumbler = GetTargetTumbler();
            if (targetTumbler != null && IsOpen)
                Tumbler_PreviewMouseWheel(targetTumbler, e);
        }

        private void mainGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // is this the end of a drag operation?
            if (_dragTumbler != null)
            {
                double offset = -Canvas.GetTop(_dragTumbler);

                Point mousePt = Mouse.GetPosition(mainGrid);
                if (mousePt.Equals(_dragPt))
                {
                    // user clicked on tumbler without dragging, select the clicked-on value
                    offset += mousePt.Y - mainGrid.ActualHeight / 2;
                }
                // find the Canvas offset for the value closest to where the drag ended

                if (_dragTumbler.Tag is TumblerData td)
                {
                    double itemHeight = _dragTumbler.ActualHeight / td.Values.Count;
                    double newVal = offset / itemHeight + 2;
                    var iVal = (int)Math.Round(newVal);

                    // update index, limit to valid values.  The update will cause NotifyPropertyChanged which
                    // will animate the tumbler into the appropriate position for the selected value
                    td.SelectedValueIndex = Math.Min(Math.Max(0, iVal), td.Values.Count-1);
                }

                _dragTumbler = null;
            }

            if (mainGrid.IsMouseCaptured && AlwaysOpen)
                mainGrid.ReleaseMouseCapture();
        }

        private void mainGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // are we dragging a tumbler?
            if (Mouse.LeftButton != MouseButtonState.Pressed || _dragTumbler == null) return;

            Point pt = Mouse.GetPosition(mainGrid);
            double diff = pt.Y - _dragPt.Y;
            // have to remove the animation before setting it manually (otherwise nothing happens because
            // the animation fill behavior is set to HoldEnd)
            _dragTumbler.BeginAnimation(Canvas.TopProperty, null);
            Canvas.SetTop(_dragTumbler, _originalDragOffset + diff);
        }

        /// <summary>
        /// Gets the Tumbler (grid) that currently contains the mouse.  Useful for forwarding mouse events to the specific tumbler.
        /// </summary>
        /// <returns>the Grid (itemsGrid) that contains the mouse pointer</returns>
        private Grid GetTargetTumbler()
        {
            Grid targetTumbler = null;
            for (var i = 0; i < Tumblers.Count; ++i)
            {
                Border foundTumbler = mainGrid.FindChild<Border>("tumblerBorder", i);
                if (foundTumbler == null || !foundTumbler.ContainsMouse()) continue;
                targetTumbler = foundTumbler.FindChild<Grid>("itemsGrid", 0);
                break;
            }
            return targetTumbler;
        }

        private void thisCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            IsOpen = AlwaysOpen;
            // Trigger an update on each tumbler so its canvas offset is set properly for displaying the selected value
            if (Tumblers == null) return;

            foreach (TumblerData td in Tumblers)
                td.TriggerUpdate();
        }
    }

}
