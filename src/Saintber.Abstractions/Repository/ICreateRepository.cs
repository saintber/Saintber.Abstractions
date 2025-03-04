namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 建立資料知識庫介面。
    /// </summary>
    /// <typeparam name="TKey">資料識別碼型別。</typeparam>
    /// <typeparam name="TCreateModel">建立資料模型型別。</typeparam>
    public interface ICreateRepository<TKey, TCreateModel>
    {
        /// <summary>
        /// 建立資料。
        /// </summary>
        /// <param name="creates">建立資料清單。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>新資料識別碼清單。</returns>
        Task<IEnumerable<TKey>> CreateAsync(IEnumerable<TCreateModel> creates, CancellationToken cancellationToken = default);
    }
}
