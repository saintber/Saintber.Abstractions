using Saintber.Abstractions.Repository;
using Saintber.Validation;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義刪除資訊的服務，負責根據請求刪除符合條件的資訊。
    /// </summary>
    /// <typeparam name="TFilterDataModel">儲存層的刪除篩選條件數據模型型別。</typeparam>
    /// <typeparam name="TDeleteRequest">應用層的刪除請求型別。</typeparam>
    public class DeleteService<TFilterDataModel, TDeleteRequest> : IDeleteService<TDeleteRequest>
        where TFilterDataModel : new()
    {
        private readonly IDeleteServiceContext<TFilterDataModel, TDeleteRequest> serviceContext;
        private readonly IDeleteRepository<TFilterDataModel> repository;

        /// <summary>
        /// 初始化刪除資訊服務的新執行個體。
        /// </summary>
        /// <param name="serviceContext">刪除資訊的服務內容。</param>
        /// <param name="repository">刪除資料的儲存庫。</param>
        public DeleteService(
            IDeleteServiceContext<TFilterDataModel, TDeleteRequest> serviceContext
            , IDeleteRepository<TFilterDataModel> repository)
        {
            this.serviceContext = serviceContext;
            this.repository = repository;
        }

        public virtual async Task DeleteAsync(TDeleteRequest delete, CancellationToken cancellationToken = default)
        {
            // 資料檢核
            if (delete == null) throw new ArgumentNullException(nameof(delete));
            foreach (var validator in serviceContext.DeleteValidators ?? new List<ValidationHandler<TDeleteRequest>>())
            {
                await validator.ValidateAndThrowAsync(delete, cancellationToken).ConfigureAwait(false);
            }

            // 前置處理
            var deleteDataModel = await serviceContext.OnCreatingAsync(delete, cancellationToken).ConfigureAwait(false);

            // 刪除資料
            await repository.DeleteAsync(deleteDataModel, cancellationToken).ConfigureAwait(false);

            // 後置處理
            await serviceContext.OnCreatedAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 建立新的刪除資訊服務實例。
        /// </summary>
        /// <param name="serviceContext">刪除資訊的服務內容。</param>
        /// <param name="repository">刪除資料的儲存庫。</param>
        /// <returns>新的刪除資訊服務實例。</returns>
        public static DeleteService<TFilterDataModel, TDeleteRequest> Create(
            IDeleteServiceContext<TFilterDataModel, TDeleteRequest> serviceContext
            , IDeleteRepository<TFilterDataModel> repository)
            => new DeleteService<TFilterDataModel, TDeleteRequest>(serviceContext, repository);
    }
}
