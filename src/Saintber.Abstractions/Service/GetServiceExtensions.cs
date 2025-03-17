namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 提供查詢服務的擴充方法，簡化查詢流程並統一錯誤處理方式。
    /// </summary>
    public static class GetServiceExtensions
    {
        /// <summary>
        /// 根據篩選條件取得唯一符合的資訊。
        /// 如果未找到資料則回傳 <c>null</c>，若查詢結果超過一筆資料則會拋出例外。
        /// </summary>
        /// <typeparam name="TDto">查詢結果 DTO 型別。</typeparam>
        /// <typeparam name="TFilterRequest">查詢請求的篩選條件型別。</typeparam>
        /// <param name="service">查詢服務實例。</param>
        /// <param name="filterRequest">包含篩選條件的請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>符合篩選條件的唯一資訊，若未找到資料則回傳 <c>null</c>。</returns>
        /// <exception cref="InvalidOperationException">當查詢結果超過一筆資料時拋出。</exception>
        public static async Task<TDto?> SingleOrDefaultAsync<TDto, TFilterRequest>(
            this IGetService<TDto, TFilterRequest> service,
            TFilterRequest filterRequest,
            CancellationToken cancellationToken = default)
            => (await service.GetAsync(filterRequest, cancellationToken).ConfigureAwait(false)).SingleOrDefault();
    }
}
