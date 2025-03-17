using Saintber.Abstractions.Repository;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義更新資訊的服務，並回傳識別碼。
    /// </summary>
    /// <typeparam name="TUpdateDataModel">用於更新的儲存層數據模型。</typeparam>
    /// <typeparam name="TUpdateRequest">用於更新資訊的請求型別。</typeparam>
    public class UpdateService<TUpdateDataModel, TUpdateRequest>
        : UpdateService<string, TUpdateDataModel, TUpdateRequest>
    {
        public UpdateService(
            IUpdateServiceContext<TUpdateDataModel, TUpdateRequest> managerContext,
            IUpdateRepository<TUpdateDataModel> repository)
            : base(managerContext, repository) { }
    }

    /// <summary>
    /// 定義更新資訊的服務，並回傳識別碼。
    /// </summary>
    /// <typeparam name="TIdentifier">更新後回傳的識別碼型別。</typeparam>
    /// <typeparam name="TUpdateDataModel">用於更新的儲存層數據模型。</typeparam>
    /// <typeparam name="TUpdateRequest">用於更新資訊的請求型別。</typeparam>
    public class UpdateService<TIdentifier, TUpdateDataModel, TUpdateRequest>
        : UpdateService<TIdentifier, TUpdateDataModel, TIdentifier, TUpdateRequest>
        , IUpdateService<TIdentifier, TUpdateRequest>
    {
        public UpdateService(
            IUpdateServiceContext<TIdentifier, TUpdateDataModel, TUpdateRequest> managerContext,
            IUpdateRepository<TIdentifier, TUpdateDataModel> repository)
            : base(managerContext, repository) { }
    }

    /// <summary>
    /// 定義更新資訊的服務，並回傳完整的應用層 DTO。
    /// </summary>
    /// <typeparam name="TDataModel">儲存層數據模型型別。</typeparam>
    /// <typeparam name="TUpdateDataModel">異動資料模型型別。</typeparam>
    /// <typeparam name="TDto">回傳給應用程式的 DTO 型別。</typeparam>
    /// <typeparam name="TUpdateRequest">異動資訊型別。</typeparam>
    public class UpdateService<TDataModel, TUpdateDataModel, TDto, TUpdateRequest> : IUpdateService<TDto, TUpdateRequest>
    {
        private readonly IUpdateServiceContext<TDataModel, TUpdateDataModel, TDto, TUpdateRequest> managerContext;
        private readonly IUpdateRepository<TDataModel, TUpdateDataModel> repository;

        /// <summary>
        /// 更新資訊異動管理員的新執行個體。
        /// </summary>
        /// <param name="managerContext">資訊異動管理員內容。</param>
        /// <param name="repository">資料異動知識庫。</param>
        /// <returns>資訊異動管理員。</returns>
        public UpdateService(
            IUpdateServiceContext<TDataModel, TUpdateDataModel, TDto, TUpdateRequest> managerContext
            , IUpdateRepository<TDataModel, TUpdateDataModel> repository)
        {
            this.managerContext = managerContext;
            this.repository = repository;
        }

        public virtual async Task<TDto> UpdateAsync(TUpdateRequest update, CancellationToken cancellationToken = default)
        {
            // 資料檢核
            if (update == null) throw new ArgumentNullException(nameof(update));
            foreach (var validator in managerContext.UpdateValidators)
            {
                await validator.ValidateAsync(update, cancellationToken).ConfigureAwait(false);
            }

            // 前置處理
            var updateDataModel = await managerContext.OnCreatingAsync(update, cancellationToken).ConfigureAwait(false);

            // 異動資料
            var dataModel = await repository.UpdateAsync(updateDataModel, cancellationToken).ConfigureAwait(false);

            // 後置處理
            var dto = await managerContext.OnCreatedAsync(dataModel, cancellationToken).ConfigureAwait(false);

            return dto;
        }

        /// <summary>
        /// 更新資訊異動管理員的新執行個體。
        /// </summary>
        /// <param name="serviceContext">資訊異動管理員內容。</param>
        /// <param name="repository">資料異動知識庫。</param>
        /// <returns>資訊異動管理員。</returns>
        public static UpdateService<TDataModel, TUpdateDataModel, TDto, TUpdateRequest> Update(
            IUpdateServiceContext<TDataModel, TUpdateDataModel, TDto, TUpdateRequest> serviceContext
            , IUpdateRepository<TDataModel, TUpdateDataModel> repository)
            => new UpdateService<TDataModel, TUpdateDataModel, TDto, TUpdateRequest>(serviceContext, repository);
    }
}
