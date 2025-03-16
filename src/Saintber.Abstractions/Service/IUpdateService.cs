namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義更新資訊的服務介面。
    /// </summary>
    /// <typeparam name="TUpdate">更新資訊的型別。</typeparam>
    public interface IUpdateService<TUpdate>
        : IUpdateService<string, TUpdate>
    { }

    /// <summary>
    /// 定義更新資訊的服務介面。
    /// </summary>
    /// <typeparam name="TStampOrKey">更新戳記或資訊識別碼型別。</typeparam>
    /// <typeparam name="TUpdate">更新資訊的型別。</typeparam>
    public interface IUpdateService<TStampOrKey, TUpdate>
    {
        /// <summary>
        /// 根據提供的更新資訊，執行資訊更新並返回更新戳記或識別碼。
        /// </summary>
        /// <param name="update">包含更新資訊的請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>更新後的資訊戳記或識別碼。</returns>
        Task<TStampOrKey> UpdateAsync(TUpdate update, CancellationToken cancellationToken = default);
    }
}
