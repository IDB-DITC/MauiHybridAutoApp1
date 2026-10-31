namespace MauiHybridAutoApp1.Web.Services
{
    public interface IServerLocalStorageService
    {
        ValueTask<RecordQueryResult<TRecord>> ReadAsync<TRecord>(RecordQueryRequest<TRecord> request);
        ValueTask<RecordDeleteResult> RemoveAsync<TRecord>(RecordDeleteRequest request);
        ValueTask<CommandResult> SaveAsync<TRecord>(CommandRequest<TRecord> request);
    }

    public record CommandRequest<TRecord>(string StorageName, TRecord Record);

    public record RecordDeleteRequest(string StorageName);
    public record RecordQueryRequest<TRecord>(string StorageName);

    public record CommandResult
    {
        public bool Successful { get; init; }
        public string Message { get; init; } = string.Empty;

        public static CommandResult Success()
            => new CommandResult { Successful = true };

        public static CommandResult Failure(string message)
            => new CommandResult { Successful = false };
    }

    public record RecordQueryResult<TRecord>
    {
        public TRecord? Record { get; init; }
        public bool Successful { get; init; }
        public string Message { get; init; } = string.Empty;

        public static RecordQueryResult<TRecord> Success(TRecord record)
            => new RecordQueryResult<TRecord> { Record = record, Successful = true };

        public static RecordQueryResult<TRecord> Failure(string message)
            => new RecordQueryResult<TRecord> { Message = message, Successful = false };
    }
    public record RecordDeleteResult
    {
        public bool Successful { get; init; }
        public string Message { get; init; } = string.Empty;

        public static RecordDeleteResult Success()
            => new RecordDeleteResult { Successful = true };

        public static RecordDeleteResult Failure(string message)
            => new RecordDeleteResult { Message = message, Successful = false };
    }
}