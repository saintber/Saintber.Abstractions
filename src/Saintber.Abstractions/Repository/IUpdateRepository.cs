namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 定義更新資料的儲存庫介面。
    /// </summary>
    /// <typeparam name="TStampOrKey">更新後回傳的更新戳記或識別碼型別。</typeparam>
    /// <typeparam name="TUpdateModel">用於更新資料的模型型別。</typeparam>
    public interface IUpdateRepository<TStampOrKey, TUpdateModel>
    {
        /// <summary>
        /// 更新一批資料並返回對應的更新戳記或識別碼清單。
        /// </summary>
        /// <param name="updates">包含要更新的資料清單。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>更新後的更新戳記或識別碼清單。</returns>
        Task<IEnumerable<TStampOrKey>> UpdateAsync(IEnumerable<TUpdateModel> updates, CancellationToken cancellationToken = default);
    }
}
