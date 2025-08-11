using DevExpress.Xpf.Grid;

using System.ComponentModel;

namespace WpfAppGenericGrid;

public class ColumnPickerItem : INotifyPropertyChanged
{
    private readonly GridColumn _col;
    public ColumnPickerItem(GridColumn col) { _col = col; }

    public string FieldName => _col.FieldName;
    public string Header => string.IsNullOrWhiteSpace(_col.Header?.ToString()) ? _col.FieldName : _col.Header.ToString()!;

    public bool IsVisible
    {
        get => _col.Visible;
        set
        {
            if (_col.Visible == value) return;
            _col.Visible = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
        }
    }

    public void SyncFrom(GridColumn col)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}