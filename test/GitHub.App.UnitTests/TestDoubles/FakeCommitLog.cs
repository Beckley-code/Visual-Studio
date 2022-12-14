using System;
using System.Collections.Generic;
using LibGit2Sharp;

public class FakeCommitLog : List<Commit>, IQueryableCommitLog
{
    public CommitSortStrategies SortedBy
    {
        get
        {
            return CommitSortStrategies.Topological;
        }
    }

    public IEnumerable<LogEntry> QueryBy(string path)
    {
        throw new NotImplementedException();
    }

    public ICommitLog QueryBy(CommitFilter filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<LogEntry> QueryBy(string path, CommitFilter filter)
    {
        throw new NotImplementedException();
    }
}
