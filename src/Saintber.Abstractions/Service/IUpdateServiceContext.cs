using Saintber.Validation;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 提供更新資訊所需的管理內容。
    /// </summary>
    /// <typeparam name="TUpdateDataModel">用於更新的儲存層數據模型。</typeparam>
    /// <typeparam name="TUpdateRequest">用於更新資訊的請求型別。</typeparam>
    public interface IUpdateServiceContext<TUpdateDataModel, TUpdateRequest>
        : IUpdateServiceContext<string, TUpdateDataModel, TUpdateRequest>
    {
    }

    /// <summary>
    /// 提供更新資訊所需的管理內容。
    /// </summary>
    /// <typeparam name="TIdentifier">更新後回傳的識別碼型別。</typeparam>
    /// <typeparam name="TUpdateDataModel">用於更新的儲存層數據模型。</typeparam>
    /// <typeparam name="TUpdateRequest">用於更新資訊的請求型別。</typeparam>
    public interface IUpdateServiceContext<TIdentifier, TUpdateDataModel, TUpdateRequest>
        : IUpdateServiceContext<TIdentifier, TUpdateDataModel, TIdentifier, TUpdateRequest>
    {
    }

    /// <summary>
    /// 異動管理員內容介面。
    /// </summary>
    /// <typeparam name="TUpdateDataModel">異動資料模型。</typeparam>
    /// <typeparam name="TUpdateRequest">異動資訊。</typeparam>
    public interface IUpdateServiceContext<TDataModel, TUpdateDataModel, TDto, TUpdateRequest>
    {
        /// <summary>
        /// 異動驗證器清單。
        /// </summary>
        IEnumerable<ValidationHandler<TUpdateRequest>> UpdateValidators { get; }

        /// <summary>
        /// 在更新前執行的處理程序，可用於修改更新請求或進行業務邏輯驗證。
        /// </summary>
        Task<TUpdateDataModel> BeforeUpdateAsync(TUpdateRequest create, CancellationToken cancellationToken = default);

        /// <summary>
        /// 在更新後執行的處理程序，可用於進一步處理已更新的數據，例如觸發事件或更新快取。
        /// </summary>
        Task<TDto> AfterUpdateAsync(TDataModel dataModel, CancellationToken cancellationToken = default);
    }
}
