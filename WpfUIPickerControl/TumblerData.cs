using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace WpfUIPickerLib
{
    public class TumblerData : INotifyPropertyChanged
    {
        public delegate void OnSelectionChanged();

        //Added
        private readonly OnSelectionChanged _onSelectionChanged;

        public TumblerData()
        {
            Seperator = "";
            Values = new List<object>();
            TumblerWidth = double.NaN;
        }

        public TumblerData(IList values, int selectedValueIndex, string seperator, OnSelectionChanged selectionChanged)
        {
            _onSelectionChanged = selectionChanged;
            Values = values;
            Seperator = seperator;
            TumblerWidth = double.NaN;
            SelectedValueIndex = selectedValueIndex;
        }

        public IList Values { get; set; }

        public string Seperator { get; set; }

        public object SelectedValue =>
            Values == null || _selVal < 0 || _selVal >= Values.Count ? null : Values[_selVal];

        private int _selVal;
        public int SelectedValueIndex
        {
            get => _selVal;
            set
            {
                _selVal = value;
                //Added
                _onSelectionChanged();
                TriggerUpdate();
            }
        }

        public double TumblerWidth { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void TriggerUpdate()
        {
            // cause binding to refire
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs("SelectedValueIndex"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedValue"));
        }
    }
}
