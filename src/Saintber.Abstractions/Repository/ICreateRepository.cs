namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 定義建立資料的儲存庫介面，回傳字串識別碼。
    /// </summary>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型型別。</typeparam>
    public interface ICreateRepository<TCreateDataModel> : ICreateRepository<string, TCreateDataModel> { }

    // <summary>
    /// 定義建立資料的儲存庫介面，負責將建立請求存入儲存層並返回結果。
    /// </summary>
    /// <typeparam name="TDataResult">建立後回傳的結果型別，可為識別碼或完整的數據模型。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型型別。</typeparam>
    public interface ICreateRepository<TDataResult, TCreateDataModel>
    {
        /// <summary>
        /// 新增一批資料並返回對應的結果。
        /// </summary>
        /// <param name="creates">包含要新增的數據模型清單。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>已建立的結果清單，可能為識別碼或完整數據模型。</returns>
        Task<IEnumerable<TDataResult>> CreateAsync(IEnumerable<TCreateDataModel> creates, CancellationToken cancellationToken = default);
    }
}
