namespace Saintber.Abstractions.Conversion
{
    /// <summary>
    /// 模型還原器。
    /// </summary>
    /// <typeparam name="TSource">來源資料型別。</typeparam>
    /// <typeparam name="TTarget">目標資料型別。</typeparam>
    public interface IReverter<TSource, TTarget>
    {
        /// <summary>
        /// 將目標轉換為來源。
        /// </summary>
        /// <param name="source">來源資料。</param>
        /// <param name="target">目標資料。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        Task RevertAsync(TSource source, TTarget target, CancellationToken cancellationToken = default);
    }
}
