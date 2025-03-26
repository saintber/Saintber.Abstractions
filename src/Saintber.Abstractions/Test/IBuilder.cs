namespace Saintber.Abstractions.Test
{
    /// <summary>
    /// 定義一個泛型建構器介面，用於非同步建立指定型別的實例。
    /// </summary>
    /// <typeparam name="T">建構的目標型別。</typeparam>
    public interface IBuilder<T>
    {
        /// <summary>
        /// 取得建構是否已經完成。
        /// 通常用於避免重複建構或在依賴流程中做判斷。
        /// </summary>
        bool IsBuilt { get; }

        /// <summary>
        /// 非同步執行建構流程，回傳建構後的實例。
        /// </summary>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>建構後的實例。</returns>
        Task<T> BuildAsync(CancellationToken cancellationToken = default);
    }
}
