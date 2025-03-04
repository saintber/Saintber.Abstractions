namespace Saintber.Abstractions.Conversion
{
    /// <summary>
    /// 模型轉換器。
    /// </summary>
    /// <typeparam name="TSource">來源資料型別。</typeparam>
    /// <typeparam name="TTarget">目標資料型別。</typeparam>
    public interface IConverter<TSource, TTarget>
    {
        /// <summary>
        /// 將來源資料轉換至目標資料。
        /// </summary>
        /// <param name="source">來源資料。</param>
        /// <param name="target">目標資料。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        Task ConvertAsync(TSource source, TTarget target, CancellationToken cancellationToken = default);
    }
}
