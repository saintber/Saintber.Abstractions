namespace Saintber.Abstractions.Repository
{
    /// <summary>
    /// 定義刪除資料的儲存庫介面，負責根據刪除條件刪除對應的數據。
    /// </summary>
    /// <typeparam name="TDataFilter">用於篩選刪除條件的儲存層數據模型型別。</typeparam>
    public interface IDeleteRepository<TDataFilter>
    {
        /// <summary>
        /// 根據提供的刪除篩選條件，刪除符合的資料。
        /// </summary>
        /// <param name="deleteFilter">包含刪除條件的模型物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>非同步作業，表示刪除操作已完成。</returns>
        Task DeleteAsync(TDataFilter deleteFilter, CancellationToken cancellationToken = default);
    }
}
