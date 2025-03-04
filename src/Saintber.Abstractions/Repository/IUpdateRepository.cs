namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 異動資料知識庫介面。
    /// </summary>
    /// <typeparam name="TKeyOrStamp">回傳識別碼或異動戳記型別。</typeparam>
    /// <typeparam name="TUpdate">異動資料模型型別。</typeparam>
    public interface IUpdateRepository<TKeyOrStamp, TUpdate>
    {
        /// <summary>
        /// 異動資料。
        /// </summary>
        /// <param name="updates">異動資料清單。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>資料識別碼或異動戳記清單。</returns>
        Task<IEnumerable<TKeyOrStamp>> UpdateAsync(IEnumerable<TUpdate> updates, CancellationToken cancellationToken = default);
    }
}
