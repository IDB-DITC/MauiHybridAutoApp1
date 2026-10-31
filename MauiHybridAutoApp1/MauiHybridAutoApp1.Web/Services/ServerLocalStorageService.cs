using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MauiHybridAutoApp1.Web.Services
{

    public class ServerLocalStorageService(ProtectedLocalStorage _storage) : IServerLocalStorageService
    {
        //private readonly ProtectedLocalStorage _storage;

        //public LocalStorageService(ProtectedLocalStorage storage)
        //    => _storage = storage;

        public async ValueTask<CommandResult> SaveAsync<TRecord>(CommandRequest<TRecord> request)
        {
            if (request.Record is not null)
                await _storage.SetAsync(request.StorageName, request.Record);

            // No return so we return success!
            return CommandResult.Success();
        }
        public async ValueTask<RecordDeleteResult> RemoveAsync<TRecord>(RecordDeleteRequest request)
        {
            if (request.StorageName is not null)
                await _storage.DeleteAsync(request.StorageName);

            // No return so we return success!
            return RecordDeleteResult.Success();
        }
        public async ValueTask<RecordQueryResult<TRecord>> ReadAsync<TRecord>(RecordQueryRequest<TRecord> request)
        {
            // We need to cover the situation were the component calling this is in the initial page
            // and Blazor server is trying to statically render the page
            try
            {
                var result = await _storage.GetAsync<TRecord>(request.StorageName);
                return new RecordQueryResult<TRecord> { Successful = result.Success, Record = result.Value, Message = $"Failed to retrieve a value for {request.StorageName}" };
            }
            catch
            {
                return new RecordQueryResult<TRecord> { Successful = false, Message = $"Failed to retrieve a value for {request.StorageName}" };
            }
        }
    }




}
