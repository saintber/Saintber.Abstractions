namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 定義刪除資料的知識庫介面。
    /// </summary>
    /// <typeparam name="TFilterModel">用於篩選刪除條件的模型型別。</typeparam>
    public interface IDeleteRepository<TFilterModel>
    {
        /// <summary>
        /// 根據提供的刪除條件，刪除符合的資料。
        /// </summary>
        /// <param name="filter">包含刪除條件的模型物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>非同步作業。</returns>
        Task DeleteAsync(TFilterModel filter, CancellationToken cancellationToken = default);
    }
}
