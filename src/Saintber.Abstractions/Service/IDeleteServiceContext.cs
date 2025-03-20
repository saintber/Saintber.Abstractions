using Saintber.Validation;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 提供刪除資訊所需的管理內容，並支援前後置處理。
    /// </summary>
    /// <typeparam name="TFilterDataModel">用於篩選刪除條件的儲存層數據模型型別。</typeparam>
    /// <typeparam name="TDeleteRequest">應用層的刪除請求型別。</typeparam>
    public interface IDeleteServiceContext<TFilterDataModel, TDeleteRequest>
    {
        /// <summary>
        /// 取得刪除資訊的驗證器清單。
        /// </summary>
        IEnumerable<ValidationHandler<TDeleteRequest>> DeleteValidators { get; }

        /// <summary>
        /// 在刪除前執行的處理程序，例如轉換刪除請求或進行業務邏輯驗證。
        /// </summary>
        Func<TDeleteRequest, CancellationToken, Task<TFilterDataModel>> OnBeforeDeleteAsync { get; set; }

        /// <summary>
        /// 在刪除後執行的處理程序，例如觸發事件或更新快取。
        /// </summary>
        Func<CancellationToken, Task> OnAfterDeleteAsync { get; set; }
    }
}
