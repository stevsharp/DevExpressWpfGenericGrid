namespace WpfAppGenericGrid;

/// <summary>
/// Interaction logic for GenericGridControl.xaml

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

using WpfAppGenericGrid.Model;

public partial class GenericGridControl : System.Windows.Controls.UserControl
{
    public GenericGridControl()
    {
        InitializeComponent();
        Loaded += async (_, __) => { 
            if (AutoLoad) await LoadAsync(); };
    }

    public static readonly DependencyProperty DbPathProperty =
        DependencyProperty.Register(nameof(DbPath), typeof(string), typeof(GenericGridControl),
            new PropertyMetadata("app.db", OnConfigChanged));
    public string DbPath
    {
        get => (string)GetValue(DbPathProperty);
        set => SetValue(DbPathProperty, value);
    }

    public static readonly DependencyProperty EntityTypeProperty =
        DependencyProperty.Register(nameof(EntityType), typeof(Type), typeof(GenericGridControl),
            new PropertyMetadata(null, OnConfigChanged));
    public Type? EntityType
    {
        get => (Type?)GetValue(EntityTypeProperty);
        set => SetValue(EntityTypeProperty, value);
    }

    public static readonly DependencyProperty AutoLoadProperty =
        DependencyProperty.Register(nameof(AutoLoad), typeof(bool), typeof(GenericGridControl),
            new PropertyMetadata(true));
    public bool AutoLoad
    {
        get => (bool)GetValue(AutoLoadProperty);
        set => SetValue(AutoLoadProperty, value);
    }

    private static async void OnConfigChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var c = (GenericGridControl)d;
        if (c.IsLoaded && c.AutoLoad)
            await c.LoadAsync();
    }

    public async Task LoadAsync(CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(DbPath) || EntityType is null)
        {
            Grid.ItemsSource = null;
            return;
        }

        await using var db = new AppDbContext(DbPath);
        await db.Database.EnsureCreatedAsync(ct);


        var setMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set), Type.EmptyTypes)!;
        var genericSetMethod = setMethod.MakeGenericMethod(EntityType);
        var dbSet = genericSetMethod.Invoke(db, null)!;

        var queryable = (IQueryable)dbSet;

        var toListAsync = typeof(EntityFrameworkQueryableExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .First(m => m.Name == nameof(EntityFrameworkQueryableExtensions.ToListAsync)
                        && m.GetParameters().Length == 2)
            .MakeGenericMethod(EntityType);

        var task = (Task)toListAsync.Invoke(null, [queryable, ct])!;
        await task.ConfigureAwait(false);

        var result = task.GetType().GetProperty("Result")!.GetValue(task) as IList;

        Grid.ItemsSource = result;
    }

}

