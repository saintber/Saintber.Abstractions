using Saintber.Validation;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 提供建立資訊所需的管理內容，回傳字串識別碼。
    /// </summary>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public interface ICreateServiceContext<TCreateDataModel, TCreateRequest>
        : ICreateServiceContext<string, TCreateDataModel, TCreateRequest>
    {
    }

    /// <summary>
    /// 提供建立資訊所需的管理內容，回傳識別碼。
    /// </summary>
    /// <typeparam name="TIdentifier">建立後回傳的識別碼型別。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public interface ICreateServiceContext<TIdentifier, TCreateDataModel, TCreateRequest>
        : ICreateServiceContext<TIdentifier, TCreateDataModel, TIdentifier, TCreateRequest>
    {
    }

    /// <summary>
    /// 提供建立資訊所需的管理內容，並支援前後置處理。
    /// </summary>
    /// <typeparam name="TDataModel">儲存層數據模型型別。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型型別。</typeparam>
    /// <typeparam name="TDto">回傳給應用層的 DTO 型別。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public interface ICreateServiceContext<TDataModel, TCreateDataModel, TDto, TCreateRequest>
    {
        /// <summary>
        /// 取得建立資訊的驗證器清單。
        /// </summary>
        IEnumerable<ValidationHandler<TCreateRequest>> CreateValidators { get; }

        /// <summary>
        /// 在建立前執行的處理程序，例如轉換查詢請求或進行業務邏輯驗證。
        /// </summary>
        Func<TCreateRequest, CancellationToken, Task<TCreateDataModel>> OnBeforeCreateAsync { get; set; }

        /// <summary>
        /// 在建立後執行的處理程序，例如轉換回應數據、觸發事件或更新快取。
        /// </summary>
        Func<TDataModel, CancellationToken, Task<TDto>> OnAfterCreateAsync { get; set; }
    }
}
