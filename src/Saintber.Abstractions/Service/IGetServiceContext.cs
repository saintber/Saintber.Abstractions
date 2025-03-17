using Saintber.Validation;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 提供查詢資訊的管理內容，並支援查詢前後的處理邏輯。
    /// </summary>
    /// <typeparam name="TDataModel">儲存層使用的數據模型型別。</typeparam>
    /// <typeparam name="TFilterDataModel">儲存層使用的篩選條件數據模型型別。</typeparam>
    /// <typeparam name="TDto">回傳給應用層的 DTO 型別。</typeparam>
    /// <typeparam name="TFilterRequest">用於查詢的請求型別。</typeparam>
    public interface IGetServiceContext<TDataModel, TFilterDataModel, TDto, TFilterRequest>
    {
        // <summary>
        /// 取得查詢請求的驗證器清單。
        /// </summary>
        IEnumerable<ValidationHandler<TFilterRequest>> GetValidators { get; }

        /// <summary>
        /// 在查詢前執行的處理程序，例如轉換查詢請求或進行業務邏輯驗證。
        /// </summary>
        Func<TFilterRequest, CancellationToken, Task<TFilterDataModel>> OnCreatingAsync { get; set; }

        /// <summary>
        /// 在查詢後執行的處理程序，例如轉換回應數據、觸發事件或更新快取。
        /// </summary>
        Func<IEnumerable<TDataModel>, CancellationToken, Task<IEnumerable<TDto>>> OnCreatedAsync { get; set; }
    }
}
