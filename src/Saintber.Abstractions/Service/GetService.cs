using FluentValidation;
using Saintber.Abstractions.Repository;
using Saintber.Validation;

namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義查詢資料的服務，負責根據查詢請求取得對應的 DTO 清單。
    /// </summary>
    /// <typeparam name="TDataModel">儲存層使用的數據模型型別。</typeparam>
    /// <typeparam name="TFilterDataModel">儲存層使用的篩選條件數據模型型別。</typeparam>
    /// <typeparam name="TDto">回傳給應用層的 DTO 型別。</typeparam>
    /// <typeparam name="TFilterRequest">用於查詢的請求型別。</typeparam>
    public class GetService<TDataModel, TFilterDataModel, TDto, TFilterRequest> : IGetService<TDto, TFilterRequest>
        where TFilterDataModel : new()
        where TDto : new()
    {
        private readonly IGetServiceContext<TDataModel, TFilterDataModel, TDto, TFilterRequest> serviceContext;
        private readonly IGetRepository<TDataModel, TFilterDataModel> repository;

        /// <summary>
        /// 初始化查詢服務的新執行個體。
        /// </summary>
        /// <param name="serviceContext">查詢服務的服務內容。</param>
        /// <param name="repository">查詢資料的儲存庫。</param>
        public GetService(
            IGetServiceContext<TDataModel, TFilterDataModel, TDto, TFilterRequest> serviceContext,
            IGetRepository<TDataModel, TFilterDataModel> repository)
        {
            this.serviceContext = serviceContext;
            this.repository = repository;
        }

        /// <summary>
        /// 根據提供的查詢請求，取得符合條件的 DTO 清單。
        /// </summary>
        /// <param name="filterRequest">包含查詢條件的請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>符合查詢條件的 DTO 清單。</returns>
        public virtual async Task<IEnumerable<TDto>> GetAsync(TFilterRequest filterRequest, CancellationToken cancellationToken = default)
        {
            // 資料檢核
            if (filterRequest == null) throw new ArgumentNullException(nameof(filterRequest));
            foreach (var validator in serviceContext.GetValidators ?? new List<ValidationHandler<TFilterRequest>>())
            {
                await validator.ValidateAndThrowAsync(filterRequest, cancellationToken).ConfigureAwait(false);
            }

            // 前置處理（篩選條件轉換）
            var filterDataModel = await serviceContext.OnCreatingAsync(filterRequest, cancellationToken).ConfigureAwait(false);

            // 查詢資料
            var dataModels = await repository.GetAsync(filterDataModel, cancellationToken).ConfigureAwait(false);

            // 後置處理（轉換為 DTO）
            var dtoList = await serviceContext.OnCreatedAsync(dataModels, cancellationToken).ConfigureAwait(false);

            return dtoList;
        }

        /// <summary>
        /// 建立新的查詢服務實例。
        /// </summary>
        /// <param name="serviceContext">查詢服務的服務內容。</param>
        /// <param name="repository">查詢資料的儲存庫。</param>
        /// <returns>新的查詢服務實例。</returns>
        public static GetService<TDataModel, TFilterDataModel, TDto, TFilterRequest> Create(
            IGetServiceContext<TDataModel, TFilterDataModel, TDto, TFilterRequest> serviceContext,
            IGetRepository<TDataModel, TFilterDataModel> repository)
            => new GetService<TDataModel, TFilterDataModel, TDto, TFilterRequest>(serviceContext, repository);
    }
}
