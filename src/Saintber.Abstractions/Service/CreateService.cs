using FluentValidation;
using Saintber.Abstractions.Repository;
using Saintber.Abstractions.Validation;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義建立資訊的服務，並回傳字串識別碼。
    /// </summary>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public class CreateService<TCreateDataModel, TCreateRequest>
        : CreateService<string, TCreateDataModel, TCreateRequest>
    {
        public CreateService(
            ICreateServiceContext<TCreateDataModel, TCreateRequest> managerContext,
            ICreateRepository<TCreateDataModel> repository)
            : base(managerContext, repository) { }
    }

    /// <summary>
    /// 定義建立資訊的服務，並回傳識別碼。
    /// </summary>
    /// <typeparam name="TIdentifier">建立後回傳的識別碼型別。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public class CreateService<TIdentifier, TCreateDataModel, TCreateRequest>
        : CreateService<TIdentifier, TCreateDataModel, TIdentifier, TCreateRequest>
        , ICreateService<TIdentifier, TCreateRequest>
    {
        public CreateService(
            ICreateServiceContext<TIdentifier, TCreateDataModel, TCreateRequest> managerContext,
            ICreateRepository<TIdentifier, TCreateDataModel> repository)
            : base(managerContext, repository) { }
    }

    /// <summary>
    /// 定義建立資訊的服務，並回傳完整的應用層 DTO。
    /// </summary>
    /// <typeparam name="TDataModel">儲存層數據模型型別。</typeparam>
    /// <typeparam name="TCreateDataModel">用於建立的儲存層數據模型型別。</typeparam>
    /// <typeparam name="TDto">回傳給應用層的 DTO 型別。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public class CreateService<TDataModel, TCreateDataModel, TDto, TCreateRequest> : ICreateService<TDto, TCreateRequest>
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

        public async Task<TDto> CreateAsync(TCreateRequest create, CancellationToken cancellationToken = default)
        {
            // 資料檢核
            if (create == null) throw new ArgumentNullException(nameof(create));
            foreach (var validator in managerContext.CreateValidators ?? new List<ValidationHandler<TCreateRequest>>())
            {
                await validator.ValidateAndThrowAsync(create, cancellationToken).ConfigureAwait(false);
            }

            // 前置處理
            var createDataModel = await managerContext.OnBeforeCreateAsync(create, cancellationToken).ConfigureAwait(false);

            // 建立資料
            var dataModel = await repository.CreateAsync(createDataModel, cancellationToken).ConfigureAwait(false);

            // 後置處理
            var dto = await managerContext.OnAfterCreateAsync(dataModel, cancellationToken).ConfigureAwait(false);

            return dto;
        }

        /// <summary>
        /// 建立新的建立服務實例。
        /// </summary>
        /// <param name="serviceContext">建立服務的服務內容。</param>
        /// <param name="repository">建立資料的儲存庫。</param>
        /// <returns>新的建立服務實例。</returns>
        public static CreateService<TDataModel, TCreateDataModel, TDto, TCreateRequest> Create(
            ICreateServiceContext<TDataModel, TCreateDataModel, TDto, TCreateRequest> serviceContext,
            ICreateRepository<TDataModel, TCreateDataModel> repository)
            => new CreateService<TDataModel, TCreateDataModel, TDto, TCreateRequest>(serviceContext, repository);
    }
}
