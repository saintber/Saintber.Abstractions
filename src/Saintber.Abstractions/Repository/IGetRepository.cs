namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 取得資料知識庫介面。
    /// </summary>
    /// <typeparam name="T">資料模型型別。</typeparam>
    /// <typeparam name="TFilter">篩選資料模型型別。</typeparam>
    public interface IGetRepository<T, TFilter>
    {
        /// <summary>
        /// 取得資料。
        /// </summary>
        /// <param name="filter">篩選條件。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>資料清單。</returns>
        Task<IEnumerable<T>> GetAsync(TFilter filter, CancellationToken cancellationToken = default);
    }
}
