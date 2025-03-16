namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義刪除資訊的服務介面。
    /// </summary>
    /// <typeparam name="TDelete">刪除資訊的型別。</typeparam>
    public interface IDeleteService<TDelete>
    {
        /// <summary>
        /// 根據提供的刪除資訊，執行資訊刪除。
        /// </summary>
        /// <param name="delete">包含刪除資訊的請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>非同步作業，表示刪除操作已完成。</returns>
        Task DeleteAsync(TDelete delete, CancellationToken cancellationToken = default);
    }
}
