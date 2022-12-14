using System;
using System.Globalization;
using GitHub.Extensions;

namespace GitHub.Primitives
{
    public class HostAddress
    {
        public static readonly HostAddress GitHubDotComHostAddress = new HostAddress();
        static readonly Uri gistUri = new Uri("https://gist.github.com");

        /// <summary>
        /// Creates a host address based on the hostUri based on the expected patterns for GitHub.com and 
        /// GitHub Enterprise instances. The passed in URI can be any URL to the instance.
        /// </summary>
        /// <param name="hostUri">The URI to a GitHub or GitHub Enterprise instance.</param>
        /// <returns></returns>
        public static HostAddress Create(Uri hostUri)
        {
            return IsGitHubDotComUri(hostUri)
                ? GitHubDotComHostAddress
                : new HostAddress(hostUri);
        }

        /// <summary>
        /// Creates a host address from a host name or URL as a string.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static HostAddress Create(string host)
        {
            Uri uri;
            if (Uri.TryCreate(host, UriKind.Absolute, out uri)
                   || Uri.TryCreate("https://" + host, UriKind.Absolute, out uri))
            {
                return Create(uri);
            }
            throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                "The host '{0}' is not a valid host",
                host));
        }

        private HostAddress(Uri enterpriseUri)
        {
            WebUri = new Uri(enterpriseUri, new Uri("/", UriKind.Relative));
            ApiUri = new Uri(enterpriseUri, new Uri("/api/v3/", UriKind.Relative));
            GraphQLUri = new Uri(enterpriseUri, new Uri("/api/graphql", UriKind.Relative));
            CredentialCacheKeyHost = WebUri.ToString();
        }

        public HostAddress()
        {
            WebUri = new Uri("https://github.com");
            ApiUri = new Uri("https://api.github.com");
            GraphQLUri = new Uri("https://api.github.com/graphql");
            CredentialCacheKeyHost = WebUri.ToString();
        }

        /// <summary>
        /// The Base URL to the host. For example, "https://github.com" or "https://github-enterprise.com"
        /// </summary>
        public Uri WebUri { get; set; }

        /// <summary>
        /// The Base Url to the host's API endpoint. For example, "https://api.github.com" or
        ///  "https://github-enterprise.com/api/v3"
        /// </summary>
        public Uri ApiUri { get; set; }

        /// <summary>
        /// The Base Url to the host's GraphQL API endpoint. For example, "https://api.github.com/graphql" or
        ///  "https://github-enterprise.com/api/graphql"
        /// </summary>
        public Uri GraphQLUri { get; set; }

        // If the host name is "api.github.com" or "gist.github.com", we really only want "github.com",
        // since that's the same cache key for all the other github.com operations.
        public string CredentialCacheKeyHost { get; private set; }

        public static bool IsGitHubDotComUri(Uri hostUri)
        {
            return hostUri.IsSameHost(GitHubDotComHostAddress.WebUri)
                   || hostUri.IsSameHost(GitHubDotComHostAddress.ApiUri)
                   || hostUri.IsSameHost(gistUri);
        }

        public static bool operator ==(HostAddress a, HostAddress b)
        {
            return object.ReferenceEquals(a, null) ? object.ReferenceEquals(b, null) : a.Equals(b);
        }

        public static bool operator !=(HostAddress a, HostAddress b)
        {
            return !(a == b);
        }

        public bool IsGitHubDotCom()
        {
            return IsGitHubDotComUri(ApiUri);
        }

        public string Title
        {
            get {  return IsGitHubDotCom() ? "GitHub" : ApiUri.Host; }
        }

        public override int GetHashCode()
        {
            return (WebUri?.GetHashCode() ?? 0) ^ (ApiUri?.GetHashCode() ?? 0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as HostAddress;
            return other != null && WebUri.IsSameHost(other.WebUri) && ApiUri.IsSameHost(other.ApiUri);
        }
    }
}
