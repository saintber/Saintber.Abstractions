using FluentValidation;
using Saintber.Abstractions.Repository;
using Saintber.Validation;

namespace Saintber.Abstractions.Service
{
    #region == 回傳識別碼 ==
    /// <summary>
    /// 定義建立資訊的服務，並回傳識別碼。
    /// </summary>
    /// <typeparam name="TIdentifier">建立後回傳的識別碼型別。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public class CreateService<TIdentifier, TCreateDataModel, TCreateRequest>
        : CreateService<TIdentifier, TCreateDataModel, TIdentifier, TCreateRequest>
        , ICreateService<TIdentifier, TCreateRequest>
        where TCreateDataModel : new()
    {
        public CreateService(
            ICreateServiceContext<TIdentifier, TCreateDataModel, TCreateRequest> managerContext,
            ICreateRepository<TIdentifier, TCreateDataModel> repository)
            : base(managerContext, repository) { }
    }

    /// <summary>
    /// 提供建立資訊所需的管理內容。
    /// </summary>
    /// <typeparam name="TIdentifier">建立後回傳的識別碼型別。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public interface ICreateServiceContext<TIdentifier, TCreateDataModel, TCreateRequest>
        : ICreateServiceContext<TIdentifier, TCreateDataModel, TIdentifier, TCreateRequest>
    {
    }
    #endregion

    #region == 回傳資料模型 ==
    /// <summary>
    /// 定義建立資訊的服務，並回傳完整的應用層 DTO。
    /// </summary>
    /// <typeparam name="TDataModel">儲存層數據模型型別。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型型別。</typeparam>
    /// <typeparam name="TDto">回傳給應用程式的 DTO 型別。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public class CreateService<TDataModel, TCreateDataModel, TDto, TCreateRequest>
        : ICreateService<TDto, TCreateRequest>
        where TCreateDataModel : new()
    {
        private ICreateServiceContext<TDataModel, TCreateDataModel, TDto, TCreateRequest> managerContext;
        private readonly ICreateRepository<TDataModel, TCreateDataModel> repository;

        public CreateService(
            ICreateServiceContext<TDataModel, TCreateDataModel, TDto, TCreateRequest> managerContext,
            ICreateRepository<TDataModel, TCreateDataModel> repository)
        {
            this.managerContext = managerContext;
            this.repository = repository;
        }

        /// <summary>
        /// 建立新的資訊並返回應用程式可用的 DTO。
        /// </summary>
        public async Task<TDto> CreateAsync(TCreateRequest create, CancellationToken cancellationToken = default)
        {
            // 資料檢核
            if (create == null) throw new ArgumentNullException(nameof(create));
            foreach (var validator in managerContext.CreateValidators ?? new List<ValidationHandler<TCreateRequest>>())
            {
                await validator.ValidateAndThrowAsync(create, cancellationToken).ConfigureAwait(false);
            }

            // 前置處理
            var createDataModel = await managerContext.BeforeCreateAsync(create, cancellationToken).ConfigureAwait(false);

            // 建立資料
            var dataModel = await repository.CreateAsync(createDataModel, cancellationToken).ConfigureAwait(false);

            // 後置處理
            var dto = await managerContext.AfterCreateAsync(dataModel, cancellationToken).ConfigureAwait(false);

            return dto;
        }
    }

    /// <summary>
    /// 提供建立資訊所需的管理內容，並支援前後置處理。
    /// </summary>
    /// <typeparam name="TDataModel">儲存層數據模型型別。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型型別。</typeparam>
    /// <typeparam name="TDto">回傳給應用程式的 DTO 型別。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public interface ICreateServiceContext<TDataModel, TCreateDataModel, TDto, TCreateRequest>
    {
        /// <summary>
        /// 取得建立資訊的驗證器清單。
        /// </summary>
        IEnumerable<ValidationHandler<TCreateRequest>> CreateValidators { get; }

        /// <summary>
        /// 在建立前執行的處理程序，可用於修改建立請求或進行業務邏輯驗證。
        /// </summary>
        Task<TCreateDataModel> BeforeCreateAsync(TCreateRequest create, CancellationToken cancellationToken = default);

        /// <summary>
        /// 在建立後執行的處理程序，可用於進一步處理已建立的數據，例如觸發事件或更新快取。
        /// </summary>
        Task<TDto> AfterCreateAsync(TDataModel dataModel, CancellationToken cancellationToken = default);
    }
    #endregion
}
