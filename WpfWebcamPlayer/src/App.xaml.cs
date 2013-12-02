// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="CatenaLogic">
//   Copyright (c) 2012 - 2013 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WebcamPlayer
{
    using System.Windows;
    using Catel.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            StyleHelper.CreateStyleForwardersForDefaultStyles();
        }

        #endregion
    }
}