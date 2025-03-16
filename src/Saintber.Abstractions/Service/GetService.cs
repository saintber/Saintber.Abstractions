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
    /// <typeparam name="TDto">回傳給應用程式的 DTO 型別。</typeparam>
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
        /// <param name="serviceContext">查詢服務內容。</param>
        /// <param name="repository">查詢儲存庫。</param>
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
            var filterDataModel = await serviceContext.BeforeQueryAsync(filterRequest, cancellationToken).ConfigureAwait(false);

            // 查詢資料
            var dataModels = await repository.GetAsync(filterDataModel, cancellationToken).ConfigureAwait(false);

            // 後置處理（轉換為 DTO）
            var dtoList = await serviceContext.AfterQueryAsync(dataModels, cancellationToken).ConfigureAwait(false);

            return dtoList;
        }

        /// <summary>
        /// 建立查詢服務的新執行個體。
        /// </summary>
        /// <param name="serviceContext">查詢服務內容。</param>
        /// <param name="repository">查詢儲存庫。</param>
        /// <returns>新的查詢服務實例。</returns>
        public static GetService<TDataModel, TFilterDataModel, TDto, TFilterRequest> Create(
            IGetServiceContext<TDataModel, TFilterDataModel, TDto, TFilterRequest> serviceContext,
            IGetRepository<TDataModel, TFilterDataModel> repository)
            => new GetService<TDataModel, TFilterDataModel, TDto, TFilterRequest>(serviceContext, repository);
    }

    /// <summary>
    /// 提供查詢資訊所需的管理內容，並支援查詢前後的處理邏輯。
    /// </summary>
    /// <typeparam name="TDataModel">儲存層使用的數據模型型別。</typeparam>
    /// <typeparam name="TFilterDataModel">儲存層使用的篩選條件數據模型型別。</typeparam>
    /// <typeparam name="TDto">回傳給應用層的 DTO 型別。</typeparam>
    /// <typeparam name="TFilterRequest">用於查詢的請求型別。</typeparam>
    public interface IGetServiceContext<TDataModel, TFilterDataModel, TDto, TFilterRequest>
    {
        /// <summary>
        /// 取得查詢請求的驗證器清單。
        /// </summary>
        IEnumerable<ValidationHandler<TFilterRequest>> GetValidators { get; }

        /// <summary>
        /// 在查詢前執行的處理程序，可用於轉換查詢請求或進行業務邏輯驗證。
        /// </summary>
        Task<TFilterDataModel> BeforeQueryAsync(TFilterRequest filterRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// 在查詢後執行的處理程序，可用於轉換回應數據，例如觸發事件或更新快取。
        /// </summary>
        Task<IEnumerable<TDto>> AfterQueryAsync(IEnumerable<TDataModel> dataModels, CancellationToken cancellationToken = default);
    }
}
