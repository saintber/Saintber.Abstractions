using Saintber.Validation;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 提供建立資訊所需的管理內容，並支援前後置處理。
    /// </summary>
    /// <typeparam name="TFilterDataModel">用於篩選的儲存層數據模型型別。</typeparam>
    /// <typeparam name="TDeleteRequest">用於建立資訊的請求型別。</typeparam>
    public interface IDeleteServiceContext<TFilterDataModel, TDeleteRequest>
    {
        /// <summary>
        /// 取得建立資訊的驗證器清單。
        /// </summary>
        IEnumerable<ValidationHandler<TDeleteRequest>> DeleteValidators { get; }

        /// <summary>
        /// 在建立前執行的處理程序，例如轉換查詢請求或進行業務邏輯驗證。
        /// </summary>
        /// <param name="create">建立請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>經過前置處理後的建立數據模型。</returns>
        Task<TFilterDataModel> BeforeDeleteAsync(TDeleteRequest create, CancellationToken cancellationToken = default);

        /// <summary>
        /// 在建立後執行的處理程序，例如轉換回應數據、觸發事件或更新快取。
        /// </summary>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns></returns>
        Task AfterDeleteAsync(CancellationToken cancellationToken = default);
    }
}
