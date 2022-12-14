using System;
using System.Threading.Tasks;
using LibGit2Sharp;
using GitHub.Primitives;
using System.Collections.Generic;
using GitHub.Models;

namespace GitHub.Services
{
    public interface IGitClient
    {
        /// <summary>
        /// Pull changes from the configured upstream remote and branch into the branch pointed at by HEAD.
        /// </summary>
        /// <param name="repository">The repository to pull</param>
        /// <returns></returns>
        Task Pull(IRepository repository);

        /// <summary>
        /// Pushes HEAD to specified branch on the remote.
        /// </summary>
        /// <param name="repository">The repository to push</param>
        /// <param name="branchName">the branch remote to push to</param>
        /// <param name="remoteName">The name of the remote</param>
        /// <returns></returns>
        Task Push(IRepository repository, string branchName, string remoteName);

        /// <summary>
        /// Fetches from the remote, using custom refspecs.
        /// </summary>
        /// <param name="repository">The repository to pull</param>
        /// <param name="remoteName">The name of the remote</param>
        /// <param name="refspecs">The custom refspecs or none to use the default</param>
        /// <returns></returns>
        Task Fetch(IRepository repository, string remoteName, params string[] refspecs);

        /// <summary>
        /// Fetches from a remote URI, using custom refspecs. 
        /// </summary>
        /// <remarks>
        /// If the URI is the same as origin then origin will be used, otherwise a
        /// temporary remote will be created for the fetch. The fetch will always be made
        /// using HTTPS.
        /// </remarks>
        /// <param name="repository">The repository to pull</param>
        /// <param name="remoteUri">The remote URI to fetch from</param>
        /// <param name="refspecs">The custom refspecs</param>
        /// <returns></returns>
        Task Fetch(IRepository repository, UriString remoteUri, params string[] refspecs);

        /// <summary>
        /// Checks out a branch.
        /// </summary>
        /// <param name="repository">The repository to carry out the checkout on</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        Task Checkout(IRepository repository, string branchName);

        /// <summary>
        /// Checks if a commit exists a the repository.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="sha">The SHA of the commit.</param>
        /// <returns></returns>
        Task<bool> CommitExists(IRepository repository, string sha);

        /// <summary>
        /// Creates a new branch.
        /// </summary>
        /// <param name="repository">The repository to carry out the checkout on</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns></returns>
        Task CreateBranch(IRepository repository, string branchName);

        /// <summary>
        /// Gets the value of a configuration key.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="key">The configuration key. Keys are in the form 'section.name'.</param>
        /// <returns></returns>
        Task<T> GetConfig<T>(IRepository repository, string key);

        /// <summary>
        /// Sets the configuration key to the specified value in the local config.
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="key">The configuration key. Keys are in the form 'section.name'.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task SetConfig(IRepository repository, string key, string value);

        /// <summary>
        /// Sets the specified remote to the specified URL.
        /// </summary>
        /// <param name="repository">The repository to set</param>
        /// <param name="remoteName">The name of the remote</param>
        /// <param name="url">The URL to set as the remote</param>
        /// <returns></returns>
        Task SetRemote(IRepository repository, string remoteName, Uri url);

        /// <summary>
        /// Sets the remote branch that the local branch tracks
        /// </summary>
        /// <param name="repository">The repository to set</param>
        /// <param name="branchName">The name of the local branch</param>
        /// <param name="remoteName">The name of the remote</param>
        /// <returns></returns>
        Task SetTrackingBranch(IRepository repository, string branchName, string remoteName);

        /// <summary>
        /// Unsets the configuration key in the local config.
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="key">The configuration key. Keys are in the form 'section.name'.</param>
        /// <returns></returns>
        Task UnsetConfig(IRepository repository, string key);

        Task<Remote> GetHttpRemote(IRepository repo, string remote);

        /// <summary>
        /// Extracts a file at a specified commit from the repository.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="commitSha">The SHA of the commit.</param>
        /// <param name="fileName">The path to the file, relative to the repository.</param>
        /// <returns>
        /// The contents of the file, or null if the file was not found at the specified commit.
        /// </returns>
        Task<string> ExtractFile(IRepository repository, string commitSha, string fileName);

        /// <summary>
        /// Extracts a file at a specified commit from the repository as binary data.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="commitSha">The SHA of the commit.</param>
        /// <param name="fileName">The path to the file, relative to the repository.</param>
        /// <returns>
        /// The contents of the file, or null if the file was not found at the specified commit.
        /// </returns>
        Task<byte[]> ExtractFileBinary(IRepository repository, string commitSha, string fileName);

        /// <summary>
        /// Checks whether the latest commit of a file in the repository has the specified file
        /// contents.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="path">The relative path to the file.</param>
        /// <param name="contents">The file contents to test.</param>
        /// <returns></returns>
        Task<bool> IsModified(IRepository repository, string path, byte[] contents);

        /// <summary>
        /// Find the merge base SHA between two commits.
        /// </summary>
        /// <param name="repo">The repository.</param>
        /// <param name="targetCloneUrl">The clone url of the PR target repo.</param>
        /// <param name="baseSha">The PR base SHA.</param>
        /// <param name="headSha">The PR head SHA.</param>
        /// <param name="baseRef">The PR base ref (e.g. 'master').</param>
        /// <param name="pullNumber">The PR number.</param>
        /// <returns>
        /// The merge base SHA or null.
        /// </returns>
        /// <exception cref="LibGit2Sharp.NotFoundException">Thrown when the merge base can't be found.</exception>
        Task<string> GetPullRequestMergeBase(IRepository repo, UriString targetCloneUrl, string baseSha, string headSha, string baseRef, int pullNumber);

        /// <summary>
        /// Checks whether the current head is pushed to its remote tracking branch.
        /// </summary>
        /// <param name="repo">The repository.</param>
        /// <returns></returns>
        Task<bool> IsHeadPushed(IRepository repo);

        /// <summary>
        /// Gets the unique commits from <paramref name="compareBranch"/> to the merge base of 
        /// <paramref name="baseBranch"/> and <paramref name="compareBranch"/> and returns their
        /// commit messages.
        /// </summary>
        /// <param name="repo">The repository.</param>
        /// <param name="baseBranch">The base branch to find a merge base with.</param>
        /// <param name="compareBranch">The compare branch to find a merge base with.</param>
        /// <param name="maxCommits">The maximum number of commits to return.</param>
        /// <returns>An enumerable of unique commits from the merge base to the compareBranch.</returns>
        Task<IReadOnlyList<CommitMessage>> GetMessagesForUniqueCommits(
            IRepository repo,
            string baseBranch,
            string compareBranch,
            int maxCommits);
    }
}
