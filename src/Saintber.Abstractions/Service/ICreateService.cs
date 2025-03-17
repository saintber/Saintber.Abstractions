namespace Saintber.Abstractions.Service
{
    /// <summary>
    /// 定義建立資訊的服務介面，負責根據請求建立新資訊並回傳字串識別碼。
    /// </summary>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public interface ICreateService<TCreateRequest> : ICreateService<string, TCreateRequest> { }

    /// <summary>
    /// 定義建立資訊的服務介面，負責根據請求建立新資訊並回傳結果。
    /// </summary>
    /// <typeparam name="TResult">建立後回傳的結果型別，可為識別碼、DTO 或完整資料模型。</typeparam>
    /// <typeparam name="TCreateRequest">用於建立資訊的請求型別。</typeparam>
    public interface ICreateService<TResult, TCreateRequest>
    {
        /// <summary>
        /// 根據提供的建立請求，創建新的資訊並返回建立結果。
        /// </summary>
        /// <param name="createRequest">包含建立資訊的請求物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>已建立的資訊結果，可能為識別碼、DTO 或完整資料模型。</returns>
        Task<TResult> CreateAsync(TCreateRequest createRequest, CancellationToken cancellationToken = default);
    }
}
