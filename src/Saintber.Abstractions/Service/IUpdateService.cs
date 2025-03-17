namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義更新資訊的服務介面，負責根據請求異動資訊，並回傳識別碼或異動結果。
    /// </summary>
    /// <typeparam name="TUpdateRequest">應用層的異動請求型別。</typeparam>
    public interface IUpdateService<TUpdateRequest> : IUpdateService<string, TUpdateRequest> { }

    /// <summary>
    /// 定義更新資訊的服務介面，負責根據請求異動資訊，並回傳識別碼或異動結果。
    /// </summary>
    /// <typeparam name="TResult">更新後回傳的結果型別，可為識別碼、更新戳記或完整資料模型。</typeparam>
    /// <typeparam name="TUpdateRequest">應用層的異動請求型別。</typeparam>
    public interface IUpdateService<TResult, TUpdateRequest>
    {
        /// <summary>
        /// 根據提供的更新資訊，執行資訊更新並返回更新戳記或識別碼。
        /// </summary>
        /// <param name="update">包含更新資訊的請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>更新後的結果，可為識別碼、更新戳記或完整資料模型。</returns>
        Task<TResult> UpdateAsync(TUpdateRequest update, CancellationToken cancellationToken = default);
    }
}
