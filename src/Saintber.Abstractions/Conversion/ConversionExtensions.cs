namespace Saintber.Abstractions.Conversion
{
    /// <summary>
    /// 模型轉換擴充方法。
    /// </summary>
    public static class ConversionExtensions
    {
        /// <summary>
        /// 轉換來源資料為目標資料。
        /// </summary>
        /// <typeparam name="TSource">來源資料型別。</typeparam>
        /// <typeparam name="TTarget">目標資料型別。</typeparam>
        /// <param name="converter">資料轉換器。</param>
        /// <param name="source">來源資料。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>目標資料。</returns>
        public static async Task<TTarget> ConvertAsync<TSource, TTarget>(this IConverter<TSource, TTarget> converter
            , TSource source, CancellationToken cancellationToken = default)
            where TTarget : new()
        {
            var target = new TTarget();
            await converter.ConvertAsync(source, target, cancellationToken);
            return target;
        }

        /// <summary>
        /// 轉換來源資料清單為目標資料清單。
        /// </summary>
        /// <typeparam name="TSource">來源資料型別。</typeparam>
        /// <typeparam name="TTarget">目標資料型別。</typeparam>
        /// <param name="converter">資料轉換器。</param>
        /// <param name="sources">來源資料清單。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>目標資料清單。</returns>
        public static async Task<IEnumerable<TTarget>> ConvertAsync<TSource, TTarget>(this IConverter<TSource, TTarget> converter
            , IEnumerable<TSource> sources, CancellationToken cancellationToken = default)
            where TTarget : new()
        {
            var targets = new List<TTarget>();
            foreach (var source in sources)
            {
                var target = new TTarget();
                await converter.ConvertAsync(source, target, cancellationToken);
                targets.Add(target);
            }
            return targets;
        }

        /// <summary>
        /// 反轉目標資料為來源資料。
        /// </summary>
        /// <typeparam name="TSource">來源資料型別。</typeparam>
        /// <typeparam name="TTarget">目標資料型別。</typeparam>
        /// <param name="converter">資料轉換器。</param>
        /// <param name="target">目標資料。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>來源資料。</returns>
        public static async Task<TSource> RevertAsync<TSource, TTarget>(this IReverter<TSource, TTarget> converter
            , TTarget target, CancellationToken cancellationToken = default)
            where TSource : new()
        {
            var source = new TSource();
            await converter.RevertAsync(source, target, cancellationToken);
            return source;
        }

        /// <summary>
        /// 反轉目標資料清單為來源資料清單。
        /// </summary>
        /// <typeparam name="TSource">來源資料型別。</typeparam>
        /// <typeparam name="TTarget">目標資料型別。</typeparam>
        /// <param name="converter">資料轉換器。</param>
        /// <param name="targets">目標資料清單。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>來源資料清單。</returns>
        public static async Task<IEnumerable<TSource>> RevertAsync<TSource, TTarget>(this IReverter<TSource, TTarget> converter
            , IEnumerable<TTarget> targets, CancellationToken cancellationToken = default)
            where TSource : new()
        {
            var sources = new List<TSource>();
            foreach (var target in targets)
            {
                var source = new TSource();
                await converter.RevertAsync(source, target, cancellationToken);
                sources.Add(source);
            }
            return sources;
        }
    }
}
