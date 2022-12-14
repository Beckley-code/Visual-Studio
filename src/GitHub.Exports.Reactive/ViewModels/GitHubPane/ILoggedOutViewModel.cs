using System;
using System.Reactive;
using ReactiveUI;

namespace GitHub.ViewModels.GitHubPane
{
    /// <summary>
    /// Defines the view model for the "Sign in to GitHub" view in the GitHub pane.
    /// </summary>
    public interface ILoggedOutViewModel : IPanePageViewModel
    {
        /// <summary>
        /// Gets the command executed when the user clicks the "Sign in" link.
        /// </summary>
        ReactiveCommand<Unit, Unit> SignIn { get; }

        /// <summary>
        /// Gets the command executed when the user clicks the "Create an Account" link.
        /// </summary>
        ReactiveCommand<Unit, Unit> Register { get; }
    }
}