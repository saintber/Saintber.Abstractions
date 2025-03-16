namespace Saintber.Abstractions.Repository
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 建立資料模型。
        /// </summary>
        /// <typeparam name="TModel">建立後回傳的資料模型型別。</typeparam>
        /// <typeparam name="TCreateModel">用於建立資料的模型型別。</typeparam>
        /// <param name="repository">儲存庫。</param>
        /// <param name="create">建立資料。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>資料識別碼。</returns>
        public static async Task<TModel> CreateAsync<TModel, TCreateModel>(this ICreateRepository<TModel, TCreateModel> repository
            , TCreateModel create, CancellationToken cancellationToken = default)
            => (await repository.CreateAsync(new List<TCreateModel> { create }, cancellationToken)).Single();

        /// <summary>
        /// 取得符合篩選條件的單筆資料。
        /// </summary>
        /// <typeparam name="TModel">資料模型型別。</typeparam>
        /// <typeparam name="TFilter">篩選資料模型型別。</typeparam>
        /// <param name="repository">知識庫。</param>
        /// <param name="filter">篩選資料模型。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>符合指定識別碼的資料。</returns>
        public static async Task<TModel?> SingleOrDefaultAsync<TModel, TFilterModel>(this IGetRepository<TModel, TFilterModel> repository
            , TFilterModel filter, CancellationToken cancellationToken = default)
            => (await repository.GetAsync(filter, cancellationToken)).SingleOrDefault();

        /// <summary>
        /// 取得指定識別碼的資料。
        /// </summary>
        /// <typeparam name="TStampOrKey">異動戳記或識別碼型別。</typeparam>
        /// <typeparam name="TUpdateModel">異動資料模型型別。</typeparam>
        /// <param name="repository">知識庫。</param>
        /// <param name="update">異動資料模型。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>異動戳記或識別碼。</returns>
        public static async Task<TStampOrKey> UpdateAsync<TStampOrKey, TUpdateModel>(this IUpdateRepository<TStampOrKey, TUpdateModel> repository
            , TUpdateModel update, CancellationToken cancellationToken = default)
            => (await repository.UpdateAsync(new List<TUpdateModel> { update }, cancellationToken)).Single();
    }
}
