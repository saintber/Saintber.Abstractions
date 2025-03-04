namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 刪除資料知識庫介面。
    /// </summary>
    /// <typeparam name="TFilterModel">篩選資料模型型別。</typeparam>
    public interface IDeleteRepository<TFilterModel>
    {
        /// <summary>
        /// 刪除資料。
        /// </summary>
        /// <param name="filter">刪除條件。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>非同步作業資訊。</returns>
        Task DeleteAsync(TFilterModel filter, CancellationToken cancellationToken = default);
    }
}
