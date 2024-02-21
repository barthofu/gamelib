﻿using System.Configuration;
using System.Data;
using System.Windows;
using gamelib.Context;
using Microsoft.EntityFrameworkCore;

namespace gamelib;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using (var context = new GamelibContext())
        {
            context.Database.Migrate();
        }
    }
}