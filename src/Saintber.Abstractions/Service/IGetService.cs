namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義查詢資料的服務介面，負責根據查詢請求取得符合條件的資訊清單。
    /// </summary>
    /// <typeparam name="TDto">查詢後回傳的 DTO 型別。</typeparam>
    /// <typeparam name="TFilterRequest">應用層的查詢請求型別。</typeparam>
    public interface IGetService<TDto, TFilterRequest>
    {
        /// <summary>
        /// 根據提供的查詢請求，取得符合條件的資訊清單。
        /// </summary>
        /// <param name="filterRequest">包含查詢條件的請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>符合查詢條件的資訊清單。</returns>
        Task<IEnumerable<TDto>> GetAsync(TFilterRequest filterRequest, CancellationToken cancellationToken = default);
    }
}
