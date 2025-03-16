namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 定義建立資料的儲存庫介面。
    /// </summary>
    /// <typeparam name="TDataResult">建立後回傳的數據模型型別，可為數據模型或識別碼。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型型別。</typeparam>
    public interface ICreateRepository<TDataResult, TCreateDataModel>
    {
        /// <summary>
        /// 新增一批資料並返回對應的數據模型清單或識別碼清單。
        /// </summary>
        /// <param name="creates">包含要新增的數據模型清單。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>已建立的數據模型清單或識別碼清單。</returns>
        Task<IEnumerable<TDataResult>> CreateAsync(IEnumerable<TCreateDataModel> creates, CancellationToken cancellationToken = default);
    }
}
