using Saintber.Abstractions.Repository;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 刪除資訊管理員基底類別。
    /// </summary>
    /// <typeparam name="TFilterModel">篩選資料模型型別。</typeparam>
    /// <typeparam name="TDelete">刪除資訊型別。</typeparam>
    public class DeleteService<TFilterModel, TDelete> : IDeleteService<TDelete>
        where TFilterModel : new()
    {
        private readonly IDeleteServiceContext<TFilterModel, TDelete> managerContext;
        private readonly IDeleteRepository<TFilterModel> repository;

        /// <summary>
        /// 建立資訊刪除管理員的新執行個體。
        /// </summary>
        /// <param name="managerContext">資訊刪除管理員內容。</param>
        /// <param name="repository">資料刪除知識庫。</param>
        /// <returns>資訊刪除管理員。</returns>
        public DeleteService(
            IDeleteServiceContext<TFilterModel, TDelete> managerContext
            , IDeleteRepository<TFilterModel> repository)
        {
            this.managerContext = managerContext;
            this.repository = repository;
        }

        public virtual async Task DeleteAsync(TDelete delete, CancellationToken cancellationToken = default)
        {
            // 資料檢核
            if (delete == null) throw new ArgumentNullException(nameof(delete));
            foreach (var validator in managerContext.DeleteValidators)
            {
                await validator.ValidateAsync(delete, cancellationToken).ConfigureAwait(false);
            }

            // 前置處理
            var deleteDataModel = await managerContext.BeforeDeleteAsync(delete, cancellationToken).ConfigureAwait(false);

            // 刪除資料
            await repository.DeleteAsync(deleteDataModel, cancellationToken).ConfigureAwait(false);

            // 後置處理
            await managerContext.AfterDeleteAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 建立資訊刪除管理員的新執行個體。
        /// </summary>
        /// <param name="managerContext">資訊刪除管理員內容。</param>
        /// <param name="repository">資料刪除知識庫。</param>
        /// <returns>資訊刪除管理員。</returns>
        public static DeleteService<TFilterModel, TDelete> Create(
            IDeleteServiceContext<TFilterModel, TDelete> managerContext
            , IDeleteRepository<TFilterModel> repository)
            => new DeleteService<TFilterModel, TDelete>(managerContext, repository);
    }
}
