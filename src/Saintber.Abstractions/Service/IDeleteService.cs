namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義刪除資訊的服務介面，負責根據請求刪除符合條件的資訊。
    /// </summary>
    /// <typeparam name="TDeleteRequest">應用層的刪除請求型別。</typeparam>
    public interface IDeleteService<TDeleteRequest>
    {
        /// <summary>
        /// 根據提供的刪除請求，執行資訊刪除。
        /// </summary>
        /// <param name="deleteRequest">包含刪除資訊的請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>非同步作業，表示刪除操作已完成。</returns>
        Task DeleteAsync(TDeleteRequest deleteRequest, CancellationToken cancellationToken = default);
    }
}
