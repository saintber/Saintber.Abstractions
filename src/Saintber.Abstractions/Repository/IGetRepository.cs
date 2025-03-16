namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 定義查詢資料的儲存庫介面，負責根據篩選條件取得對應的數據模型清單。
    /// </summary>
    /// <typeparam name="TDataModel">查詢後回傳的數據模型型別。</typeparam>
    /// <typeparam name="TFilterDataModel">用於篩選查詢條件的儲存層數據模型型別。</typeparam>
    public interface IGetRepository<TDataModel, TFilterDataModel>
    {
        /// <summary>
        /// 根據提供的篩選條件查詢符合的數據模型清單。
        /// </summary>
        /// <param name="filterDataModel">包含查詢篩選條件的儲存層數據模型。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>符合篩選條件的數據模型清單。</returns>
        Task<IEnumerable<TDataModel>> GetAsync(TFilterDataModel filterDataModel, CancellationToken cancellationToken = default);
    }
}
