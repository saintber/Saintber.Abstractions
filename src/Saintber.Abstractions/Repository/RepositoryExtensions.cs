namespace Saintber.Abstractions.Repository
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 取得指定識別碼的資料。
        /// </summary>
        /// <typeparam name="TKey">識別碼型別。</typeparam>
        /// <typeparam name="TCreate">建立資料模型型別。</typeparam>
        /// <param name="repository">知識庫。</param>
        /// <param name="create">建立資料。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>資料識別碼。</returns>
        public static async Task<TKey> CreateAsync<TKey, TCreate>(this ICreateRepository<TKey, TCreate> repository
            , TCreate create, CancellationToken cancellationToken = default)
            => (await repository.CreateAsync(new List<TCreate> { create }, cancellationToken)).Single();

        /// <summary>
        /// 取得符合篩選條件的單筆資料。
        /// </summary>
        /// <typeparam name="TKey">識別碼型別。</typeparam>
        /// <typeparam name="T">資料模型型別。</typeparam>
        /// <typeparam name="TFilter">篩選資料模型型別。</typeparam>
        /// <param name="repository">知識庫。</param>
        /// <param name="filter">篩選資料模型。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>符合指定識別碼的資料。</returns>
        public static async Task<T?> SingleOrDefaultAsync<TKey, T, TFilter>(this IGetRepository<T, TFilter> repository
            , TFilter filter, CancellationToken cancellationToken = default)
            => (await repository.GetAsync(filter, cancellationToken)).SingleOrDefault();

        /// <summary>
        /// 取得指定識別碼的資料。
        /// </summary>
        /// <typeparam name="TStampOrKey">異動戳記或識別碼型別。</typeparam>
        /// <typeparam name="TUpdate">異動資料模型型別。</typeparam>
        /// <param name="repository">知識庫。</param>
        /// <param name="update">異動資料模型。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>異動戳記或識別碼。</returns>
        public static async Task<TStampOrKey> UpdateAsync<TStampOrKey, TUpdate>(this IUpdateRepository<TStampOrKey, TUpdate> repository
            , TUpdate update, CancellationToken cancellationToken = default)
            => (await repository.UpdateAsync(new List<TUpdate> { update }, cancellationToken)).Single();
    }
}
