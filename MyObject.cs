namespace sfValidation
{
    using SoftFluent.Windows;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;

    public class MyObject : INotifyDataErrorInfo, IDataErrorInfo, INotifyPropertyChanged
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null || propertyName == nameof(Text))
            {
                if (string.IsNullOrWhiteSpace(_text))
                    yield return "Text must not be empty.";
            }
        }

        public bool HasErrors => (GetErrors(null)?.Cast<object>()?.Any()).GetValueOrDefault();

        private string _text;
        [PropertyGridOptions(EditorDataTemplateResourceKey = "TextEditor")]
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                    return;

                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasErrors)));
                if ((GetErrors(nameof(Text))?.Cast<object>()?.Any()).GetValueOrDefault())
                {
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Text)));
                }
            }
        }

        string IDataErrorInfo.Error => ((IDataErrorInfo)this)[null];
        string IDataErrorInfo.this[string columnName] => GetErrors(columnName)?.Cast<object>()?.FirstOrDefault()?.ToString();
    }
}
