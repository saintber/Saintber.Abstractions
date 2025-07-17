namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 定義更新資料的儲存庫介面，負責將更新請求存入儲存層，並返回更新結果。
    /// </summary>
    /// <typeparam name="TDataUpdate">用於更新的儲存層數據模型型別。</typeparam>
    public interface IUpdateRepository<TDataUpdate> : IUpdateRepository<string, TDataUpdate> { }

    /// <summary>
    /// 定義更新資料的儲存庫介面，負責將更新請求存入儲存層，並返回更新結果。
    /// </summary>
    /// <typeparam name="TUpdateResult">更新後回傳的結果型別，可為識別碼、更新戳記或完整數據模型。</typeparam>
    /// <typeparam name="TDataUpdate">用於更新的儲存層數據模型型別。</typeparam>
    public interface IUpdateRepository<TUpdateResult, TDataUpdate>
    {
        /// <summary>
        /// 更新一批資料並返回對應的數據模型、更新戳記或識別碼清單。
        /// </summary>
        /// <param name="updates">包含要更新的數據模型清單。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>更新後的結果清單，可能為識別碼、更新戳記或完整數據模型。</returns>
        Task<IReadOnlyList<TUpdateResult>> UpdateAsync(IEnumerable<TDataUpdate> updates, CancellationToken cancellationToken = default);
    }
}
