using System.Collections.Generic;
using System.Reactive;
using GitHub.Models;
using ReactiveUI;

namespace GitHub.ViewModels.Dialog
{
    public interface IRepositoryCreationViewModel : IDialogContentViewModel,
        IConnectionInitializedViewModel,
        IRepositoryForm,
        IRepositoryCreationTarget
    {
        /// <summary>
        /// Command that creates the repository.
        /// </summary>
        ReactiveCommand<Unit, Unit> CreateRepository { get; }

        /// <summary>
        /// True when creation is in progress.
        /// </summary>
        bool IsCreating { get; }

        GitIgnoreItem SelectedGitIgnoreTemplate { get; set; }

        LicenseItem SelectedLicense { get; set; }

        /// <summary>
        /// The list of GitIgnore templates supported by repository creation
        /// </summary>
        IReadOnlyList<GitIgnoreItem> GitIgnoreTemplates { get; }

        /// <summary>
        /// The list of license templates supported by repository creation
        /// </summary>
        IReadOnlyList<LicenseItem> Licenses { get; }
    }
}
