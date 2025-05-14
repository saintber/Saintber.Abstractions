namespace Saintber.Abstractions.DataScope
{
    /// <summary>
    /// 資料範圍提供者介面，用於提供資料範圍建立與查詢。
    /// </summary>
    public interface IDataScopeProvider
    {
        /// <summary>
        /// 根據當前登入身分，取得或建立對應擁有者類型的資料識別碼。
        /// </summary>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>對應擁有者的資料識別碼，若無則建立新識別碼後返回。</returns>
        Task<string> GetOrCreateDataIdAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 取得可存取的資料識別碼清單。
        /// </summary>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>可存取的資料識別碼清單。</returns>
        Task<IEnumerable<string>> GetAccessibleDataIdsAsync(CancellationToken cancellationToken = default);
    }
}
