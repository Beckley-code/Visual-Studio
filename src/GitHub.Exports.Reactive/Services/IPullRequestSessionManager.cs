using System;
using System.ComponentModel;
using System.Threading.Tasks;
using GitHub.Models;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace GitHub.Services
{
    /// <summary>
    /// Manages pull request sessions.
    /// </summary>
    /// <remarks>
    /// If the currently checked out branch represents a pull request then <see cref="CurrentSession"/>
    /// will return an <see cref="IPullRequestSession"/> containing the details of that pull request.
    /// A session for any other pull request can also be retrieved by calling
    /// <see cref="GetSession(string, string, int)"/>.
    /// 
    /// Calling <see cref="GetLiveFile(string, ITextView, ITextBuffer)"/> will return an
    /// <see cref="IPullRequestSessionFile"/> which tracks both the contents of a text buffer and the
    /// current session, and updates the review comments in real-time.
    /// </remarks>
    public interface IPullRequestSessionManager : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the current pull request session.
        /// </summary>
        /// <returns>
        /// The current pull request session, or null if the currently checked out branch is not
        /// a pull request branch.
        /// </returns>
        IPullRequestSession CurrentSession { get; }

        /// <summary>
        /// Ensures that the service is initialized.
        /// </summary>
        /// <returns>A task that when completed indicates that the service is initialized.</returns>
        /// <remarks>
        /// If you are simply monitoring changes to the <see cref="CurrentSession"/> then you
        /// don't need to call this method: <see cref="CurrentSession"/> will be updated on
        /// initialization. If however, you want to be sure that <see cref="CurrentSession"/> is
        /// initialized, you can await the task returned from this method.
        /// </remarks>
        Task EnsureInitialized();

        /// <summary>
        /// Gets an <see cref="IPullRequestSessionFile"/> that tracks the live state of a document.
        /// </summary>
        /// <param name="relativePath">The relative path to the file in the repository.</param>
        /// <param name="textView">The text view that is showing the file.</param>
        /// <param name="textBuffer">The text buffer with the file contents.</param>
        /// <returns>An <see cref="IPullRequestSessionLiveFile"/>.</returns>
        Task<IPullRequestSessionFile> GetLiveFile(
            string relativePath,
            ITextView textView,
            ITextBuffer textBuffer);

        /// <summary>
        /// Gets the path of a document displayed in a text buffer, relative to the current
        /// repository.
        /// </summary>
        /// <param name="buffer">The text buffer.</param>
        /// <returns>
        /// The relative path, or null if the buffer does not represent a file in the repository.
        /// </returns>
        string GetRelativePath(ITextBuffer buffer);

        /// <summary>
        /// Gets an <see cref="IPullRequestSession"/> for a pull request.
        /// </summary>
        /// <param name="owner">The repository owner.</param>
        /// <param name="name">The repository name.</param>
        /// <param name="number">The pull request number.</param>
        /// <returns>An <see cref="IPullRequestSession"/>.</returns>
        Task<IPullRequestSession> GetSession(string owner, string name, int number);

        /// <summary>
        /// Gets information about the pull request that a Visual Studio text buffer is a part of.
        /// </summary>
        /// <param name="buffer">The text buffer.</param>
        /// <returns>
        /// A <see cref="PullRequestTextBufferInfo"/> or null if the pull request for the text
        /// buffer could not be determined.
        /// </returns>
        /// <remarks>
        /// This method looks for a <see cref="PullRequestTextBufferInfo"/> object stored in the text
        /// buffer's properties.
        /// </remarks>
        PullRequestTextBufferInfo GetTextBufferInfo(ITextBuffer buffer);
    }
}
