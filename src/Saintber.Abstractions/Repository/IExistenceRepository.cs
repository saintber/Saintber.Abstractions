namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 定義檢查資料存在性的儲存庫介面。
    /// </summary>
    /// <typeparam name="TDataFilter">用於篩選查詢條件的儲存層數據模型型別。</typeparam>
    public interface IExistenceRepository<TDataFilter>
    {
        /// <summary>
        /// 非同步檢查是否存在符合條件的資料。
        /// </summary>
        /// <param name="filter">包含查詢篩選條件的儲存層數據模型。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>如果存在符合條件的資料，返回 <c>true</c>；否則返回 <c>false</c>。</returns>
        public Task<bool> ExistsAsync(TDataFilter filter, CancellationToken cancellationToken = default);
    }
}
