using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Q1_PE_SP23_01.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Q1_PE_SP23_01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IServiceProvider ServiceProvider { get; set; }
        public IConfiguration Configuration { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceColection = new ServiceCollection();
            serviceColection.AddTransient<MainWindow>();
            serviceColection.AddScoped<PRN_Spr23_B1Context>();
            ServiceProvider = serviceColection.BuildServiceProvider();
            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }
    }
}
