// PkgCmdID.cs
// MUST match PkgCmdID.h
namespace GitHub.VisualStudio
{
    public static class PkgCmdIDList
    {
        // IDs defined in GitHub.VisualStudio.vsct
        public const int addConnectionCommand = 0x110;
        public const int idGitHubToolbar = 0x1120;
        public const int showGitHubPaneCommand = 0x200;
        public const int openPullRequestsCommand = 0x201;
        public const int showCurrentPullRequestCommand = 0x202;
        public const int syncSubmodulesCommand = 0x203;
        public const int openFromUrlCommand = 0x204;
        public const int openFromClipboardCommand = 0x205;
        public const int showIssueishDocumentCommand = 0x206;

        public const int backCommand = 0x300;
        public const int forwardCommand = 0x301;
        public const int refreshCommand = 0x302;
        public const int pullRequestCommand = 0x310;
        public const int createGistCommand = 0x400;
        public const int createGistEnterpriseCommand = 0x401;
        public const int openLinkCommand = 0x100;
        public const int copyLinkCommand = 0x101;
        public const int goToSolutionOrPullRequestFileCommand = 0x102;
        public const int githubCommand = 0x320;
        public const int helpCommand = 0x321;
        public const int blameCommand = 0x500;

        // IDs defined in InlineReviewsPackage.vsct
        public const int NextInlineCommentId = 0x1001;
        public const int PreviousInlineCommentId = 0x1002;
        public const int ToggleInlineCommentMarginId = 0x1003;
    };
}